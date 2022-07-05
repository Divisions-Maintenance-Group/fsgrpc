namespace FsGrpc
open System.Text.Json.Serialization
open System.Text.Json
open System
open System.Runtime.CompilerServices

[<Struct;IsReadOnlyAttribute;CustomEquality;NoComparison;StructuredFormatDisplay("Bytes: {Base64}")>]
[<JsonConverter(typeof<BytesConverter>)>]
type Bytes(mem: Google.Protobuf.ByteString) =
    member internal _.ByteString = match mem with | null -> Google.Protobuf.ByteString.Empty | mem -> mem
    member x.Data = x.ByteString.Memory
    member x.Length = x.ByteString.Length
    member x.Base64 = x.ByteString.ToBase64()
    member _.Equals (other: Bytes) =
        match mem with
        | null -> other.Length = 0
        | mem -> mem.Equals(other.ByteString)
    static member Empty = Bytes Google.Protobuf.ByteString.Empty
    static member FromUtf8 (utf8: string) = match utf8.Length with | 0 -> Bytes.Empty | _ -> Bytes (Google.Protobuf.ByteString.CopyFromUtf8 utf8)
    static member FromBase64 (b64: string) = match b64.Length with | 0 -> Bytes.Empty | _ -> Bytes (Google.Protobuf.ByteString.FromBase64 b64)
    static member CopyFrom (bytes: byte array) = match bytes.Length with | 0 -> Bytes.Empty | _ -> Bytes (Google.Protobuf.ByteString.CopyFrom bytes)
    override x.Equals (other: obj) =
        match other with
        | :? Bytes as b -> x.Equals b
        | _ -> false
    override x.GetHashCode () =
        x.ByteString.GetHashCode()
    interface IEquatable<Bytes> with
        member x.Equals(other: Bytes) = x.Equals other
and BytesConverter() =
    inherit JsonConverter<Bytes>()
    override _.Read (reader: byref<Utf8JsonReader>, typeToConvert: Type, options: JsonSerializerOptions): Bytes =
        failwith "Not Implemented"
    override _.Write(writer: Utf8JsonWriter, value: Bytes, options: JsonSerializerOptions): unit =
        writer.WriteBase64StringValue value.Data.Span
