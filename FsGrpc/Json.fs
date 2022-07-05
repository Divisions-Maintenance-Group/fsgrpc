module FsGrpc.Json

open System.Text.Json.Serialization
open Microsoft.FSharp.Reflection
open System.Text.Json
open System
open Protobuf

let inline tryCastTo<'T> (a: obj) : 'T option =
    match a with
    | :? 'T -> Some (a :?> 'T)
    | _ -> None

type OneofConverter<'U>() =
    inherit JsonConverter<'U>()
    let getTag = FSharpValue.PreComputeUnionTagReader typeof<'U>
    let cases = FSharpType.GetUnionCases typeof<'U>
    let jsonNameOfCase (case: UnionCaseInfo) =
        let jsonNameAttr = case.GetCustomAttributes() |> Seq.tryPick tryCastTo<JsonPropertyNameAttribute>
        match jsonNameAttr with
        | Some attr -> attr.Name
        | None -> case.Name
    let makeCaseWriter (case: UnionCaseInfo) =
        let fields = case.GetFields()
        let name = jsonNameOfCase case
        match fields.Length with
        | 0 ->
            let doWrite (writer: Utf8JsonWriter) (_: obj) (_: JsonSerializerOptions) =
                writer.WriteNullValue()
            doWrite
        | _ ->
            let field = fields[0]
            let reader = FSharpValue.PreComputeUnionReader case
            let doWrite (writer: Utf8JsonWriter) (value: obj) (options: JsonSerializerOptions) =
                writer.WriteStartObject()
                writer.WritePropertyName name
                let values = reader value
                JsonSerializer.Serialize (writer, values[0], field.PropertyType, options)
                writer.WriteEndObject()
            doWrite
    let doWrite = cases |> Array.map makeCaseWriter
    override _.Read (reader: byref<Utf8JsonReader>, typeToConvert: Type, options: JsonSerializerOptions): 'U =
        failwith "Not Implemented"
    override _.Write(writer: Utf8JsonWriter, value: 'U, options: JsonSerializerOptions): unit =
        let tag = getTag value
        let doWrite = doWrite[tag]
        doWrite writer value options

let inline private writeInt (n: int) (w: Utf8JsonWriter) = w.WriteNumberValue n
let inline private writeString (s: string) (w: Utf8JsonWriter) = w.WriteStringValue s

let private enumValWriter (e: Type) (render: JsonEnumStyle) (name: string, number: int) =
    match render with
    | JsonEnumStyle.Number ->
        writeInt number
    | JsonEnumStyle.Name ->
        writeString name
    | JsonEnumStyle.ProtobufName ->
        let memInfo = e.GetMember(name) |> Seq.find (fun m -> m.DeclaringType = e)
        let protoNameAttr = memInfo.GetCustomAttributes(false) |> Seq.tryPick tryCastTo<ProtobufNameAttribute>
        match protoNameAttr with
        | None -> writeString name
        | Some attr -> writeString attr.Name

type EnumConverter<'E>(render: JsonEnumStyle) =
    inherit JsonConverter<'E>()
    let e = typeof<'E>
    // we now know the enum and the render option, so partially apply those
    let enumValWriter = enumValWriter e render
    // get all of the enum values as ints
    let numbers = (Enum.GetValues e) :?> 'E array |> Array.map unbox<int>
    // get the f# names of the values
    let names = Enum.GetNames e
    // zip them into tuples of (name, int)
    let pairs = Array.zip names numbers
    // we now can partially apply this to the writer
    let writers = pairs |> Array.map enumValWriter
    // now create a map of them, indexed by their number
    let writersByNumber = Array.zip numbers writers |> Map.ofSeq
    new() = EnumConverter<'E>(JsonEnumStyle.ProtobufName)
    override _.Read (reader: byref<Utf8JsonReader>, typeToConvert: Type, options: JsonSerializerOptions): 'E =
        failwith "Not Implemented"
    override _.Write(writer: Utf8JsonWriter, value: 'E, options: JsonSerializerOptions): unit =
        let writeTo = writersByNumber.Item (unbox<int> value)
        writeTo writer

type EnumConverter(render: JsonEnumStyle) =
    inherit System.Text.Json.Serialization.JsonConverterFactory()
    override this.CanConvert (typeToConvert: Type) =
        let isEnum = typeToConvert.IsEnum && typeToConvert.GetEnumUnderlyingType() = typeof<int>
        isEnum
    override this.CreateConverter (typeToConvert: Type, options: JsonSerializerOptions): JsonConverter = 
        let generic = typedefof<EnumConverter<_>>
        let concrete = generic.MakeGenericType(typeToConvert)
        let instance = Activator.CreateInstance(concrete, render)
        instance :?> JsonConverter

type MessageConverter<'M>(options: JsonOptions) =
    inherit JsonConverter<'M>()
    let proto = Protobuf.ReflectProtoOf<'M> ()
    let encode = proto.EncodeJson options
    override _.Read (reader: byref<Utf8JsonReader>, typeToConvert: Type, options: JsonSerializerOptions): 'M =
        failwith "Not Implemented"
    override _.Write(writer: Utf8JsonWriter, value: 'M, _: JsonSerializerOptions): unit =
        writer.WriteStartObject()
        encode writer value
        writer.WriteEndObject()

type MessageConverter(options: JsonOptions option) =
    inherit JsonConverterFactory()
    new() =
        MessageConverter (None)
    override this.CanConvert (typeToConvert: Type) =
        FSharpType.IsRecord typeToConvert && (typeToConvert.GetCustomAttributes(false) |> Seq.tryPick tryCastTo<MessageAttribute>).IsSome
    override this.CreateConverter (typeToConvert: Type, jso: JsonSerializerOptions): JsonConverter =
        let opts =
            match options with
            | None -> JsonOptions.FromJsonSerializerOptions jso
            | Some opts -> opts
        let generic = typedefof<MessageConverter<_>>
        let concrete = generic.MakeGenericType(typeToConvert)
        let instance = Activator.CreateInstance(concrete, opts)
        instance :?> JsonConverter

let Options = {|
    Default =
        let jso = JsonSerializerOptions(JsonSerializerDefaults.General, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault)
        jso.Converters.Add(new MessageConverter(Some JsonOptions.Proto3Defaults))
        jso
|}

let serializeWith (options: JsonSerializerOptions) (x: obj) =
    System.Text.Json.JsonSerializer.Serialize (x, options)

let serialize: obj -> string =
    serializeWith Options.Default