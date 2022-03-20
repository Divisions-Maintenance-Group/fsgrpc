module Helpers
open System.Text.RegularExpressions

let sanitizeHex hex = 
    Regex.Replace(hex, "[^a-z0-9]", "")

let bytesToHex (bytes: byte seq) =
    bytes |> Seq.map (fun x -> System.String.Format("{0:x2}", x)) |> String.concat ""

let bytesFromHex (hex: string) : byte array =
    let toNibble (char: char) =
        match char with
        | '0' -> 0uy
        | '1' -> 1uy
        | '2' -> 2uy
        | '3' -> 3uy
        | '4' -> 4uy
        | '5' -> 5uy
        | '6' -> 6uy
        | '7' -> 7uy
        | '8' -> 8uy
        | '9' -> 9uy
        | 'A' | 'a' -> 10uy
        | 'B' | 'b' -> 11uy
        | 'C' | 'c' -> 12uy
        | 'D' | 'd' -> 13uy
        | 'E' | 'e' -> 14uy
        | 'F' | 'f' -> 15uy
        | _ -> failwith $"invalid char {char}"
    let toByte (chars: char array) =
        let c1 = chars[0] |> toNibble
        let c2 = chars[1] |> toNibble
        let b = (c1 <<< 4) ||| (c2)
        b
    let bytes = hex |> sanitizeHex |> Seq.chunkBySize 2 |> Seq.map toByte |> Array.ofSeq
    bytes

let readerFromHex (hex: string) : Google.Protobuf.CodedInputStream =
    let bytes = bytesFromHex hex
    let reader = new Google.Protobuf.CodedInputStream(bytes)
    reader

let inline roundTrip< ^T when ^T : (static member Proto : Lazy<FsGrpc.ProtoDef< 'T>>)> (value: ^T) =
    value |> FsGrpc.encode< ^T> |> FsGrpc.decode< ^T>
