module ProtocGenFsgrpc.ProtoCodeGen
open ProtocGenFsgrpc.CodeTree
open ChangeCase
open Google.Protobuf
open ProtoGenFsgrpc.Model
open System.Text.RegularExpressions

[<Literal>]
let private bytesFsType = "Google.Protobuf.ByteString"

let private nsCombine (ns: string) (name: string) =
    match ns, name with
    | "", "" -> ""
    | "", name
    | name, "" -> name
    | ns, name -> $"{ns}.{name}"

type ProtoFieldType = FieldDescriptorProto.Type

let private toFsTypeName (protoName: string) =
    protoName

let private toFsNamespace (package: string) =
    package.Split [|'.'|] |> Seq.map toPascalCase |> String.concat "."

let private toRecordFieldName (protoName: string) =
    protoName |> toPascalCase

type NsNode =
| Message of (NsNode option * MessageDef)
| Enum of (NsNode option * EnumDef)
| File of FileDef
| Files of seq<FileDef>
with
    static member protoNameOf (node: NsNode) =
        match node with
        | Message (None, m) -> m.Name
        | Message (Some parent, m) when (NsNode.protoNameOf parent).Length = 0 -> m.Name
        | Message (Some parent, m) -> $"{NsNode.protoNameOf parent}.{m.Name}"
        | Enum (None, e) -> e.Name
        | Enum (Some parent, e) when (NsNode.protoNameOf parent).Length = 0 -> e.Name
        | Enum (Some parent, e) -> $"{NsNode.protoNameOf parent}.{e.Name}"
        | File f -> $".{f.Package}"
        | Files _ -> ""
    static member fsNameOf (node: NsNode) =
        match node with
        | Message (None, m) -> toPascalCase m.Name
        | Message (Some parent, m) when (NsNode.fsNameOf parent).Length = 0 -> m.Name
        | Message (Some parent, m) -> $"{NsNode.fsNameOf parent}.{m.Name}"
        | Enum (None, e) -> toPascalCase e.Name
        | Enum (Some parent, e) when (NsNode.fsNameOf parent).Length = 0 -> e.Name
        | Enum (Some parent, e) -> $"{NsNode.fsNameOf parent}.{e.Name}"
        | File f -> f.Package.Split [|'.'|] |> Seq.map toPascalCase |> String.concat "."
        | Files _ -> ""

type TypeInfo = {
    FsName: string
    Def: NsNode
}

type TypeMap = (string -> TypeInfo)
type CommentMap = (string -> string)

let private toFsEnumValueName (enumType: EnumDef) (value: EnumValueDef) =
    let prefix = toPascalCase enumType.Name
    let fullVal = toPascalCase value.Name
    if fullVal.StartsWith(prefix) then
        fullVal[prefix.Length..]
    else
        fullVal

let tryMinOr<'T when 'T : comparison> (defVal: 'T) (s: 'T seq) : 'T =
    let length = s |> Seq.length
    match length with
    | 0 -> defVal
    | _ -> s |> Seq.min

let renderComment comment : CodeNode =
    match comment with
    | "" -> Frag []
    | comment ->
        let comment = comment.Trim([|'\r'; '\n'|])
        let lines =
            Regex.Split(comment, @"\r\n|\r|\n")
            |> Seq.filter (fun line -> not (Regex.IsMatch (line, @"^\s*(\{[^\}]+\}\s*)+$")))
            |> List.ofSeq
        let paddingOf line =
            let m = Regex.Match (line, "^ *")
            match m.Success with
            | true -> m.Value.Length
            | false -> 0
        let padding =
            lines
            |> Seq.filter (System.String.IsNullOrWhiteSpace >> not)
            |> Seq.map paddingOf
            |> (tryMinOr 0)
        let removePadding (length: int) (line: string) =
            if line.Length < length then
                ""
            else
                line.Substring(length)
        match lines with
        | [] -> Frag []
        | [line] -> Line $"/// <summary>{line.Substring(padding)}</summary>"
        | lines -> Frag [
            Line "/// <summary>"
            Frag (lines |> Seq.map (fun line -> Line $"/// {removePadding padding line}"))
            Line "/// </summary>"
        ]

let commentFrom (sci: Sci option) =
    match sci with
    | None -> ""
    | Some sci ->
        let comment = sci.LeadingComments
        comment

let private toFsEnumValueDef (enumType: EnumDef) (value: EnumValueDef) =
    let name = toFsEnumValueName enumType value
    let number = value.Number
    Frag [
        renderComment (commentFrom value.Sci)
        Line $"| {name} = {number}"
    ]

