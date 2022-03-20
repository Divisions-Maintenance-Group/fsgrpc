module FsGrpc
open System.IO

let private ``???``<'T> : 'T = raise (System.NotImplementedException())

let read (r: Google.Protobuf.CodedInputStream) (tag: outref<int>) : bool =
    let tagAndType = r.ReadTag ()
    tag <- int (tagAndType >>> 3)
    tagAndType <> 0u

type ProtoDef<'M> = {
    Name: string
    Empty: 'M
    Size: 'M -> int
    Encode: Google.Protobuf.CodedOutputStream -> 'M -> unit
    Decode: Google.Protobuf.CodedInputStream -> 'M
}

let inline ProtoOf< ^T when ^T : (static member Proto : Lazy<ProtoDef< ^T>>)> : Lazy<ProtoDef< ^T>> =
    ((^T) : (static member Proto : Lazy<ProtoDef< ^T>>) ())

let encodeProto<'T> (proto: Lazy<ProtoDef<'T>>) (v: 'T) =
    let {Encode = encodeTo} = proto.Force()
    use memstr = new MemoryStream()
    use writer = new Google.Protobuf.CodedOutputStream(memstr)
    encodeTo writer v
    writer.Flush()
    memstr.ToArray()

let decodeProto<'T> (proto: Lazy<ProtoDef<'T>>) (bytes: byte array) : 'T =
    let {Decode = decode} = proto.Force()
    let cis = new Google.Protobuf.CodedInputStream(bytes)
    decode cis

let inline encode< 'T when 'T : (static member Proto : Lazy<ProtoDef< 'T>>)> (v: 'T) : byte array =
    encodeProto ProtoOf< 'T> v

let inline decode< 'T when 'T : (static member Proto : Lazy<ProtoDef< 'T>>)> (bytes: byte array) : 'T =
    decodeProto ProtoOf< 'T> bytes

type ValueSize<'T> =
| Fixed of int
| Variable

type RepeatEncoding<'T> =
| Packed of ValueSize<'T>
| Repeat

type Codec = Google.Protobuf.CodedOutputStream
type Writer = Google.Protobuf.CodedOutputStream
type Reader = Google.Protobuf.CodedInputStream
type WireType = Google.Protobuf.WireFormat.WireType

module WriteTag =
    let Varint (writer: Writer) (tag: int) =
        writer.WriteTag(tag, WireType.Varint)
    let Fixed64 (writer: Writer) (tag: int) =
        writer.WriteTag(tag, WireType.Fixed64)
    let Fixed32 (writer: Writer) (tag: int) =
        writer.WriteTag(tag, WireType.Fixed32)
    let LengthDelimited (writer: Writer) (tag: int) =
        writer.WriteTag(tag, WireType.LengthDelimited)

let private defer v =
    let r () = v
    r

type ValueCodec<'V> = {
    WriteTag: Writer -> int -> unit
    WriteValue: Writer -> 'V -> unit
    ReadValue: Reader -> 'V
    RepeatEncoding: RepeatEncoding<'V>
    CalcSize: 'V -> int
    // Q: why does this return a function?
    // A: protocol buffer message definitions can be circular
    //    and since this library generates the definitions of the messages at runtime
    //    and a message which is defined by other messages uses the defintions of those messages
    //    you might need to define a message using its own definition which can't work
    GetDefault: unit -> 'V
    IsNonDefault: 'V -> bool
}

let private calcFieldSize (valcodec: ValueCodec<'V>) (tag: int) (value: 'V) =
    if valcodec.IsNonDefault value then
        Writer.ComputeInt32Size(tag <<< 3) +
        valcodec.CalcSize value
    else 0

let private writeField (valcodec: ValueCodec<'V>) (tag: int) (writer: Writer) (value: 'V) =
    if valcodec.IsNonDefault value then
        valcodec.WriteTag writer tag
        valcodec.WriteValue writer value

