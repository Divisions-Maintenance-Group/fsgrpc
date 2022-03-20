module ProtoDump

type FieldHead = {
    Number: uint
    WireType: string
    Metadata: byte list
}
type Field = {
    Head: FieldHead
    Data: byte list
}

let bytesToHex bytes =
    bytes |> Seq.map (fun b -> sprintf "%02x" b) |> String.concat ""

// converts from base 127 (big endian) to uint64
let base127ToUint64 (bytes: byte list) : uint64 =
    let bytes = bytes |> List.rev
    let rec recFromBase127 (acc: uint64) (bytes: byte list) : uint64 =
        match bytes with
        | n :: remain -> recFromBase127 ((acc <<< 7) ||| (uint64 (n &&& 0b0111_1111uy))) remain
        | [] -> acc
    recFromBase127 0UL bytes
// attempts to return the bytes of the next varint
// fails if there is any data but not a complete varint
// returns none for end of stream
let tryTakeVarintBytes (bytes: byte seq) : Result<byte list option * byte seq, string> =
    let rec recTakeVarint (acc: byte list) (bytes: byte seq) : Result<byte list option * byte seq, string> =
        let next = bytes |> Seq.tryHead
        match (acc, next) with
        | [], None -> Ok (None, [])
        | _, None -> Error "end of bytes before end of varint"
        | _, Some b -> 
            let msb = b &&& 0b1000_0000uy
            let appended = (acc @ [b])
            match msb with
            | 0uy -> Ok (Some appended, (bytes |> Seq.skip 1))
            | _ -> recTakeVarint appended (bytes |> Seq.skip 1)
    let b127 = recTakeVarint [] bytes
    match b127 with
    | Error e -> Error e
    | Ok (v, bytes) -> Ok (v, bytes)
// attempts to return the bytes of the next varint
// fails if there is not a complete varint to read
let takeVarintBytes (bytes: byte seq) : Result<byte list * byte seq, string> =
    match tryTakeVarintBytes bytes with
    | Ok (None, _) -> Error "end of bytes before varint bytes"
    | Ok (Some bytes, remain) -> Ok (bytes, remain)
    | Error e -> Error e
// attempts to take the next byte
// returns none if there isn't one
let tryTakeByte (bytes: byte seq) =
    let next = bytes |> Seq.tryHead
    match next with
    | None -> (None, seq [])
    | Some b -> (Some b, bytes |> Seq.skip 1)
// takes the specified number of bytes
// failing if there are not that many bytes to read
let takeBytes count (bytes: byte seq) =
    let rec recTakeBytes (acc: byte list) (count: int) (bytes: byte seq) =
        match (count, bytes) with
        | (0, bytes) -> Ok (acc, bytes)
        | (count, bytes) ->
            match tryTakeByte bytes with
            | (Some next, bytes) -> recTakeBytes (acc @ [next]) (count - 1) bytes
            | (None, _) ->
                let error = sprintf "end of bytes with %d remaining" count
                Error error
    recTakeBytes [] count bytes