[<RequireQualifiedAccess>]
type ValueType =
| Double
| Float
| Int64
| UInt64
| Int32
| Fixed64
| Fixed32
| Bool
| String
| Bytes
| UInt32
| SFixed32
| SFixed64
| SInt32
| SInt64
| Enum of TypeInfo
| Message of TypeInfo
| Packed of ValueType
| Wrap of ValueType
| Timestamp
| Duration
with
    static member From (typeMap: TypeMap) (proto: FieldDescriptorProto.Type) (typeName: string) =
        match proto with
        | ProtoFieldType.Double -> ValueType.Double
        | ProtoFieldType.Float -> ValueType.Float
        | ProtoFieldType.Int64 -> ValueType.Int64
        | ProtoFieldType.Uint64 -> ValueType.UInt64
        | ProtoFieldType.Int32 -> ValueType.Int32
        | ProtoFieldType.Fixed64 -> ValueType.Fixed64
        | ProtoFieldType.Fixed32 -> ValueType.Fixed32
        | ProtoFieldType.Bool -> ValueType.Bool
        | ProtoFieldType.String -> ValueType.String
        | ProtoFieldType.Bytes -> ValueType.Bytes
        | ProtoFieldType.Uint32 -> ValueType.UInt32
        | ProtoFieldType.Sfixed32 -> ValueType.SFixed32
        | ProtoFieldType.Sfixed64 -> ValueType.SFixed64
        | ProtoFieldType.Sint32 -> ValueType.SInt32
        | ProtoFieldType.Sint64 -> ValueType.SInt64
        | ProtoFieldType.Enum -> ValueType.Enum (typeMap typeName)
        | ProtoFieldType.Message ->
            match typeName with
            | ".google.protobuf.DoubleValue" -> ValueType.Wrap ValueType.Double
            | ".google.protobuf.FloatValue" -> ValueType.Wrap ValueType.Float
            | ".google.protobuf.Int64Value" -> ValueType.Wrap ValueType.Int64
            | ".google.protobuf.UInt64Value" -> ValueType.Wrap ValueType.UInt64
            | ".google.protobuf.Int32Value" -> ValueType.Wrap ValueType.Int32
            | ".google.protobuf.UInt32Value" -> ValueType.Wrap ValueType.UInt32
            | ".google.protobuf.BoolValue" -> ValueType.Wrap ValueType.Bool
            | ".google.protobuf.StringValue" -> ValueType.Wrap ValueType.String
            | ".google.protobuf.BytesValue" -> ValueType.Wrap ValueType.Bytes
            | ".google.protobuf.Timestamp" -> ValueType.Timestamp
            | ".google.protobuf.Duration" -> ValueType.Duration
            | other -> ValueType.Message (typeMap other)
        | _ -> failwith $"Don't know how to handle type {proto}"


type MapInfo = {
    Key: ValueType
    Value: ValueType
}

type FieldType =
| Primitive of ValueType
| Optional of ValueType
| Repeated of ValueType
| Map of MapInfo
| OneofOption of ValueType * string
with
    static member NameOf (ft: FieldType) =
        match ft with
        | Primitive _ -> "Primitive"
        | Optional _ -> "Optional"
        | Repeated _ -> "Repeated"
        | OneofOption _ -> "OneofOption"
        | Map _ -> "Map"
    static member From (typeMap: TypeMap) (oneofs: int -> OneofDef) (field: FieldDef) =
        (* NOTE about "repeated optional"
            it is not legal to use "optional" and "repeated" together so that you cannot do "repeated optional int32" for example
            this is because "optional" allows to distinguish between "not present" and "default value" but there is no way within the wire format to encode a repeated value that is "not present"
            so "repeated optional int32" is not allowed but "repeated google.protobuf.Int32Value" is allowed, so what should we do in this case?
            Note: there is no reason to construct a proto like this because "repeated google.protobuf.Int32Value" cannot be used any differently from "repeated int32", it just wastes space on the wire
                    Even the official C# generator makes a Repeated of int? but disallows at runtime adding a null value, which just makes it worse version of Repeated of int
            So we don't have to support it well, we only have to support it reasonably well
            And it should actually be fine to represent it in F# as int seq so long as it behaves on the wire like repeated Int32Value
        *)
        let typeCode = field.Type
        let typeName = field.TypeName
        let valueType = ValueType.From typeMap typeCode typeName
        let repeated = field.Label = FieldDescriptorProto.Label.Repeated
        let mapType = 
            match typeName with
            | "" -> None
            | typeInfo ->
                match typeMap typeInfo with
                | { FsName = _; Def = Message (_, m) } when (match m.Options with | Some {MapEntry = true} -> true | _ -> false) -> Some m
                | _ -> None
        let oneofOption = field.OneofIndex
        let optional = field.Proto3Optional
        let message = field.Type = ProtoFieldType.Message
        let fieldType = 
            match (mapType, oneofOption, repeated, optional, message) with
            | (Some mapType, _, _, _, _) ->
                let k = mapType.Fields |> Seq.find (fun f -> f.Number = 1)
                let v = mapType.Fields |> Seq.find (fun f -> f.Number = 2)
                let keyTypeCode = k.Type
                let keyTypeName = k.TypeName
                let valTypeCode = v.Type
                let valTypeName = v.TypeName
                let keyType = ValueType.From typeMap keyTypeCode keyTypeName
                let valType = ValueType.From typeMap valTypeCode valTypeName
                FieldType.Map { Key = keyType; Value = valType }
            | (None, None, true, _, _) ->
                let valueType =
                    match valueType with
                    | ValueType.Double
                    | ValueType.Float
                    | ValueType.Int64
                    | ValueType.UInt64
                    | ValueType.Int32
                    | ValueType.Fixed64
                    | ValueType.Fixed32
                    | ValueType.Bool
                    | ValueType.UInt32
                    | ValueType.SFixed32
                    | ValueType.SFixed64
                    | ValueType.SInt32
                    | ValueType.SInt64
                    | ValueType.Enum _ ->
                        ValueType.Packed valueType
                    | _ ->
                        valueType
                match valueType with
                | ValueType.Packed _ ->
                    FieldType.Primitive valueType
                | _ ->
                    FieldType.Repeated valueType
            | (None, Some index, _, false, _) ->
                FieldType.OneofOption (valueType, (oneofs index).Name)
            | (None, _, false, _, true) -> FieldType.Optional valueType
            | (None, _, false, true, _) -> FieldType.Optional valueType
            | _ -> FieldType.Primitive valueType
        fieldType

