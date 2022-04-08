module ProtoGenFsgrpc.Model
open Google.Protobuf

type Sci = SourceCodeInfo.Location
type SciMap = Map<string, Sci>

let private sciMapFrom (sci: SourceCodeInfo option) =
    let sciRecordFrom (location: SourceCodeInfo.Location) =
        let path = location.Paths |> Seq.map string |> String.concat "/"
        (path, location)
    match sci with
    | None -> Map.empty
    | Some sci ->
        sci.Location
        |> Seq.map sciRecordFrom
        |> Map.ofSeq

type Context = {
    Path : int list
    SciMap : SciMap
}
with
    static member From (sci) =
        {
        Path = List.empty
        SciMap = sciMapFrom sci
        }
    static member (+) (left: Context, right: int) =
        { left with Path = right :: left.Path }

let private path (ctx: Context) =
    ctx.Path |> List.rev |> Seq.map string |> String.concat "/"

let private sci (ctx: Context) =
    let path = path ctx
    let sci = ctx.SciMap.TryFind path
    sci

type EnumValueDef = {
    // _proto: EnumValueDescriptorProto
    Sci: Sci option
    Name: string
    Number: int
}
with
    static member From (ctx: Context) (i: int) (d: EnumValueDescriptorProto) =
        let ctx = ctx + i
        {
        // _proto = d
        Sci = sci ctx
        Name = d.Name
        Number = d.Number
        }

type EnumDef = {
    // _proto: EnumDescriptorProto
    Sci: Sci option
    Name: string // (1)
    Values: EnumValueDef seq // (2)
}
with
    static member From (ctx: Context) (i: int) (d: EnumDescriptorProto) =
        let ctx = ctx + i
        {
        // _proto = d
        Sci = sci ctx
        Name = d.Name
        Values = d.Values |> Seq.mapi (EnumValueDef.From (ctx + 2))
        }

type FieldDef = {
    // _proto: FieldDescriptorProto
    Sci: Sci option
    Name: string // (1)
    JsonName: string // (10)
    Number: int // (3)
    Label: Google.Protobuf.FieldDescriptorProto.Label // (4)
    Type: Google.Protobuf.FieldDescriptorProto.Type // (5)
    TypeName: string // (6)
    OneofIndex: int option // (9)
    Proto3Optional: bool // (17)
}
with
    static member From (ctx: Context) (i: int) (d: FieldDescriptorProto) =
        let ctx = ctx + i
        {
        // _proto = d
        Sci = sci ctx
        Number = d.Number
        Type = d.Type
        TypeName = d.TypeName
        Label = d.Label
        Name = d.Name
        JsonName = d.JsonName
        OneofIndex = d.OneofIndex
        Proto3Optional = d.Proto3Optional
        }

type OneofDef = {
    // _proto: OneofDescriptorProto
    Sci: Sci option
    Name: string
}
with
    static member From (ctx: Context) (i: int) (d: OneofDescriptorProto) =
        let ctx = ctx + i
        {
        // _proto = d
        Sci = sci ctx
        Name = d.Name
        }

type MessageDef = {
    // _proto: DescriptorProto
    Sci: Sci option
    Name: string // (1)
    Fields: FieldDef seq // (2)
    NestedTypes: MessageDef seq // (3)
    EnumTypes: EnumDef seq // (4)
    OneofDecls: OneofDef seq // (8)
    Options: Google.Protobuf.MessageOptions option // (7)
}
with
    static member From (ctx: Context) (i: int) (d: DescriptorProto) =
        let ctx = ctx + i
        {
        // _proto = d
        Sci = sci ctx
        Name = d.Name
        Fields = d.Fields |> Seq.mapi (FieldDef.From (ctx + 2))
        NestedTypes = d.NestedTypes |> Seq.mapi (MessageDef.From (ctx + 3))
        EnumTypes = d.EnumTypes |> Seq.mapi (EnumDef.From (ctx + 4))
        OneofDecls = d.OneofDecls |> Seq.mapi (OneofDef.From (ctx + 8))
        Options = d.Options
        }

type FileDef = {
    // _proto: FileDescriptorProto
    Name: string // (1)
    Package: string // (2)
    Dependencies: string seq // (3)
    MessageTypes: MessageDef seq // (4)
    EnumTypes: EnumDef seq // (5)
    //Services: Google.Protobuf.ServiceDescriptorProto seq // (6)
    //Extensions: Google.Protobuf.FieldDescriptorProto seq // (7)
    //Options: Google.Protobuf.FileOptions option // (8)
    //SourceCodeInfo: Google.Protobuf.SourceCodeInfo option // (9)
    //Syntax: string // (12)    
}
with
    static member From (d: FileDescriptorProto) =
        let ctx = Context.From d.SourceCodeInfo
        {
        // _proto = d
        Name = d.Name
        Package = d.Package
        Dependencies = d.Dependencies
        MessageTypes = d.MessageTypes |> Seq.mapi (MessageDef.From (ctx + 4))
        EnumTypes = d.EnumTypes |> Seq.mapi (EnumDef.From (ctx + 5))
        }