// attempts to read an entire field, tag and data
// returns none for end of stream
// returns an error if any errors are encountered
let tryTakeField (bytes: byte seq) : Result<(Field option * byte seq), string> =
    let result = tryTakeVarintBytes bytes
    match result with
    | Error e -> Error e
    | Ok (None, remain) ->
        Ok (None, remain)
    | Ok (Some tagAndTypeBytes, bytes) ->
        let varint = base127ToUint64 tagAndTypeBytes
        let wt = byte (varint &&& 0b111UL)
        let tag = (uint (varint >>> 3))
        match wt with
        | 0uy ->
            let result = takeVarintBytes bytes
            match result with
            | Error e -> Error e
            | Ok (bytes, remain) ->
                let field = {
                    Head = {
                        Number = tag
                        WireType = "varint"
                        Metadata = tagAndTypeBytes
                    }
                    Data = bytes
                }
                Ok (Some field, remain)
        | 1uy ->
            let result = takeBytes 8 bytes
            match result with
            | Error e -> Error e
            | Ok (bytes, remain) ->
                let field = {
                    Head = {
                        Number = tag
                        WireType = "double"
                        Metadata = tagAndTypeBytes
                    }
                    Data = bytes
                }
                Ok (Some field, remain)
        | 2uy ->
            match takeVarintBytes bytes with
            | Error e -> Error e
            | Ok (lengthBytes, bytes) ->
                let length = base127ToUint64 lengthBytes
                match takeBytes (int length) bytes with
                | Error e -> Error e
                | Ok (bytes, remain) ->
                    let field = {
                        Head = {
                            Number = tag
                            WireType = "<data>"
                            Metadata = (tagAndTypeBytes @ lengthBytes)
                        }
                        Data = bytes
                    }
                    Ok (Some field, remain)
        | 3uy
        | 4uy ->
            let error = sprintf "unsupported wt %u" wt
            Error error
        | 5uy ->
            let result = takeBytes 4 bytes
            match result with
            | Error e -> Error e
            | Ok (bytes, remain) ->
                let field = {
                    Head = {
                        Number = tag
                        WireType = "single"
                        Metadata = tagAndTypeBytes
                    }
                    Data = bytes
                }
                Ok (Some field, remain)
        | _ ->
            let error = sprintf "invalid wt %u" wt
            Error error
let rec toProtoFields (bytes: byte seq) =
    let rec recToProtoFields (acc: Field list) (bytes: byte seq) : Result<Field list * byte seq, string> =
        let result = tryTakeField bytes
        match result with
        | Error e ->
            Error e
        | Ok (Some field, remain) ->
            let next = acc @ [field]
            recToProtoFields next remain
        | Ok (None, bytes) ->
            Ok (acc, bytes)
    recToProtoFields [] bytes
let rec toProtoFieldsPartial (bytes: byte seq) =
    let rec recToProtoFields (acc: Field list) (bytes: byte seq) : Field list * byte seq * string =
        let result = tryTakeField bytes
        match result with
        | Error e ->
            (acc, bytes, e)
        | Ok (Some field, remain) ->
            let next = acc @ [field]
            recToProtoFields next remain
        | Ok (None, bytes) ->
            (acc, bytes, "")
    recToProtoFields [] bytes
let header prefix (head: FieldHead) = 
    $"%s{prefix}[%d{head.Number}. %s{head.WireType}] %s{bytesToHex head.Metadata}"
let line prefix (field: Field) =
    $"%s{header prefix field.Head} %s{bytesToHex field.Data}"
let toProtoHex (bytes: byte seq) : string seq =
    seq {
        let rec recPrintProtoHex (prefix: string) (fields: Field list) : string seq =
            seq {
                let protoHex (prefix: string) (field: Field) =
                    line prefix field
                for field in fields do
                    match field.Head.WireType with
                    | "<data>" ->
                        match toProtoFields field.Data with
                        | Ok (fields, remain) ->
                            yield (header prefix field.Head)
                            yield! recPrintProtoHex (prefix + "  ") fields
                            let hexOfRemain = bytesToHex remain
                            if hexOfRemain.Length > 0 then
                                let remain = sprintf "%sREMAIN: %s" prefix hexOfRemain
                                yield remain
                        | Error _ ->
                            yield protoHex prefix field
                    | _ ->
                        yield protoHex prefix field

            }
        let (fields, remain, error) = toProtoFieldsPartial bytes
        yield! recPrintProtoHex "" fields
        if not (remain |> Seq.isEmpty) then
            let remainHex = (bytesToHex remain)
            let remainStr = sprintf "REMAIN: (%d b) %s" (remain |> Seq.length) remainHex
            yield remainStr
        if error <> "" then
            let errorStr = sprintf "ERROR: %s" error
            yield errorStr
    }
let dumpProtoHex (bytes: byte seq) =
    let lines = toProtoHex bytes
    for line in lines do
        printfn "%s" line