let rec private valueCodecExpr (vt: ValueType) : string =
    match vt with
    | ValueType.Enum {FsName = e} -> $"ValueCodec.Enum<{e}>"
    | ValueType.Message {FsName = m} -> $"ValueCodec.Message<{m}>"
    | ValueType.Wrap w -> $"(ValueCodec.Wrap {valueCodecExpr w})"
    | ValueType.Packed i -> $"(ValueCodec.Packed {valueCodecExpr i})"
    | basic -> $"ValueCodec.{basic}"

let rec private fsValueTypeOf (valueType: ValueType) =
    match valueType with
    | ValueType.Double -> "double"
    | ValueType.Float -> "float32"
    | ValueType.Int64 -> "int64"
    | ValueType.UInt64 -> "uint64"
    | ValueType.Int32 -> "int"
    | ValueType.Fixed64 -> "uint64"
    | ValueType.Fixed32 -> "uint"
    | ValueType.Bool -> "bool"
    | ValueType.String -> "string"
    | ValueType.Bytes -> bytesFsType
    | ValueType.UInt32 -> "uint32"
    | ValueType.SFixed32 -> "int"
    | ValueType.SFixed64 -> "int64"
    | ValueType.SInt32 -> "int"
    | ValueType.SInt64 -> "int64"
    | ValueType.Enum {FsName = name} -> $"%s{name}"
    | ValueType.Message {FsName = name} -> $"%s{name}"
    | ValueType.Wrap vt -> $"%s{fsValueTypeOf vt}"
    | ValueType.Packed vt -> $"%s{fsValueTypeOf vt} seq"
    | ValueType.Timestamp -> "NodaTime.Instant"
    | ValueType.Duration -> "NodaTime.Duration"

type FsBuilderType = {
    Name: string
    BuildExprPattern: string
    PutExprPattern: string
}