module ValueCodec =
    let Double =
        let writeValue (writer: Writer) (value: double) =
            writer.WriteDouble(value)
        let readValue (reader: Reader) : double =
            reader.ReadDouble ()
        let isNonDefault (value: double) =
            value <> 0.0
        {
            WriteTag = WriteTag.Fixed64
            WriteValue = writeValue
            ReadValue = readValue
            RepeatEncoding = Packed (Fixed 8)
            CalcSize = Codec.ComputeDoubleSize
            GetDefault = defer 0.0
            IsNonDefault = isNonDefault
        }
    let Float =
        let writeValue (writer: Writer) (value: float32) =
            writer.WriteFloat(value)
        let readValue (reader: Reader) : float32 =
            reader.ReadFloat ()
        let isNonDefault (value: float32) =
            value <> 0f
        {
            WriteTag = WriteTag.Fixed32
            WriteValue = writeValue
            ReadValue = readValue
            RepeatEncoding = Packed (Fixed 4)
            CalcSize = Codec.ComputeFloatSize
            GetDefault = defer 0f
            IsNonDefault = isNonDefault
        }
    let Int64 =
        let writeValue (writer: Writer) (value: int64) =
            writer.WriteInt64(value)
        let readValue (reader: Reader) : int64 =
            reader.ReadInt64 ()
        let isNonDefault (value: int64) =
            value <> 0
        {
            WriteTag = WriteTag.Varint
            WriteValue = writeValue
            ReadValue = readValue
            RepeatEncoding = Packed Variable
            CalcSize = Codec.ComputeInt64Size
            GetDefault = defer 0L
            IsNonDefault = isNonDefault
        }
    let UInt64 =
        let writeValue (writer: Writer) (value: uint64) =
            writer.WriteUInt64(value)
        let readValue (reader: Reader) : uint64 =
            reader.ReadUInt64 ()
        let isNonDefault (value: uint64) =
            value <> 0UL
        {
            WriteTag = WriteTag.Varint
            WriteValue = writeValue
            ReadValue = readValue
            RepeatEncoding = Packed Variable
            CalcSize = Codec.ComputeUInt64Size
            GetDefault = defer 0UL
            IsNonDefault = isNonDefault
        }
    let Int32 =
        let writeValue (writer: Writer) (value: int32) =
            writer.WriteInt32(value)
        let readValue (reader: Reader) : int32 =
            reader.ReadInt32 ()
        let isNonDefault (value: int32) =
            value <> 0
        {
            WriteTag = WriteTag.Varint
            WriteValue = writeValue
            ReadValue = readValue
            RepeatEncoding = Packed Variable
            CalcSize = Codec.ComputeInt32Size
            GetDefault = defer 0
            IsNonDefault = isNonDefault
        }
    let Fixed64 =
        let writeValue (writer: Writer) (value: uint64) =
            writer.WriteFixed64(value)
        let readValue (reader: Reader) : uint64 =
            reader.ReadFixed64 ()
        let isNonDefault (value: uint64) =
            value <> 0UL
        {
            WriteTag = WriteTag.Fixed64
            WriteValue = writeValue
            ReadValue = readValue
            RepeatEncoding = Packed (Fixed 8)
            CalcSize = Codec.ComputeFixed64Size
            GetDefault = defer 0UL
            IsNonDefault = isNonDefault
        }
    let Fixed32 =
        let writeValue (writer: Writer) (value: uint32) =
            writer.WriteFixed32(value)
        let readValue (reader: Reader) : uint32 =
            reader.ReadFixed32 ()
        let isNonDefault (value: uint32) =
            value <> 0u
        {
            WriteTag = WriteTag.Fixed32
            WriteValue = writeValue
            ReadValue = readValue
            RepeatEncoding = Packed (Fixed 4)
            CalcSize = Codec.ComputeFixed32Size
            GetDefault = defer 0u
            IsNonDefault = isNonDefault
        }
    let Bool =
        let writeValue (writer: Writer) (value: bool) =
            writer.WriteBool(value)
        let readValue (reader: Reader) : bool =
            reader.ReadBool ()
        let isNonDefault (value: bool) =
            value <> false
        {
            WriteTag = WriteTag.Varint
            WriteValue = writeValue
            ReadValue = readValue
            RepeatEncoding = Packed Variable
            CalcSize = Codec.ComputeBoolSize
            GetDefault = defer false
            IsNonDefault = isNonDefault
        }
    let String =
        let writeValue (writer: Writer) (value: string) =
            writer.WriteString(value)
        let readValue (reader: Reader) : string =
            reader.ReadString ()
        let isNonDefault (value: string) =
            value.Length <> 0
        {
            WriteTag = WriteTag.LengthDelimited
            WriteValue = writeValue
            ReadValue = readValue
            RepeatEncoding = Repeat
            CalcSize = Codec.ComputeStringSize
            GetDefault = defer ""
            IsNonDefault = isNonDefault
        }
    let Bytes =
        let writeValue (writer: Writer) (value: Google.Protobuf.ByteString) =
            writer.WriteBytes(value)
        let readValue (reader: Reader) : Google.Protobuf.ByteString =
            reader.ReadBytes ()
        let isNonDefault (value: Google.Protobuf.ByteString) =
            value.Length <> 0
        {
            WriteTag = WriteTag.LengthDelimited
            WriteValue = writeValue
            ReadValue = readValue
            RepeatEncoding = Repeat
            CalcSize = Codec.ComputeBytesSize
            GetDefault = defer Google.Protobuf.ByteString.Empty
            IsNonDefault = isNonDefault
        }
    let UInt32 =
        let writeValue (writer: Writer) (value: uint32) =
            writer.WriteUInt32(value)
        let readValue (reader: Reader) : uint32 =
            reader.ReadUInt32 ()
        let isNonDefault (value: uint32) =
            value <> 0u
        {
            WriteTag = WriteTag.Varint
            WriteValue = writeValue
            ReadValue = readValue
            RepeatEncoding = Packed Variable
            CalcSize = Codec.ComputeUInt32Size
            GetDefault = defer 0u
            IsNonDefault = isNonDefault
        }
    let SFixed32 =
        let writeValue (writer: Writer) (value: int) =
            writer.WriteSFixed32(value)
        let readValue (reader: Reader) : int =
            reader.ReadSFixed32 ()
        let isNonDefault (value: int) =
            value <> 0
        {
            WriteTag = WriteTag.Fixed32
            WriteValue = writeValue
            ReadValue = readValue
            RepeatEncoding = Packed (Fixed 4)
            CalcSize = Codec.ComputeSFixed32Size
            GetDefault = defer 0
            IsNonDefault = isNonDefault
        }
    let SFixed64 =
        let writeValue (writer: Writer) (value: int64) =
            writer.WriteSFixed64(value)
        let readValue (reader: Reader) : int64 =
            reader.ReadSFixed64 ()
        let isNonDefault (value: int64) =
            value <> 0
        {
            WriteTag = WriteTag.Fixed64
            WriteValue = writeValue
            ReadValue = readValue
            RepeatEncoding = Packed (Fixed 8)
            CalcSize = Codec.ComputeSFixed64Size
            GetDefault = defer 0L
            IsNonDefault = isNonDefault
        }
    let SInt32 =
        let writeValue (writer: Writer) (value: int) =
            writer.WriteSInt32(value)
        let readValue (reader: Reader) : int =
            reader.ReadSInt32 ()
        let isNonDefault (value: int) =
            value <> 0
        {
            WriteTag = WriteTag.Varint
            WriteValue = writeValue
            ReadValue = readValue
            RepeatEncoding = Packed Variable
            CalcSize = Codec.ComputeSInt32Size
            GetDefault = defer 0
            IsNonDefault = isNonDefault
        }
    let SInt64 =
        let writeValue (writer: Writer) (value: int64) =
            writer.WriteSInt64(value)
        let readValue (reader: Reader) : int64 =
            reader.ReadSInt64 ()
        let isNonDefault (value: int64) =
            value <> 0
        {
            WriteTag = WriteTag.Varint
            WriteValue = writeValue
            ReadValue = readValue
            RepeatEncoding = Packed Variable
            CalcSize = Codec.ComputeSInt64Size
            GetDefault = defer 0L
            IsNonDefault = isNonDefault
        }

    let EnumFor<'E when 'E : equality> (castTo: int -> 'E) (castFrom: 'E -> int) =
        let writeValue (writer: Writer) (value: 'E) =
            Int32.WriteValue writer (castFrom value)
        let readValue (reader: Reader) : 'E =
            let v = Int32.ReadValue reader
            (castTo v)
        let computeSize (value: 'E) =
            Writer.ComputeInt32Size (castFrom value)
        let defVal : 'E = (castTo 0)
        let isNonDefault (value: 'E) =
            value <> defVal
        {
            WriteTag = WriteTag.Varint
            WriteValue = writeValue
            ReadValue = readValue
            RepeatEncoding = Packed Variable
            CalcSize = computeSize
            GetDefault = defer defVal
            IsNonDefault = isNonDefault
        }

    let inline Enum<'E when 'E : (static member op_Explicit : 'E -> int) and 'E : enum<int> and 'E : equality> : ValueCodec<'E> =
        EnumFor<'E> LanguagePrimitives.EnumOfValue int

    let MessageFrom<'M when 'M : equality> (proto: Lazy<ProtoDef<'M>>) : ValueCodec<'M> =
        // this calculates the length-prefixed size
        let calcSize = fun (m: 'M) ->
            let size = proto.Force().Size m
            let length = Writer.ComputeLengthSize(size)
            size + length
        // this will write a length-prefixed message
        let writeValue (writer: Writer) (value: 'M) =
            // this calculates the raw size
            let size = proto.Force().Size value
            writer.WriteInt32(size)
            proto.Force().Encode writer value
        let readValue (reader: Reader) : 'M =
            let bytes = reader.ReadBytes()
            use subreader = bytes.CreateCodedInput()
            proto.Force().Decode subreader
        let isNonDefault (value: 'M) =
            not (value = proto.Force().Empty)
        let getDefault () =
            proto.Force().Empty
        {
            WriteTag = WriteTag.LengthDelimited
            WriteValue = writeValue
            ReadValue = readValue
            RepeatEncoding = Repeat
            CalcSize = calcSize
            // the following are not actually used because these are used when this value is used for a non-optional field
            // but protocol buffers doesn't allow non-optional messages,
            // However, if it ever does, then the following should be the correct behavior
            GetDefault = getDefault 
            IsNonDefault = isNonDefault
        }
    
    let inline Message< ^M when ^M : equality and ^M : (static member Proto : Lazy<ProtoDef< ^M>>)> : ValueCodec< ^M> =
        let proto = ProtoOf< ^M>
        MessageFrom proto
    
    let Wrap<'P when 'P : equality> (primitive: ValueCodec<'P>) : ValueCodec<'P> =
        // Note: this will convert the primitive into a message with the value in field 1
        //       but it does not actually handle the optional (None/Nullable) part of this
        //       which will be handled by using this with FieldCodec.Optional as all message types are
        let wrapProto =
            lazy
            let defVal = primitive.GetDefault()
            let calcFieldSize = calcFieldSize primitive 1
            let writeField = writeField primitive 1
            { // ProtoDef<'P>
                Name = "<Wrapper>"
                Empty = defVal
                Size = fun (m : 'P) ->
                    calcFieldSize m
                Encode = fun w m ->
                    writeField w m
                Decode = fun r ->
                    let mutable value = defVal
                    let mutable tag = 0
                    while read r &tag do
                        match tag with
                        | 1 -> value <- primitive.ReadValue r
                        | _ -> r.SkipLastField()
                    value
            }
        MessageFrom wrapProto
    
    let Packed<'P> (primitive: ValueCodec<'P>) : ValueCodec<'P seq> =
        let valtype =
            match primitive.RepeatEncoding with
            | Packed valtype -> valtype
            | Repeat -> failwith "Invalid use of Packed with non-packed primitive type"
        let sizeOf value =
            match valtype with
            | Fixed size -> (value |> Seq.length) * size
            | Variable -> (value |> Seq.sumBy primitive.CalcSize)
        let writeValue (w: Writer) (value: 'P seq) =
            let size = sizeOf value
            w.WriteLength(size)
            for v in value do
                primitive.WriteValue w v
        let readValue (r: Reader) : 'P seq =
            match valtype with
            | Fixed size ->
                let sub = r.ReadBytes()
                let count = sub.Length / size
                let output = Array.zeroCreate count
                let subreader = sub.CreateCodedInput()
                for i = 0 to count - 1 do
                    let item = primitive.ReadValue subreader
                    output[i] <- item
                output
            | Variable _ -> 
                let builder = new System.Collections.Generic.List<'P>()
                let sub = r.ReadBytes()
                use subreader = sub.CreateCodedInput()
                while not subreader.IsAtEnd do
                    let item = primitive.ReadValue subreader
                    builder.Add item
                builder.ToArray()
        let calcSize (value: 'P seq) =
            let dataSize = sizeOf value
            let lengthSize = Codec.ComputeLengthSize(dataSize)
            lengthSize + dataSize
        let getDefault () =
            seq []
        let isNonDefault (v: 'P seq) =
            not (v |> Seq.isEmpty)
        {
            WriteTag = WriteTag.LengthDelimited
            WriteValue = writeValue
            ReadValue = readValue
            RepeatEncoding = Repeat
            CalcSize = calcSize
            // the following are not actually used because these are used when this value is used for a non-optional field
            // but protocol buffers doesn't allow non-optional messages
            // if it ever did, then the following should work
            GetDefault = getDefault 
            IsNonDefault = isNonDefault
        }
    
    let private createMapRecordProto<'K, 'V> (keycodec: ValueCodec<'K>) (valcodec: ValueCodec<'V>) =
        lazy
        let defVal: 'K * 'V = (keycodec.GetDefault(), valcodec.GetDefault())
        let calcKeySize = calcFieldSize keycodec 1
        let calcValSize = calcFieldSize valcodec 2
        let writeKey = writeField keycodec 1
        let writeVal = writeField valcodec 2
        { // ProtoDef<MapRecord<'K,'V>>
            Name = "<MapRecord>"
            Empty = defVal
            Size = fun ((key, value): 'K * 'V) ->
                calcKeySize key +
                calcValSize value
            Encode = fun w (key, value) ->
                writeKey w key
                writeVal w value
            Decode = fun r ->
                let mutable key = keycodec.GetDefault()
                let mutable value = valcodec.GetDefault()
                let mutable tag = 0
                while read r &tag do
                    match tag with
                    | 1 -> key <- (keycodec.ReadValue r)
                    | 2 -> value <- (valcodec.ReadValue r)
                    | _ -> r.SkipLastField()
                (key, value)
        }

    let MapRecord<'K, 'V when 'K : equality and 'V : equality> (keycodec: ValueCodec<'K>) (valcodec: ValueCodec<'V>) =
        let proto = createMapRecordProto keycodec valcodec
        MessageFrom proto

    let private epoch = NodaTime.Instant.FromUnixTimeSeconds(0)
    let private timestampProto =
        lazy
        let decompose (instant: NodaTime.Instant) = 
            let seconds = int64 (floor (instant.Minus epoch).TotalSeconds)
            let rounded = NodaTime.Instant.FromUnixTimeSeconds(seconds)
            let nanos = int (instant.Minus rounded).TotalNanoseconds
            (seconds, nanos)
        let compose ((seconds, nanos): (int64 * int32)) =
            NodaTime.Instant.FromUnixTimeSeconds(seconds).PlusNanoseconds(nanos)
        let defVal = NodaTime.Instant.FromUnixTimeSeconds(0L)
        let calcSecondsSize = calcFieldSize Int64 1
        let calcNanosSize = calcFieldSize Int32 1
        let writeSeconds = writeField Int64 1
        let writeNanos = writeField Int32 2
        { // ProtoDef<NodaTime.Instant>
            Name = "Timestamp"
            Empty = defVal
            Size = fun v ->
                let (seconds, nanos) = decompose v
                calcSecondsSize seconds +
                calcNanosSize nanos
            Encode = fun w v ->
                let (seconds, nanos) = decompose v
                writeSeconds w seconds
                writeNanos w nanos
            Decode = fun r ->
                let mutable seconds = 0L
                let mutable nanos = 0
                let mutable tag = 0
                while read r &tag do
                    match tag with
                    | 1 -> seconds <- Int64.ReadValue r
                    | 2 -> nanos <- Int32.ReadValue r
                    | _ -> r.SkipLastField()
                let value = compose (seconds, nanos)
                value
        }
    
    let private durationProto =
        lazy
        let decompose (duration: NodaTime.Duration) = 
            let seconds = int64 (floor duration.TotalSeconds)
            let rounded = NodaTime.Duration.FromSeconds seconds
            let nanos = int (duration.Minus rounded).TotalNanoseconds
            (seconds, nanos)
        let compose ((seconds, nanos): (int64 * int32)) =
            NodaTime.Duration.FromSeconds(seconds).Plus(NodaTime.Duration.FromNanoseconds (int64 nanos))
        let defVal = NodaTime.Duration.Zero
        let calcSecondsSize = calcFieldSize Int64 1
        let calcNanosSize = calcFieldSize Int32 1
        let writeSeconds = writeField Int64 1
        let writeNanos = writeField Int32 2
        { // ProtoDef<NodaTime.Duration>
            Name = "Duration"
            Empty = defVal
            Size = fun v ->
                let (seconds, nanos) = decompose v
                calcSecondsSize seconds +
                calcNanosSize nanos
            Encode = fun w v ->
                let (seconds, nanos) = decompose v
                writeSeconds w seconds
                writeNanos w nanos
            Decode = fun r ->
                let mutable seconds = 0L
                let mutable nanos = 0
                let mutable tag = 0
                while read r &tag do
                    match tag with
                    | 1 -> seconds <- Int64.ReadValue r
                    | 2 -> nanos <- Int32.ReadValue r
                    | _ -> r.SkipLastField()
                let value = compose (seconds, nanos)
                value
        }
    
    let Timestamp = MessageFrom timestampProto
    let Duration = MessageFrom durationProto

// A "Field" has a value and a tag
// 'V is the type of the value of the field on the record
// 'R is the type that you get for each incoming tag
// 'V and 'R differ in cases such as optional, repeated, oneof, and map fields
//    where presence/absence of the field itself communicates information
type FieldCodec<'V> = {
    CalcFieldSize: 'V -> int
    WriteField: Writer -> 'V -> unit
    // why does this return a function?
    // see ValueCodec.GetDefault
    GetDefault: unit -> 'V
}

module FieldCodec =
    let Primitive<'V> (valcodec: ValueCodec<'V>) (tag: int) : FieldCodec<'V> =
        {
            CalcFieldSize = calcFieldSize valcodec tag
            WriteField = writeField valcodec tag
            GetDefault = valcodec.GetDefault
        }
    
    let Optional<'V> (valcodec: ValueCodec<'V>) (tag: int) : FieldCodec<'V option> =
        let calcFieldSize (value: 'V option) =
            match value with
            | None -> 0
            | Some value ->
                Writer.ComputeInt32Size(tag <<< 3) +
                valcodec.CalcSize value
        let writeField (writer: Writer) (value: 'V option) =
            match value with
            | None -> ()
            | Some value ->
                valcodec.WriteTag writer tag
                valcodec.WriteValue writer value
        {
            CalcFieldSize = calcFieldSize
            WriteField = writeField
            GetDefault = defer None
        }
    
    let Repeated (valcodec: ValueCodec<'T>) (tag: int) : FieldCodec<'T seq> =
        match valcodec.RepeatEncoding with
        | Packed _ -> failwith "Invalid use of FieldCodec.Repeated for Packed field"
        | Repeat -> ()
        // calculate the size of an item that is emitted as an entire field (i.e. RepeateEncoding.Repeat)
        let itemFieldSize (tagSize: int) (value: 'T) =
            let dataSize = valcodec.CalcSize value
            tagSize + dataSize

        let sizeOfRepeated (value: 'T seq) =
            if value |> Seq.isEmpty then
                0
            else
                let tagSize = Writer.ComputeTagSize(tag)
                value |> Seq.sumBy (itemFieldSize tagSize)

        let writeRepeated (writer: Writer) (value: 'T seq) =
            if not (value |> Seq.isEmpty) then
                for v in value do
                    valcodec.WriteTag writer tag
                    valcodec.WriteValue writer v
        {
            CalcFieldSize = sizeOfRepeated
            WriteField = writeRepeated
            GetDefault = defer (seq [])
        }
    
    type MapRecord<'K,'V> = ('K * 'V)
    let Map (keycodec: ValueCodec<'K>) (valcodec: ValueCodec<'V>) (tag: int) : FieldCodec<FSharp.Collections.Map<'K,'V>> =
        let recordCodec = ValueCodec.MapRecord keycodec valcodec
        let sizeOfMap (map: FSharp.Collections.Map<'K, 'V>) =
            if map |> Collections.Map.isEmpty then
                0
            else
                let tagSize = Writer.ComputeTagSize(tag)
                let itemSize (item: System.Collections.Generic.KeyValuePair<'K,'V>) =
                    let dataSize = recordCodec.CalcSize (item.Key, item.Value);
                    tagSize + dataSize
                let size = map |> Seq.sumBy itemSize
                size
        let writeMap (writer: Writer) (map: FSharp.Collections.Map<'K, 'V>) =
            if not (map |> Collections.Map.isEmpty) then
                for v in map do
                    recordCodec.WriteTag writer tag
                    recordCodec.WriteValue writer (v.Key, v.Value)
        {
            CalcFieldSize = sizeOfMap
            WriteField = writeMap
            GetDefault = defer FSharp.Collections.Map.empty
        }
    
[<Struct>]
type RepeatedBuilderState<'T> =
| Empty
| Prebuilt of array: 'T array
| Building of list: System.Collections.Generic.List<'T>

[<Struct>]
type RepeatedBuilder<'T> =
    val mutable list: RepeatedBuilderState<'T>
    member x.Add (value: 'T) =
        let list =
            match x.list with
            | Empty ->
                let newList = new System.Collections.Generic.List<'T>()
                x.list <- Building newList
                newList
            | Prebuilt a ->
                let asList = new System.Collections.Generic.List<'T>(a)
                x.list <- Building asList
                asList
            | Building list ->
                list
        list.Add value
    member x.AddRange (values: 'T seq) =
        match x.list, values with
        | Empty, (:? array<'T> as prebuilt) ->
            x.list <- Prebuilt prebuilt
        | Empty, values ->
            x.list <- Prebuilt (values |> Seq.toArray)
        | Prebuilt a, _ ->
            let asList = new System.Collections.Generic.List<'T>(a)
            x.list <- Building asList
            asList.AddRange values
        | Building list, _ ->
            list.AddRange values
    member x.Build : 'T seq =
        match x.list with
        | Empty -> seq []
        | Prebuilt array ->
            array
        | Building list ->
            let array = list.ToArray ()
            array

[<Struct>]
type MapBuilder<'K, 'V when 'K : comparison> =
    val mutable list: ValueOption<System.Collections.Generic.List<'K * 'V>>
    member x.Add (value: 'K * 'V) =
        let list = x.list
        let list =
            match list with
            | ValueNone ->
                let newList = new System.Collections.Generic.List<'K * 'V>()
                x.list <- ValueSome newList
                newList
            | ValueSome list ->
                list
        list.Add value
    member x.Build : Map<'K,'V> =
        match x.list with
        | ValueNone -> Map.empty
        | ValueSome list ->
            Map.ofSeq list

[<Struct>]
type BytesBuilder =
    val mutable value: Google.Protobuf.ByteString ValueOption
    member x.Set (value: Google.Protobuf.ByteString) =
        x.value <-
            match value.Length with
            | 0 -> ValueNone
            | _ -> ValueSome value
    member x.Build : Google.Protobuf.ByteString =
        match x.value with
        | ValueSome v -> v
        | ValueNone -> Google.Protobuf.ByteString.Empty

[<Struct>]
type OptionBuilder<'T> =
    val mutable value: ValueOption<'T>
    member x.Set (value: 'T) =
        x.value <- ValueSome value
    member x.Build : 'T option =
        match x.value with
        | ValueSome v -> Some v
        | ValueNone -> None

let orEmptyString (v: string) =
    match v with
    | null -> ""
    | v -> v