let rec private fsBuilderTypeOf (valueType: ValueType) =
    match valueType with
    | ValueType.Double ->
        { Name = "double"; BuildExprPattern = "{0}"; PutExprPattern = " <- {0}.ReadValue reader" }
    | ValueType.Float ->
        { Name = "float32"; BuildExprPattern = "{0}"; PutExprPattern = " <- {0}.ReadValue reader" }
    | ValueType.Int64 ->
        { Name = "int64"; BuildExprPattern = "{0}"; PutExprPattern = " <- {0}.ReadValue reader" }
    | ValueType.UInt64 ->
        { Name = "uint64"; BuildExprPattern = "{0}"; PutExprPattern = " <- {0}.ReadValue reader" }
    | ValueType.Int32 ->
        { Name = "int"; BuildExprPattern = "{0}"; PutExprPattern = " <- {0}.ReadValue reader" }
    | ValueType.Fixed64 ->
        { Name = "uint64"; BuildExprPattern = "{0}"; PutExprPattern = " <- {0}.ReadValue reader" }
    | ValueType.Fixed32 ->
        { Name = "uint"; BuildExprPattern = "{0}"; PutExprPattern = " <- {0}.ReadValue reader" }
    | ValueType.Bool ->
        { Name = "bool"; BuildExprPattern = "{0}"; PutExprPattern = " <- {0}.ReadValue reader" }
    | ValueType.String ->
        { Name = "string"; BuildExprPattern = "{0} |> orEmptyString"; PutExprPattern = " <- {0}.ReadValue reader" }
    | ValueType.Bytes ->
        { Name = "BytesBuilder"; BuildExprPattern = "{0}.Build"; PutExprPattern = ".Set ({0}.ReadValue reader)" }
    | ValueType.UInt32 ->
        { Name = "uint32"; BuildExprPattern = "{0}"; PutExprPattern = " <- {0}.ReadValue reader" }
    | ValueType.SFixed32 ->
        { Name = "int"; BuildExprPattern = "{0}"; PutExprPattern = " <- {0}.ReadValue reader" }
    | ValueType.SFixed64 ->
        { Name = "int64"; BuildExprPattern = "{0}"; PutExprPattern = " <- {0}.ReadValue reader" }
    | ValueType.SInt32 ->
        { Name = "int"; BuildExprPattern = "{0}"; PutExprPattern = " <- {0}.ReadValue reader" }
    | ValueType.SInt64 ->
        { Name = "int64"; BuildExprPattern = "{0}"; PutExprPattern = " <- {0}.ReadValue reader" }
    | ValueType.Enum {FsName = name} ->
        { Name = $"%s{name}"; BuildExprPattern = "{0}"; PutExprPattern = " <- {0}.ReadValue reader" }
    | ValueType.Message {FsName = name} ->
        { Name = $"%s{name}"; BuildExprPattern = "{0}"; PutExprPattern = " <- {0}.ReadValue reader" }
    | ValueType.Wrap vt ->
        { Name = $"%s{fsValueTypeOf vt}"; BuildExprPattern = "{0}"; PutExprPattern = " <- {0}.ReadValue reader" }
    | ValueType.Packed vt ->
        { Name = $"%s{fsValueTypeOf vt}"; BuildExprPattern = "{0}.Build"; PutExprPattern = ".AddRange ({0}.ReadValue reader)" }
    | ValueType.Timestamp ->
        { Name = "NodaTime.Instant"; BuildExprPattern = "{0}"; PutExprPattern = " <- {0}.ReadValue reader" }
    | ValueType.Duration ->
        { Name = "NodaTime.Duration"; BuildExprPattern = "{0}"; PutExprPattern = " <- {0}.ReadValue reader" }

let private fsTypeOf (fieldType: FieldType) =
    let fsTypeName =
        match fieldType with
        | Primitive vt ->
            $"%s{fsValueTypeOf vt}"
        | Optional vt ->
            $"%s{fsValueTypeOf vt} option"
        | Repeated vt ->
            $"%s{fsValueTypeOf vt} seq"
        | OneofOption (vt, _) ->
            $"%s{fsValueTypeOf vt}"
        | Map { Key = k; Value = v } ->
            $"Map<%s{fsValueTypeOf k}, %s{fsValueTypeOf v}>"
    fsTypeName

let private fsBuilderOf (fieldType: FieldType) : FsBuilderType =
    let fsTypeName =
        match fieldType with
        | Primitive (ValueType.Packed p) ->
            { Name = $"FsGrpc.RepeatedBuilder<%s{fsValueTypeOf p}>"; BuildExprPattern = "{0}.Build"; PutExprPattern = ".AddRange ({0}.ReadValue reader)" }
        | Primitive vt ->
            fsBuilderTypeOf vt
        | Optional vt ->
            { Name = $"OptionBuilder<%s{fsValueTypeOf vt}>"; BuildExprPattern = "{0}.Build"; PutExprPattern = ".Set ({0}.ReadValue reader)" }
        | Repeated vt ->
            { Name = $"FsGrpc.RepeatedBuilder<%s{fsValueTypeOf vt}>"; BuildExprPattern = "{0}.Build"; PutExprPattern = ".Add ({0}.ReadValue reader)" }
        | OneofOption (vt, _) ->
            { Name = $"%s{fsValueTypeOf vt}"; BuildExprPattern = "{0}.Build"; PutExprPattern = " <- XXX3{0}.ReadValue reader" }
        | Map { Key = k; Value = v } ->
            { Name = $"MapBuilder<%s{fsValueTypeOf k}, %s{fsValueTypeOf v}>"; BuildExprPattern = "{0}.Build"; PutExprPattern = " <- XXX4{0}.ReadValue reader" }
    fsTypeName

type FieldInfo = {
    FsName: string
    Comment: string
    FsTypeName: string
    FsBuilder: FsBuilderType
    FieldType: FieldType
    Tag: int
}

type OneofInfo = {
    FsName: string
    Comment: string
    Index: int
    Options: FieldInfo list
}

type MemberType =
| Field of FieldInfo
| Oneof of OneofInfo

let private fieldInfoFrom (typeMap: TypeMap) (oneofs: int -> OneofDef) (field: FieldDef) : FieldInfo =
    let fieldType = FieldType.From typeMap oneofs field
    let fsTypeName = fsTypeOf fieldType
    let fsBuilder = fsBuilderOf fieldType
    {
        FsName = toRecordFieldName field.Name
        Comment = commentFrom field.Sci
        FsTypeName = fsTypeName
        FsBuilder = fsBuilder
        FieldType = fieldType
        Tag = field.Number
    }

let private toFsRecordFieldDef (field: FieldInfo) : CodeNode =
    let { FieldType = fieldType; Tag = tag } = field
    let fieldCodecExpr =
        match fieldType with
        | Primitive vt
        | Optional vt
        | Repeated vt ->
            let fieldCodecName = FieldType.NameOf fieldType
            let valueCodec = valueCodecExpr vt
            $"FieldCodec.%s{fieldCodecName} %s{valueCodec}"
        | OneofOption (vt, oneof) ->
            let valueCodec = valueCodecExpr vt
            $"FieldCodec.Optional %s{valueCodec} (* oneof {oneof} *)"
        | Map { Key = k; Value = v } ->
            let vk = valueCodecExpr k
            let vv = valueCodecExpr v
            $"FieldCodec.Map %s{vk} %s{vv}"
    Frag [
        Line $"let %s{field.FsName} = %s{fieldCodecExpr} %d{tag}"
    ]

let private toFsRecordFieldDecl (recordType: string) (field: MemberType) : CodeNode =
    match field with
    | Field field ->
        let fsTypeName = field.FsTypeName
        let tag = field.Tag
        Frag [
        (renderComment field.Comment)
        Line $"%s{field.FsName}: %s{fsTypeName} // (%d{tag})"
        ]
    | Oneof oneof ->
        Frag [
        (renderComment oneof.Comment)
        Line $"%s{oneof.FsName}: %s{recordType}.%s{oneof.FsName}Case"
        ]

let private toFsEnumDef (protoEnumDef: EnumDef) : CodeNode =
    let fsName = toFsTypeName protoEnumDef.Name
    let options = protoEnumDef.Values |> Seq.map (toFsEnumValueDef protoEnumDef)
    Frag [
    Line $""
    renderComment (commentFrom protoEnumDef.Sci)
    Line $"type {fsName} ="
    Frag options
    ]

let private toFsRecordFieldDefault (ns: string) (field: MemberType) : CodeNode =
    match field with
    | Field field ->
        let {FsName = name; FieldType = _} = field
        Line $"%s{name} = {name}.GetDefault()"
    | Oneof oneof ->
        let name = oneof.FsName
        let fqName = nsCombine ns name
        Line $"%s{name} = {fqName}Case.None"

let private toFsRecordFieldSize (ns: string) (field: MemberType) : CodeNode =
    match field with
    | Field field ->
        let {FsName = name; FieldType = _} = field
        Line $"+ %s{name}.CalcFieldSize m.{name}"
    | Oneof oneof ->
        let name = oneof.FsName
        let unionName = $"%s{name}Case"
        let unionFqName = nsCombine ns unionName
        Frag [
        Line $"+ match m.{name} with"
        Block [
            Line $"| {unionFqName}.None -> 0"
            Frag (oneof.Options |> Seq.map (fun opt -> Line $"| {unionFqName}.{opt.FsName} v -> {opt.FsName}.CalcFieldSize (Some v)"))
            ]
        ]

let toFsRecordFieldWrite (ns: string) (field: MemberType) : CodeNode =
    match field with
    | Field field ->
        let {FsName = name; FieldType = _} = field
        Line $"%s{name}.WriteField w m.{name}"
    | Oneof oneof ->
        let name = oneof.FsName
        let unionName = $"%s{name}Case"
        let unionFqName = nsCombine ns unionName
        Frag [
        Line $"(match m.{name} with"
        Line $"| {unionFqName}.None -> ()"
        Frag (oneof.Options |> Seq.map (fun opt -> Line $"| {unionFqName}.{opt.FsName} v -> {opt.FsName}.WriteField w (Some v)"))
        Line $")"
        ]

let private toProtoDefImpl (ns: string) (protoTypeName: string) (fsTypeName: string) (fieldModel: MemberType seq) : CodeNode =
    let count = fieldModel |> Seq.length

    let emptyImpl =
        match count with
        | 0 ->
            Line $"Empty = %s{fsTypeName}.empty"
        | _ ->
            let ns = nsCombine ns fsTypeName
            Frag [
            Line $"Empty = {{"
            Block [
                Frag (fieldModel |> Seq.map (toFsRecordFieldDefault ns))
                Line $"}}"
                ]
            ]

    let nsqualifier = nsCombine ns fsTypeName

    Frag [
    Line $"{{ // ProtoDef<%s{fsTypeName}>"
    Block [
        Line $"Name = \"%s{protoTypeName}\""
        emptyImpl
        Line $"Size = fun (m: %s{fsTypeName}) ->"
        Block [
            Line "0"
            Frag (fieldModel |> Seq.map (toFsRecordFieldSize nsqualifier))
            ]
        Line $"Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: %s{fsTypeName}) ->"
        Block [
            match count with
            | 0 -> Line "()"
            | _ -> Frag (fieldModel |> Seq.map (toFsRecordFieldWrite nsqualifier))
            ]
        Line $"Decode = fun (r: Google.Protobuf.CodedInputStream) ->"
        Block [
            match count with
            | 0 ->
                Frag [
                Line $"let mutable tag = 0"
                Line $"while read r &tag do"
                Block [
                    Line $"r.SkipLastField()"
                ]
                Line $"{fsTypeName}.empty"
                ]
            | _ ->
                Frag [
                Line $"let mutable builder = new %s{nsqualifier}.Builder()"
                Line $"let mutable tag = 0"
                Line $"while read r &tag do"
                Block [
                    Line $"builder.Put (tag, r)"
                ]
                Line $"builder.Build"
                ]
            ]
        Line $"}}"
        ]
    ]

let recordMembers (typeMap: TypeMap) (oneofs: OneofDef seq) (fields: FieldDef seq) : MemberType seq =
    // in the proto definition that we get, the fields of the oneof are flattened and we want to unflatten them here
    // which means replacing the first option with the oneof that contains all of the other options, and then omitting the rest of the options
    let oneofs =
        let map = oneofs |> Seq.mapi (fun index oneof -> (index, oneof)) |> Map.ofSeq
        (fun index -> map[index])
    let fieldInfoFrom = fieldInfoFrom typeMap oneofs
    let fields = fields |> Seq.rev
    let group (members: MemberType list) (field: FieldDef) : MemberType list =
        let oneof =
            match field.OneofIndex, field.Proto3Optional with
            | Some oneof, false -> Some oneof
            | _ -> None
        let field = fieldInfoFrom field
        let memb =
            match oneof with
            | None -> Field field
            | Some oneof ->
                let index = oneof
                let oneof = oneofs index
                Oneof {
                    FsName = toRecordFieldName oneof.Name
                    Comment = (commentFrom oneof.Sci)
                    Index = index
                    Options = [field]
                }
        let members =
            match members with
            | [] -> [memb]
            | head :: tail ->
                match head, memb with
                | Oneof prev, Oneof next when prev.Index = next.Index ->
                    let combined = Oneof { prev with Options = next.Options @ prev.Options }
                    let nextHead = combined
                    nextHead :: tail
                | _ -> memb :: members
        members
    let members = fields |> Seq.fold group []
    members

let private toOneofOptionDef (option: FieldInfo) =
    Frag [
        renderComment option.Comment
        Line $"| {option.FsName} of {option.FsTypeName}"
    ]

let private toOneofUnionDefs (oneof: OneofInfo) =
    let optionDefs = oneof.Options |> Seq.map toOneofOptionDef
    Frag [
        Line ""
        Line $"[<RequireQualifiedAccess>]"
        Line $"type {oneof.FsName}Case ="
        Line $"| None"
        Frag optionDefs
    ]

let private isMapType (messageType: MessageDef) =
    match messageType.Options with
    | Some {MapEntry = true} -> true
    | _ -> false

let private md5_32 (data : string) : int =
    let data = System.Text.Encoding.UTF8.GetBytes data
    use md5 = System.Security.Cryptography.MD5.Create()
    let hash =
        md5.ComputeHash(data)
        |> Seq.take 4
        |> Seq.fold (fun n b -> n + (n <<< 8) + (int b)) 0
    hash

let toBuilderField (recordType: string) (m: MemberType) : CodeNode =
    match m with
    | MemberType.Oneof oneof ->
        Line $"val mutable %s{oneof.FsName}: OptionBuilder<%s{recordType}.%s{oneof.FsName}Case>"
    | MemberType.Field field ->
        let fsBuilderName = field.FsBuilder.Name
        let tag = field.Tag
        Line $"val mutable %s{field.FsName}: %s{fsBuilderName} // ({tag})"

let toBuilderPut (f: FieldInfo) : CodeNode =
    let expr =
        match f.FieldType with
        | Primitive vt
        | Optional vt
        | Repeated vt ->
            let vc = valueCodecExpr vt
            let putExpr = System.String.Format(f.FsBuilder.PutExprPattern, vc)
            $"x.%s{f.FsName}{putExpr}"
        | OneofOption (a, unionName) ->
            let fsName = toRecordFieldName unionName
            let vc = valueCodecExpr a
            $"x.{fsName}.Set ({fsName}Case.%s{f.FsName} ({vc}.ReadValue reader))"
        | Map map ->
            let kc = valueCodecExpr map.Key
            let vc = valueCodecExpr map.Value
            $"x.%s{f.FsName}.Add ((ValueCodec.MapRecord {kc} {vc}).ReadValue reader)"
    Line $"| %d{f.Tag} -> %s{expr}"

let toBuilderInit (m: MemberType) : CodeNode =
    match m with
    | MemberType.Oneof oneof ->
        Line $"%s{oneof.FsName} = x.%s{oneof.FsName}.Build |> (Option.defaultValue {oneof.FsName}Case.None)"
    | MemberType.Field field ->
        let builder = fsBuilderOf field.FieldType
        let fieldExpr = $"x.%s{field.FsName}"
        let builderExpr = System.String.Format(builder.BuildExprPattern, fieldExpr)
        Line $"%s{field.FsName} = {builderExpr}"

let rec private toFsRecordDef (typeMap: TypeMap) (protoNs: string) (protoMessageDef: MessageDef) : CodeNode =
    let protoName = protoMessageDef.Name
    let fsName = toFsTypeName protoName
    let fsNs = toFsNamespace protoNs
    let fsFqName = $"%s{fsNs}.%s{fsName}"
    // members contains all fields where oneofs are a single record
    let members = recordMembers typeMap protoMessageDef.OneofDecls protoMessageDef.Fields
    // fields contains all fields where oneofs are broken out into their options
    let fields = members |> Seq.collect (fun m ->
        match m with
        | Oneof {Options = list} -> list
        | Field field -> [field]
        )
    let fieldDeclarations = members |> Seq.map (toFsRecordFieldDecl fsFqName)
    let fieldDefinitions = fields |> Seq.map toFsRecordFieldDef
    let protoDefImpl = toProtoDefImpl fsNs protoName fsName members

    let oneofUnions = members |> Seq.choose (fun m -> match m with | MemberType.Oneof oneof -> Some oneof | _ -> None)
    let oneofUnionDefs = oneofUnions |> Seq.map toOneofUnionDefs
    let nestedTypes = protoMessageDef.NestedTypes |> Seq.filter (isMapType >> not) |> Seq.map (toFsRecordDef typeMap $"{protoNs}.{protoName}")
    let nestedEnums = protoMessageDef.EnumTypes |> Seq.map toFsEnumDef

    let builderFields = members |> Seq.map (toBuilderField fsFqName)
    let builderPuts = fields |> Seq.map toBuilderPut
    let builderTakes = members |> Seq.map toBuilderInit

    let moduleDef =
        Frag [
        Line $""
        Line $"[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]"
        Line $"module {fsName} ="
        Block [
            Frag oneofUnionDefs
            Frag nestedTypes
            Frag nestedEnums
            Line $""
            Line $"[<System.Runtime.CompilerServices.IsByRefLike>]"
            Line $"type Builder ="
            Block [
                Line $"struct"
                Block [
                    Frag builderFields
                ]
                Line $"end"
                Line $"with"
                Line $"member x.Put ((tag, reader): int * Reader) ="
                Block [
                    Line $"match tag with"
                    Frag builderPuts
                    Line $"| _ -> reader.SkipLastField()"
                ]
                match (builderTakes |> Seq.length) with
                | 0 -> Line $"member x.Build = _%s{fsName}Proto.Empty"
                | _ ->
                    Line $"member x.Build : %s{fsFqName} = {{"
                    Block [
                        Frag builderTakes
                        Line "}"
                    ]
            ]
        ]
        ]

    let memberCount = members |> Seq.length

    Frag [
    moduleDef
    Line ""
    Line $"let private _{fsName}Proto : ProtoDef<{fsName}> ="
    Block [
        Line "// Field Definitions"
        Frag fieldDefinitions
        Line "// Proto Definition Implementation"
        protoDefImpl
    ]
    match memberCount with
    | 0 ->
        Frag[
            Line $"type {fsName} private() ="
            Block [
                Line $"override _.Equals other : bool = other :? {fsName}"
                Line $"override _.GetHashCode() : int = {md5_32 fsFqName}"
                Line $"static member empty = new {fsName}()"
            ]
        ]
    | _ ->
        Frag[
        renderComment (commentFrom protoMessageDef.Sci)
        Line $"type {fsName} = {{"
        Block [
            Line "// Field Declarations"
            Frag fieldDeclarations
            Line $"}}"
            Line "with"
            Line $"static member empty = _%s{fsName}Proto.Empty"
            Line $"static member Proto = lazy _%s{fsName}Proto"
            ]
        ]
    ]

let private toFsRecordDefs (typeMap: TypeMap) (protoNs: string) (protoMessageDefs: MessageDef seq) (protoEnumDefs: EnumDef seq) : CodeNode =
    Frag [
    Frag (protoEnumDefs |> Seq.map toFsEnumDef)
    Frag (protoMessageDefs |> Seq.map (toFsRecordDef typeMap protoNs))
    ]

let private getComments (scinfo: SourceCodeInfo option) : Map<string, string> =
    let locations =
        match scinfo with
        | Some {Location = locations} -> locations
        | _ -> seq []
    let records = locations |> Seq.map (fun location ->
        let path = location.Paths |> Seq.map (fun s -> $"/{s}") |> String.concat ""
        let leadingComments = location.LeadingComments.Trim()
        match leadingComments with
        | "" | null -> None
        | _ -> Some (path, leadingComments)
        )
    records |> Seq.choose id |> Map.ofSeq

let rec private nsDescendants (node: NsNode) =
    let children =
        match node with
        | Message (_, parent) ->
            Seq.concat [
                (parent.NestedTypes |> Seq.map (fun child -> Message (Some node, child)))
                (parent.EnumTypes |> Seq.map (fun child -> Enum (Some node, child)))
            ]
        | File f ->
            Seq.concat [
                (f.MessageTypes |> Seq.map (fun child -> Message (Some node, child)))
                (f.EnumTypes |> Seq.map (fun child -> Enum (Some node, child)))
            ]
        | Enum _ ->
            seq [] // enums can't have children
        | Files f -> f |> Seq.map File
    let descendants = children |> Seq.map nsDescendants |> Seq.collect id
    Seq.concat [children; descendants]

let createTypeMap (files: FileDef seq) : TypeMap =
    let leafToMapping nsnode =
        match nsnode with
        | Message _ | Enum _ ->
            let protoName = NsNode.protoNameOf nsnode
            let fsName = NsNode.fsNameOf nsnode
            let typeInfo = {
                FsName = fsName
                Def = nsnode
            }
            Some (protoName, typeInfo)
        | File _ | Files _ ->
            None
    let nsNodes = Files files |> nsDescendants |> Seq.choose leafToMapping
    let map = nsNodes |> Map.ofSeq
    let find protoName =
        match map.TryFind(protoName) with
        | Some typeInfo -> typeInfo
        | None ->
            failwith $"ERROR: Could not find type info for {protoName}"
    find

let private toFsNamespaceDecl (package: string) =
    Frag [
    Line $"[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]"
    Line $"module rec {toFsNamespace package}"
    Line $"open FsGrpc"
    Line $"#nowarn \"40\"" // TODO: need to see if we can eliminate this, possibly by having the implementation of the field writes be inlined by the generator itself
    ]

let private render = createRenderer "    "

let private toCompileInclude filename =
    Line $"""<Compile Include="$(MSBuildThisFileDirectory)/{filename}.gen.fs" />"""


let private toTargetsFile (files: FileDef seq) : CodeNode =
    let nameOf (file: FileDef) = file.Name
    let tupleByName file = (nameOf file, file)
    let filesByName =
        files
        |> Seq.map tupleByName
        |> Map.ofSeq

    let depsOf (filename: string) : string seq =
        let result = filesByName.TryFind(filename)
        match result with
        | None -> []
        | Some file -> file.Dependencies
    
    let depSort = DependencySort.depSort depsOf

    let inImportOrder = files |> Seq.map nameOf |> Seq.sortBy depSort
    let includes = inImportOrder |> Seq.map toCompileInclude
    Frag [
    Line $"""<?xml version="1.0"?>"""
    Line $"""<Project xmlns="http://schemas.microsoft.com/developer/msbuild/2003">"""
    Line $"""<ItemGroup>"""
    Block [
        Line $"""<!-- These files are listed in dependency order such that none is listed above any other file on which it depends -->"""
        Line $"""<!-- Add <Import Project="path/to/Protobuf.targets" /> to your .fsproj to use it -->"""
        Frag includes
    ]
    Line $"""</ItemGroup>"""
    Line $"""</Project>"""
    ]

let generateTargetsFile (files: FileDef seq) (_request: Google.Protobuf.Compiler.CodeGeneratorRequest) =
    render 0 (toTargetsFile files)

let generateFile (infile: FileDef) (typeMap: TypeMap) (_request: Google.Protobuf.Compiler.CodeGeneratorRequest) =
    let protoMessageDefs = infile.MessageTypes
    let protoEnumDefs = infile.EnumTypes
    //let comments = getComments infile.SourceCodeInfo
    //let findComment = comments.TryFind
    let fsNamespace = toFsNamespaceDecl infile.Package
    let fsRecordDefs = toFsRecordDefs typeMap infile.Package protoMessageDefs protoEnumDefs

    render 0 (Frag [
        fsNamespace
        Line ""
        fsRecordDefs
    ])