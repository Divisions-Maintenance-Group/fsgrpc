[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module rec Google.Protobuf
open FsGrpc.Protobuf
#nowarn "40"


[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module FileDescriptorSet =

    [<System.Runtime.CompilerServices.IsByRefLike>]
    type Builder =
        struct
            val mutable Files: RepeatedBuilder<Google.Protobuf.FileDescriptorProto> // (1)
        end
        with
        member x.Put ((tag, reader): int * Reader) =
            match tag with
            | 1 -> x.Files.Add (ValueCodec.Message<Google.Protobuf.FileDescriptorProto>.ReadValue reader)
            | _ -> reader.SkipLastField()
        member x.Build : Google.Protobuf.FileDescriptorSet = {
            Files = x.Files.Build
            }

let private _FileDescriptorSetProto : ProtoDef<FileDescriptorSet> =
    // Field Definitions
    let Files = FieldCodec.Repeated ValueCodec.Message<Google.Protobuf.FileDescriptorProto> 1
    // Proto Definition Implementation
    { // ProtoDef<FileDescriptorSet>
        Name = "FileDescriptorSet"
        Empty = {
            Files = Files.GetDefault()
            }
        Size = fun (m: FileDescriptorSet) ->
            0
            + Files.CalcFieldSize m.Files
        Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: FileDescriptorSet) ->
            Files.WriteField w m.Files
        Decode = fun (r: Google.Protobuf.CodedInputStream) ->
            let mutable builder = new Google.Protobuf.FileDescriptorSet.Builder()
            let mutable tag = 0
            while read r &tag do
                builder.Put (tag, r)
            builder.Build
        }
type FileDescriptorSet = {
    // Field Declarations
    Files: Google.Protobuf.FileDescriptorProto seq // (1)
    }
    with
    static member empty = _FileDescriptorSetProto.Empty
    static member Proto = lazy _FileDescriptorSetProto

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module FileDescriptorProto =

    [<System.Runtime.CompilerServices.IsByRefLike>]
    type Builder =
        struct
            val mutable Name: string // (1)
            val mutable Package: string // (2)
            val mutable Dependencies: RepeatedBuilder<string> // (3)
            val mutable PublicDependencies: RepeatedBuilder<int> // (10)
            val mutable WeakDependencies: RepeatedBuilder<int> // (11)
            val mutable MessageTypes: RepeatedBuilder<Google.Protobuf.DescriptorProto> // (4)
            val mutable EnumTypes: RepeatedBuilder<Google.Protobuf.EnumDescriptorProto> // (5)
            val mutable Services: RepeatedBuilder<Google.Protobuf.ServiceDescriptorProto> // (6)
            val mutable Extensions: RepeatedBuilder<Google.Protobuf.FieldDescriptorProto> // (7)
            val mutable Options: OptionBuilder<Google.Protobuf.FileOptions> // (8)
            val mutable SourceCodeInfo: OptionBuilder<Google.Protobuf.SourceCodeInfo> // (9)
            val mutable Syntax: string // (12)
        end
        with
        member x.Put ((tag, reader): int * Reader) =
            match tag with
            | 1 -> x.Name <- ValueCodec.String.ReadValue reader
            | 2 -> x.Package <- ValueCodec.String.ReadValue reader
            | 3 -> x.Dependencies.Add (ValueCodec.String.ReadValue reader)
            | 10 -> x.PublicDependencies.AddRange ((ValueCodec.Packed ValueCodec.Int32).ReadValue reader)
            | 11 -> x.WeakDependencies.AddRange ((ValueCodec.Packed ValueCodec.Int32).ReadValue reader)
            | 4 -> x.MessageTypes.Add (ValueCodec.Message<Google.Protobuf.DescriptorProto>.ReadValue reader)
            | 5 -> x.EnumTypes.Add (ValueCodec.Message<Google.Protobuf.EnumDescriptorProto>.ReadValue reader)
            | 6 -> x.Services.Add (ValueCodec.Message<Google.Protobuf.ServiceDescriptorProto>.ReadValue reader)
            | 7 -> x.Extensions.Add (ValueCodec.Message<Google.Protobuf.FieldDescriptorProto>.ReadValue reader)
            | 8 -> x.Options.Set (ValueCodec.Message<Google.Protobuf.FileOptions>.ReadValue reader)
            | 9 -> x.SourceCodeInfo.Set (ValueCodec.Message<Google.Protobuf.SourceCodeInfo>.ReadValue reader)
            | 12 -> x.Syntax <- ValueCodec.String.ReadValue reader
            | _ -> reader.SkipLastField()
        member x.Build : Google.Protobuf.FileDescriptorProto = {
            Name = x.Name |> orEmptyString
            Package = x.Package |> orEmptyString
            Dependencies = x.Dependencies.Build
            PublicDependencies = x.PublicDependencies.Build
            WeakDependencies = x.WeakDependencies.Build
            MessageTypes = x.MessageTypes.Build
            EnumTypes = x.EnumTypes.Build
            Services = x.Services.Build
            Extensions = x.Extensions.Build
            Options = x.Options.Build
            SourceCodeInfo = x.SourceCodeInfo.Build
            Syntax = x.Syntax |> orEmptyString
            }

let private _FileDescriptorProtoProto : ProtoDef<FileDescriptorProto> =
    // Field Definitions
    let Name = FieldCodec.Primitive ValueCodec.String 1
    let Package = FieldCodec.Primitive ValueCodec.String 2
    let Dependencies = FieldCodec.Repeated ValueCodec.String 3
    let PublicDependencies = FieldCodec.Primitive (ValueCodec.Packed ValueCodec.Int32) 10
    let WeakDependencies = FieldCodec.Primitive (ValueCodec.Packed ValueCodec.Int32) 11
    let MessageTypes = FieldCodec.Repeated ValueCodec.Message<Google.Protobuf.DescriptorProto> 4
    let EnumTypes = FieldCodec.Repeated ValueCodec.Message<Google.Protobuf.EnumDescriptorProto> 5
    let Services = FieldCodec.Repeated ValueCodec.Message<Google.Protobuf.ServiceDescriptorProto> 6
    let Extensions = FieldCodec.Repeated ValueCodec.Message<Google.Protobuf.FieldDescriptorProto> 7
    let Options = FieldCodec.Optional ValueCodec.Message<Google.Protobuf.FileOptions> 8
    let SourceCodeInfo = FieldCodec.Optional ValueCodec.Message<Google.Protobuf.SourceCodeInfo> 9
    let Syntax = FieldCodec.Primitive ValueCodec.String 12
    // Proto Definition Implementation
    { // ProtoDef<FileDescriptorProto>
        Name = "FileDescriptorProto"
        Empty = {
            Name = Name.GetDefault()
            Package = Package.GetDefault()
            Dependencies = Dependencies.GetDefault()
            PublicDependencies = PublicDependencies.GetDefault()
            WeakDependencies = WeakDependencies.GetDefault()
            MessageTypes = MessageTypes.GetDefault()
            EnumTypes = EnumTypes.GetDefault()
            Services = Services.GetDefault()
            Extensions = Extensions.GetDefault()
            Options = Options.GetDefault()
            SourceCodeInfo = SourceCodeInfo.GetDefault()
            Syntax = Syntax.GetDefault()
            }
        Size = fun (m: FileDescriptorProto) ->
            0
            + Name.CalcFieldSize m.Name
            + Package.CalcFieldSize m.Package
            + Dependencies.CalcFieldSize m.Dependencies
            + PublicDependencies.CalcFieldSize m.PublicDependencies
            + WeakDependencies.CalcFieldSize m.WeakDependencies
            + MessageTypes.CalcFieldSize m.MessageTypes
            + EnumTypes.CalcFieldSize m.EnumTypes
            + Services.CalcFieldSize m.Services
            + Extensions.CalcFieldSize m.Extensions
            + Options.CalcFieldSize m.Options
            + SourceCodeInfo.CalcFieldSize m.SourceCodeInfo
            + Syntax.CalcFieldSize m.Syntax
        Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: FileDescriptorProto) ->
            Name.WriteField w m.Name
            Package.WriteField w m.Package
            Dependencies.WriteField w m.Dependencies
            PublicDependencies.WriteField w m.PublicDependencies
            WeakDependencies.WriteField w m.WeakDependencies
            MessageTypes.WriteField w m.MessageTypes
            EnumTypes.WriteField w m.EnumTypes
            Services.WriteField w m.Services
            Extensions.WriteField w m.Extensions
            Options.WriteField w m.Options
            SourceCodeInfo.WriteField w m.SourceCodeInfo
            Syntax.WriteField w m.Syntax
        Decode = fun (r: Google.Protobuf.CodedInputStream) ->
            let mutable builder = new Google.Protobuf.FileDescriptorProto.Builder()
            let mutable tag = 0
            while read r &tag do
                builder.Put (tag, r)
            builder.Build
        }
type FileDescriptorProto = {
    // Field Declarations
    Name: string // (1)
    Package: string // (2)
    Dependencies: string seq // (3)
    PublicDependencies: int seq // (10)
    WeakDependencies: int seq // (11)
    MessageTypes: Google.Protobuf.DescriptorProto seq // (4)
    EnumTypes: Google.Protobuf.EnumDescriptorProto seq // (5)
    Services: Google.Protobuf.ServiceDescriptorProto seq // (6)
    Extensions: Google.Protobuf.FieldDescriptorProto seq // (7)
    Options: Google.Protobuf.FileOptions option // (8)
    SourceCodeInfo: Google.Protobuf.SourceCodeInfo option // (9)
    Syntax: string // (12)
    }
    with
    static member empty = _FileDescriptorProtoProto.Empty
    static member Proto = lazy _FileDescriptorProtoProto

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module DescriptorProto =

    [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
    module ExtensionRange =

        [<System.Runtime.CompilerServices.IsByRefLike>]
        type Builder =
            struct
                val mutable Start: int // (1)
                val mutable End: int // (2)
                val mutable Options: OptionBuilder<Google.Protobuf.ExtensionRangeOptions> // (3)
            end
            with
            member x.Put ((tag, reader): int * Reader) =
                match tag with
                | 1 -> x.Start <- ValueCodec.Int32.ReadValue reader
                | 2 -> x.End <- ValueCodec.Int32.ReadValue reader
                | 3 -> x.Options.Set (ValueCodec.Message<Google.Protobuf.ExtensionRangeOptions>.ReadValue reader)
                | _ -> reader.SkipLastField()
            member x.Build : Google.Protobuf.DescriptorProto.ExtensionRange = {
                Start = x.Start
                End = x.End
                Options = x.Options.Build
                }

    let private _ExtensionRangeProto : ProtoDef<ExtensionRange> =
        // Field Definitions
        let Start = FieldCodec.Primitive ValueCodec.Int32 1
        let End = FieldCodec.Primitive ValueCodec.Int32 2
        let Options = FieldCodec.Optional ValueCodec.Message<Google.Protobuf.ExtensionRangeOptions> 3
        // Proto Definition Implementation
        { // ProtoDef<ExtensionRange>
            Name = "ExtensionRange"
            Empty = {
                Start = Start.GetDefault()
                End = End.GetDefault()
                Options = Options.GetDefault()
                }
            Size = fun (m: ExtensionRange) ->
                0
                + Start.CalcFieldSize m.Start
                + End.CalcFieldSize m.End
                + Options.CalcFieldSize m.Options
            Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: ExtensionRange) ->
                Start.WriteField w m.Start
                End.WriteField w m.End
                Options.WriteField w m.Options
            Decode = fun (r: Google.Protobuf.CodedInputStream) ->
                let mutable builder = new Google.Protobuf.DescriptorProto.ExtensionRange.Builder()
                let mutable tag = 0
                while read r &tag do
                    builder.Put (tag, r)
                builder.Build
            }
    type ExtensionRange = {
        // Field Declarations
        Start: int // (1)
        End: int // (2)
        Options: Google.Protobuf.ExtensionRangeOptions option // (3)
        }
        with
        static member empty = _ExtensionRangeProto.Empty
        static member Proto = lazy _ExtensionRangeProto

    [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
    module ReservedRange =

        [<System.Runtime.CompilerServices.IsByRefLike>]
        type Builder =
            struct
                val mutable Start: int // (1)
                val mutable End: int // (2)
            end
            with
            member x.Put ((tag, reader): int * Reader) =
                match tag with
                | 1 -> x.Start <- ValueCodec.Int32.ReadValue reader
                | 2 -> x.End <- ValueCodec.Int32.ReadValue reader
                | _ -> reader.SkipLastField()
            member x.Build : Google.Protobuf.DescriptorProto.ReservedRange = {
                Start = x.Start
                End = x.End
                }

    let private _ReservedRangeProto : ProtoDef<ReservedRange> =
        // Field Definitions
        let Start = FieldCodec.Primitive ValueCodec.Int32 1
        let End = FieldCodec.Primitive ValueCodec.Int32 2
        // Proto Definition Implementation
        { // ProtoDef<ReservedRange>
            Name = "ReservedRange"
            Empty = {
                Start = Start.GetDefault()
                End = End.GetDefault()
                }
            Size = fun (m: ReservedRange) ->
                0
                + Start.CalcFieldSize m.Start
                + End.CalcFieldSize m.End
            Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: ReservedRange) ->
                Start.WriteField w m.Start
                End.WriteField w m.End
            Decode = fun (r: Google.Protobuf.CodedInputStream) ->
                let mutable builder = new Google.Protobuf.DescriptorProto.ReservedRange.Builder()
                let mutable tag = 0
                while read r &tag do
                    builder.Put (tag, r)
                builder.Build
            }
    type ReservedRange = {
        // Field Declarations
        Start: int // (1)
        End: int // (2)
        }
        with
        static member empty = _ReservedRangeProto.Empty
        static member Proto = lazy _ReservedRangeProto

    [<System.Runtime.CompilerServices.IsByRefLike>]
    type Builder =
        struct
            val mutable Name: string // (1)
            val mutable Fields: RepeatedBuilder<Google.Protobuf.FieldDescriptorProto> // (2)
            val mutable Extensions: RepeatedBuilder<Google.Protobuf.FieldDescriptorProto> // (6)
            val mutable NestedTypes: RepeatedBuilder<Google.Protobuf.DescriptorProto> // (3)
            val mutable EnumTypes: RepeatedBuilder<Google.Protobuf.EnumDescriptorProto> // (4)
            val mutable ExtensionRanges: RepeatedBuilder<Google.Protobuf.DescriptorProto.ExtensionRange> // (5)
            val mutable OneofDecls: RepeatedBuilder<Google.Protobuf.OneofDescriptorProto> // (8)
            val mutable Options: OptionBuilder<Google.Protobuf.MessageOptions> // (7)
            val mutable ReservedRanges: RepeatedBuilder<Google.Protobuf.DescriptorProto.ReservedRange> // (9)
            val mutable ReservedNames: RepeatedBuilder<string> // (10)
        end
        with
        member x.Put ((tag, reader): int * Reader) =
            match tag with
            | 1 -> x.Name <- ValueCodec.String.ReadValue reader
            | 2 -> x.Fields.Add (ValueCodec.Message<Google.Protobuf.FieldDescriptorProto>.ReadValue reader)
            | 6 -> x.Extensions.Add (ValueCodec.Message<Google.Protobuf.FieldDescriptorProto>.ReadValue reader)
            | 3 -> x.NestedTypes.Add (ValueCodec.Message<Google.Protobuf.DescriptorProto>.ReadValue reader)
            | 4 -> x.EnumTypes.Add (ValueCodec.Message<Google.Protobuf.EnumDescriptorProto>.ReadValue reader)
            | 5 -> x.ExtensionRanges.Add (ValueCodec.Message<Google.Protobuf.DescriptorProto.ExtensionRange>.ReadValue reader)
            | 8 -> x.OneofDecls.Add (ValueCodec.Message<Google.Protobuf.OneofDescriptorProto>.ReadValue reader)
            | 7 -> x.Options.Set (ValueCodec.Message<Google.Protobuf.MessageOptions>.ReadValue reader)
            | 9 -> x.ReservedRanges.Add (ValueCodec.Message<Google.Protobuf.DescriptorProto.ReservedRange>.ReadValue reader)
            | 10 -> x.ReservedNames.Add (ValueCodec.String.ReadValue reader)
            | _ -> reader.SkipLastField()
        member x.Build : Google.Protobuf.DescriptorProto = {
            Name = x.Name |> orEmptyString
            Fields = x.Fields.Build
            Extensions = x.Extensions.Build
            NestedTypes = x.NestedTypes.Build
            EnumTypes = x.EnumTypes.Build
            ExtensionRanges = x.ExtensionRanges.Build
            OneofDecls = x.OneofDecls.Build
            Options = x.Options.Build
            ReservedRanges = x.ReservedRanges.Build
            ReservedNames = x.ReservedNames.Build
            }

let private _DescriptorProtoProto : ProtoDef<DescriptorProto> =
    // Field Definitions
    let Name = FieldCodec.Primitive ValueCodec.String 1
    let Fields = FieldCodec.Repeated ValueCodec.Message<Google.Protobuf.FieldDescriptorProto> 2
    let Extensions = FieldCodec.Repeated ValueCodec.Message<Google.Protobuf.FieldDescriptorProto> 6
    let NestedTypes = FieldCodec.Repeated ValueCodec.Message<Google.Protobuf.DescriptorProto> 3
    let EnumTypes = FieldCodec.Repeated ValueCodec.Message<Google.Protobuf.EnumDescriptorProto> 4
    let ExtensionRanges = FieldCodec.Repeated ValueCodec.Message<Google.Protobuf.DescriptorProto.ExtensionRange> 5
    let OneofDecls = FieldCodec.Repeated ValueCodec.Message<Google.Protobuf.OneofDescriptorProto> 8
    let Options = FieldCodec.Optional ValueCodec.Message<Google.Protobuf.MessageOptions> 7
    let ReservedRanges = FieldCodec.Repeated ValueCodec.Message<Google.Protobuf.DescriptorProto.ReservedRange> 9
    let ReservedNames = FieldCodec.Repeated ValueCodec.String 10
    // Proto Definition Implementation
    { // ProtoDef<DescriptorProto>
        Name = "DescriptorProto"
        Empty = {
            Name = Name.GetDefault()
            Fields = Fields.GetDefault()
            Extensions = Extensions.GetDefault()
            NestedTypes = NestedTypes.GetDefault()
            EnumTypes = EnumTypes.GetDefault()
            ExtensionRanges = ExtensionRanges.GetDefault()
            OneofDecls = OneofDecls.GetDefault()
            Options = Options.GetDefault()
            ReservedRanges = ReservedRanges.GetDefault()
            ReservedNames = ReservedNames.GetDefault()
            }
        Size = fun (m: DescriptorProto) ->
            0
            + Name.CalcFieldSize m.Name
            + Fields.CalcFieldSize m.Fields
            + Extensions.CalcFieldSize m.Extensions
            + NestedTypes.CalcFieldSize m.NestedTypes
            + EnumTypes.CalcFieldSize m.EnumTypes
            + ExtensionRanges.CalcFieldSize m.ExtensionRanges
            + OneofDecls.CalcFieldSize m.OneofDecls
            + Options.CalcFieldSize m.Options
            + ReservedRanges.CalcFieldSize m.ReservedRanges
            + ReservedNames.CalcFieldSize m.ReservedNames
        Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: DescriptorProto) ->
            Name.WriteField w m.Name
            Fields.WriteField w m.Fields
            Extensions.WriteField w m.Extensions
            NestedTypes.WriteField w m.NestedTypes
            EnumTypes.WriteField w m.EnumTypes
            ExtensionRanges.WriteField w m.ExtensionRanges
            OneofDecls.WriteField w m.OneofDecls
            Options.WriteField w m.Options
            ReservedRanges.WriteField w m.ReservedRanges
            ReservedNames.WriteField w m.ReservedNames
        Decode = fun (r: Google.Protobuf.CodedInputStream) ->
            let mutable builder = new Google.Protobuf.DescriptorProto.Builder()
            let mutable tag = 0
            while read r &tag do
                builder.Put (tag, r)
            builder.Build
        }
type DescriptorProto = {
    // Field Declarations
    Name: string // (1)
    Fields: Google.Protobuf.FieldDescriptorProto seq // (2)
    Extensions: Google.Protobuf.FieldDescriptorProto seq // (6)
    NestedTypes: Google.Protobuf.DescriptorProto seq // (3)
    EnumTypes: Google.Protobuf.EnumDescriptorProto seq // (4)
    ExtensionRanges: Google.Protobuf.DescriptorProto.ExtensionRange seq // (5)
    OneofDecls: Google.Protobuf.OneofDescriptorProto seq // (8)
    Options: Google.Protobuf.MessageOptions option // (7)
    ReservedRanges: Google.Protobuf.DescriptorProto.ReservedRange seq // (9)
    ReservedNames: string seq // (10)
    }
    with
    static member empty = _DescriptorProtoProto.Empty
    static member Proto = lazy _DescriptorProtoProto

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module ExtensionRangeOptions =

    [<System.Runtime.CompilerServices.IsByRefLike>]
    type Builder =
        struct
            val mutable UninterpretedOptions: RepeatedBuilder<Google.Protobuf.UninterpretedOption> // (999)
        end
        with
        member x.Put ((tag, reader): int * Reader) =
            match tag with
            | 999 -> x.UninterpretedOptions.Add (ValueCodec.Message<Google.Protobuf.UninterpretedOption>.ReadValue reader)
            | _ -> reader.SkipLastField()
        member x.Build : Google.Protobuf.ExtensionRangeOptions = {
            UninterpretedOptions = x.UninterpretedOptions.Build
            }

let private _ExtensionRangeOptionsProto : ProtoDef<ExtensionRangeOptions> =
    // Field Definitions
    let UninterpretedOptions = FieldCodec.Repeated ValueCodec.Message<Google.Protobuf.UninterpretedOption> 999
    // Proto Definition Implementation
    { // ProtoDef<ExtensionRangeOptions>
        Name = "ExtensionRangeOptions"
        Empty = {
            UninterpretedOptions = UninterpretedOptions.GetDefault()
            }
        Size = fun (m: ExtensionRangeOptions) ->
            0
            + UninterpretedOptions.CalcFieldSize m.UninterpretedOptions
        Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: ExtensionRangeOptions) ->
            UninterpretedOptions.WriteField w m.UninterpretedOptions
        Decode = fun (r: Google.Protobuf.CodedInputStream) ->
            let mutable builder = new Google.Protobuf.ExtensionRangeOptions.Builder()
            let mutable tag = 0
            while read r &tag do
                builder.Put (tag, r)
            builder.Build
        }
type ExtensionRangeOptions = {
    // Field Declarations
    UninterpretedOptions: Google.Protobuf.UninterpretedOption seq // (999)
    }
    with
    static member empty = _ExtensionRangeOptionsProto.Empty
    static member Proto = lazy _ExtensionRangeOptionsProto

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module FieldDescriptorProto =

    type Type =
    | Unspecified = 0
    | Double = 1
    | Float = 2
    | Int64 = 3
    | Uint64 = 4
    | Int32 = 5
    | Fixed64 = 6
    | Fixed32 = 7
    | Bool = 8
    | String = 9
    | Group = 10
    | Message = 11
    | Bytes = 12
    | Uint32 = 13
    | Enum = 14
    | Sfixed32 = 15
    | Sfixed64 = 16
    | Sint32 = 17
    | Sint64 = 18

    type Label =
    | Unspecified = 0
    | Optional = 1
    | Required = 2
    | Repeated = 3

    [<System.Runtime.CompilerServices.IsByRefLike>]
    type Builder =
        struct
            val mutable Name: string // (1)
            val mutable Number: int // (3)
            val mutable Label: Google.Protobuf.FieldDescriptorProto.Label // (4)
            val mutable Type: Google.Protobuf.FieldDescriptorProto.Type // (5)
            val mutable TypeName: string // (6)
            val mutable Extendee: string // (2)
            val mutable DefaultValue: string // (7)
            val mutable OneofIndex: OptionBuilder<int> // (9)
            val mutable JsonName: string // (10)
            val mutable Options: OptionBuilder<Google.Protobuf.FieldOptions> // (8)
            val mutable Proto3Optional: bool // (17)
        end
        with
        member x.Put ((tag, reader): int * Reader) =
            match tag with
            | 1 -> x.Name <- ValueCodec.String.ReadValue reader
            | 3 -> x.Number <- ValueCodec.Int32.ReadValue reader
            | 4 -> x.Label <- ValueCodec.Enum<Google.Protobuf.FieldDescriptorProto.Label>.ReadValue reader
            | 5 -> x.Type <- ValueCodec.Enum<Google.Protobuf.FieldDescriptorProto.Type>.ReadValue reader
            | 6 -> x.TypeName <- ValueCodec.String.ReadValue reader
            | 2 -> x.Extendee <- ValueCodec.String.ReadValue reader
            | 7 -> x.DefaultValue <- ValueCodec.String.ReadValue reader
            | 9 -> x.OneofIndex.Set (ValueCodec.Int32.ReadValue reader)
            | 10 -> x.JsonName <- ValueCodec.String.ReadValue reader
            | 8 -> x.Options.Set (ValueCodec.Message<Google.Protobuf.FieldOptions>.ReadValue reader)
            | 17 -> x.Proto3Optional <- ValueCodec.Bool.ReadValue reader
            | _ -> reader.SkipLastField()
        member x.Build : Google.Protobuf.FieldDescriptorProto = {
            Name = x.Name |> orEmptyString
            Number = x.Number
            Label = x.Label
            Type = x.Type
            TypeName = x.TypeName |> orEmptyString
            Extendee = x.Extendee |> orEmptyString
            DefaultValue = x.DefaultValue |> orEmptyString
            OneofIndex = x.OneofIndex.Build
            JsonName = x.JsonName |> orEmptyString
            Options = x.Options.Build
            Proto3Optional = x.Proto3Optional
            }

let private _FieldDescriptorProtoProto : ProtoDef<FieldDescriptorProto> =
    // Field Definitions
    let Name = FieldCodec.Primitive ValueCodec.String 1
    let Number = FieldCodec.Primitive ValueCodec.Int32 3
    let Label = FieldCodec.Primitive ValueCodec.Enum<Google.Protobuf.FieldDescriptorProto.Label> 4
    let Type = FieldCodec.Primitive ValueCodec.Enum<Google.Protobuf.FieldDescriptorProto.Type> 5
    let TypeName = FieldCodec.Primitive ValueCodec.String 6
    let Extendee = FieldCodec.Primitive ValueCodec.String 2
    let DefaultValue = FieldCodec.Primitive ValueCodec.String 7
    let OneofIndex = FieldCodec.Optional ValueCodec.Int32 9
    let JsonName = FieldCodec.Primitive ValueCodec.String 10
    let Options = FieldCodec.Optional ValueCodec.Message<Google.Protobuf.FieldOptions> 8
    let Proto3Optional = FieldCodec.Primitive ValueCodec.Bool 17
    // Proto Definition Implementation
    { // ProtoDef<FieldDescriptorProto>
        Name = "FieldDescriptorProto"
        Empty = {
            Name = Name.GetDefault()
            Number = Number.GetDefault()
            Label = Label.GetDefault()
            Type = Type.GetDefault()
            TypeName = TypeName.GetDefault()
            Extendee = Extendee.GetDefault()
            DefaultValue = DefaultValue.GetDefault()
            OneofIndex = OneofIndex.GetDefault()
            JsonName = JsonName.GetDefault()
            Options = Options.GetDefault()
            Proto3Optional = Proto3Optional.GetDefault()
            }
        Size = fun (m: FieldDescriptorProto) ->
            0
            + Name.CalcFieldSize m.Name
            + Number.CalcFieldSize m.Number
            + Label.CalcFieldSize m.Label
            + Type.CalcFieldSize m.Type
            + TypeName.CalcFieldSize m.TypeName
            + Extendee.CalcFieldSize m.Extendee
            + DefaultValue.CalcFieldSize m.DefaultValue
            + OneofIndex.CalcFieldSize m.OneofIndex
            + JsonName.CalcFieldSize m.JsonName
            + Options.CalcFieldSize m.Options
            + Proto3Optional.CalcFieldSize m.Proto3Optional
        Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: FieldDescriptorProto) ->
            Name.WriteField w m.Name
            Number.WriteField w m.Number
            Label.WriteField w m.Label
            Type.WriteField w m.Type
            TypeName.WriteField w m.TypeName
            Extendee.WriteField w m.Extendee
            DefaultValue.WriteField w m.DefaultValue
            OneofIndex.WriteField w m.OneofIndex
            JsonName.WriteField w m.JsonName
            Options.WriteField w m.Options
            Proto3Optional.WriteField w m.Proto3Optional
        Decode = fun (r: Google.Protobuf.CodedInputStream) ->
            let mutable builder = new Google.Protobuf.FieldDescriptorProto.Builder()
            let mutable tag = 0
            while read r &tag do
                builder.Put (tag, r)
            builder.Build
        }
type FieldDescriptorProto = {
    // Field Declarations
    Name: string // (1)
    Number: int // (3)
    Label: Google.Protobuf.FieldDescriptorProto.Label // (4)
    Type: Google.Protobuf.FieldDescriptorProto.Type // (5)
    TypeName: string // (6)
    Extendee: string // (2)
    DefaultValue: string // (7)
    OneofIndex: int option // (9)
    JsonName: string // (10)
    Options: Google.Protobuf.FieldOptions option // (8)
    Proto3Optional: bool // (17)
    }
    with
    static member empty = _FieldDescriptorProtoProto.Empty
    static member Proto = lazy _FieldDescriptorProtoProto

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module OneofDescriptorProto =

    [<System.Runtime.CompilerServices.IsByRefLike>]
    type Builder =
        struct
            val mutable Name: string // (1)
            val mutable Options: OptionBuilder<Google.Protobuf.OneofOptions> // (2)
        end
        with
        member x.Put ((tag, reader): int * Reader) =
            match tag with
            | 1 -> x.Name <- ValueCodec.String.ReadValue reader
            | 2 -> x.Options.Set (ValueCodec.Message<Google.Protobuf.OneofOptions>.ReadValue reader)
            | _ -> reader.SkipLastField()
        member x.Build : Google.Protobuf.OneofDescriptorProto = {
            Name = x.Name |> orEmptyString
            Options = x.Options.Build
            }

let private _OneofDescriptorProtoProto : ProtoDef<OneofDescriptorProto> =
    // Field Definitions
    let Name = FieldCodec.Primitive ValueCodec.String 1
    let Options = FieldCodec.Optional ValueCodec.Message<Google.Protobuf.OneofOptions> 2
    // Proto Definition Implementation
    { // ProtoDef<OneofDescriptorProto>
        Name = "OneofDescriptorProto"
        Empty = {
            Name = Name.GetDefault()
            Options = Options.GetDefault()
            }
        Size = fun (m: OneofDescriptorProto) ->
            0
            + Name.CalcFieldSize m.Name
            + Options.CalcFieldSize m.Options
        Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: OneofDescriptorProto) ->
            Name.WriteField w m.Name
            Options.WriteField w m.Options
        Decode = fun (r: Google.Protobuf.CodedInputStream) ->
            let mutable builder = new Google.Protobuf.OneofDescriptorProto.Builder()
            let mutable tag = 0
            while read r &tag do
                builder.Put (tag, r)
            builder.Build
        }
type OneofDescriptorProto = {
    // Field Declarations
    Name: string // (1)
    Options: Google.Protobuf.OneofOptions option // (2)
    }
    with
    static member empty = _OneofDescriptorProtoProto.Empty
    static member Proto = lazy _OneofDescriptorProtoProto

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module EnumDescriptorProto =

    [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
    module EnumReservedRange =

        [<System.Runtime.CompilerServices.IsByRefLike>]
        type Builder =
            struct
                val mutable Start: int // (1)
                val mutable End: int // (2)
            end
            with
            member x.Put ((tag, reader): int * Reader) =
                match tag with
                | 1 -> x.Start <- ValueCodec.Int32.ReadValue reader
                | 2 -> x.End <- ValueCodec.Int32.ReadValue reader
                | _ -> reader.SkipLastField()
            member x.Build : Google.Protobuf.EnumDescriptorProto.EnumReservedRange = {
                Start = x.Start
                End = x.End
                }

    let private _EnumReservedRangeProto : ProtoDef<EnumReservedRange> =
        // Field Definitions
        let Start = FieldCodec.Primitive ValueCodec.Int32 1
        let End = FieldCodec.Primitive ValueCodec.Int32 2
        // Proto Definition Implementation
        { // ProtoDef<EnumReservedRange>
            Name = "EnumReservedRange"
            Empty = {
                Start = Start.GetDefault()
                End = End.GetDefault()
                }
            Size = fun (m: EnumReservedRange) ->
                0
                + Start.CalcFieldSize m.Start
                + End.CalcFieldSize m.End
            Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: EnumReservedRange) ->
                Start.WriteField w m.Start
                End.WriteField w m.End
            Decode = fun (r: Google.Protobuf.CodedInputStream) ->
                let mutable builder = new Google.Protobuf.EnumDescriptorProto.EnumReservedRange.Builder()
                let mutable tag = 0
                while read r &tag do
                    builder.Put (tag, r)
                builder.Build
            }
    type EnumReservedRange = {
        // Field Declarations
        Start: int // (1)
        End: int // (2)
        }
        with
        static member empty = _EnumReservedRangeProto.Empty
        static member Proto = lazy _EnumReservedRangeProto

    [<System.Runtime.CompilerServices.IsByRefLike>]
    type Builder =
        struct
            val mutable Name: string // (1)
            val mutable Values: RepeatedBuilder<Google.Protobuf.EnumValueDescriptorProto> // (2)
            val mutable Options: OptionBuilder<Google.Protobuf.EnumOptions> // (3)
            val mutable ReservedRanges: RepeatedBuilder<Google.Protobuf.EnumDescriptorProto.EnumReservedRange> // (4)
            val mutable ReservedNames: RepeatedBuilder<string> // (5)
        end
        with
        member x.Put ((tag, reader): int * Reader) =
            match tag with
            | 1 -> x.Name <- ValueCodec.String.ReadValue reader
            | 2 -> x.Values.Add (ValueCodec.Message<Google.Protobuf.EnumValueDescriptorProto>.ReadValue reader)
            | 3 -> x.Options.Set (ValueCodec.Message<Google.Protobuf.EnumOptions>.ReadValue reader)
            | 4 -> x.ReservedRanges.Add (ValueCodec.Message<Google.Protobuf.EnumDescriptorProto.EnumReservedRange>.ReadValue reader)
            | 5 -> x.ReservedNames.Add (ValueCodec.String.ReadValue reader)
            | _ -> reader.SkipLastField()
        member x.Build : Google.Protobuf.EnumDescriptorProto = {
            Name = x.Name |> orEmptyString
            Values = x.Values.Build
            Options = x.Options.Build
            ReservedRanges = x.ReservedRanges.Build
            ReservedNames = x.ReservedNames.Build
            }

let private _EnumDescriptorProtoProto : ProtoDef<EnumDescriptorProto> =
    // Field Definitions
    let Name = FieldCodec.Primitive ValueCodec.String 1
    let Values = FieldCodec.Repeated ValueCodec.Message<Google.Protobuf.EnumValueDescriptorProto> 2
    let Options = FieldCodec.Optional ValueCodec.Message<Google.Protobuf.EnumOptions> 3
    let ReservedRanges = FieldCodec.Repeated ValueCodec.Message<Google.Protobuf.EnumDescriptorProto.EnumReservedRange> 4
    let ReservedNames = FieldCodec.Repeated ValueCodec.String 5
    // Proto Definition Implementation
    { // ProtoDef<EnumDescriptorProto>
        Name = "EnumDescriptorProto"
        Empty = {
            Name = Name.GetDefault()
            Values = Values.GetDefault()
            Options = Options.GetDefault()
            ReservedRanges = ReservedRanges.GetDefault()
            ReservedNames = ReservedNames.GetDefault()
            }
        Size = fun (m: EnumDescriptorProto) ->
            0
            + Name.CalcFieldSize m.Name
            + Values.CalcFieldSize m.Values
            + Options.CalcFieldSize m.Options
            + ReservedRanges.CalcFieldSize m.ReservedRanges
            + ReservedNames.CalcFieldSize m.ReservedNames
        Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: EnumDescriptorProto) ->
            Name.WriteField w m.Name
            Values.WriteField w m.Values
            Options.WriteField w m.Options
            ReservedRanges.WriteField w m.ReservedRanges
            ReservedNames.WriteField w m.ReservedNames
        Decode = fun (r: Google.Protobuf.CodedInputStream) ->
            let mutable builder = new Google.Protobuf.EnumDescriptorProto.Builder()
            let mutable tag = 0
            while read r &tag do
                builder.Put (tag, r)
            builder.Build
        }
type EnumDescriptorProto = {
    // Field Declarations
    Name: string // (1)
    Values: Google.Protobuf.EnumValueDescriptorProto seq // (2)
    Options: Google.Protobuf.EnumOptions option // (3)
    ReservedRanges: Google.Protobuf.EnumDescriptorProto.EnumReservedRange seq // (4)
    ReservedNames: string seq // (5)
    }
    with
    static member empty = _EnumDescriptorProtoProto.Empty
    static member Proto = lazy _EnumDescriptorProtoProto

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module EnumValueDescriptorProto =

    [<System.Runtime.CompilerServices.IsByRefLike>]
    type Builder =
        struct
            val mutable Name: string // (1)
            val mutable Number: int // (2)
            val mutable Options: OptionBuilder<Google.Protobuf.EnumValueOptions> // (3)
        end
        with
        member x.Put ((tag, reader): int * Reader) =
            match tag with
            | 1 -> x.Name <- ValueCodec.String.ReadValue reader
            | 2 -> x.Number <- ValueCodec.Int32.ReadValue reader
            | 3 -> x.Options.Set (ValueCodec.Message<Google.Protobuf.EnumValueOptions>.ReadValue reader)
            | _ -> reader.SkipLastField()
        member x.Build : Google.Protobuf.EnumValueDescriptorProto = {
            Name = x.Name |> orEmptyString
            Number = x.Number
            Options = x.Options.Build
            }

let private _EnumValueDescriptorProtoProto : ProtoDef<EnumValueDescriptorProto> =
    // Field Definitions
    let Name = FieldCodec.Primitive ValueCodec.String 1
    let Number = FieldCodec.Primitive ValueCodec.Int32 2
    let Options = FieldCodec.Optional ValueCodec.Message<Google.Protobuf.EnumValueOptions> 3
    // Proto Definition Implementation
    { // ProtoDef<EnumValueDescriptorProto>
        Name = "EnumValueDescriptorProto"
        Empty = {
            Name = Name.GetDefault()
            Number = Number.GetDefault()
            Options = Options.GetDefault()
            }
        Size = fun (m: EnumValueDescriptorProto) ->
            0
            + Name.CalcFieldSize m.Name
            + Number.CalcFieldSize m.Number
            + Options.CalcFieldSize m.Options
        Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: EnumValueDescriptorProto) ->
            Name.WriteField w m.Name
            Number.WriteField w m.Number
            Options.WriteField w m.Options
        Decode = fun (r: Google.Protobuf.CodedInputStream) ->
            let mutable builder = new Google.Protobuf.EnumValueDescriptorProto.Builder()
            let mutable tag = 0
            while read r &tag do
                builder.Put (tag, r)
            builder.Build
        }
type EnumValueDescriptorProto = {
    // Field Declarations
    Name: string // (1)
    Number: int // (2)
    Options: Google.Protobuf.EnumValueOptions option // (3)
    }
    with
    static member empty = _EnumValueDescriptorProtoProto.Empty
    static member Proto = lazy _EnumValueDescriptorProtoProto

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module ServiceDescriptorProto =

    [<System.Runtime.CompilerServices.IsByRefLike>]
    type Builder =
        struct
            val mutable Name: string // (1)
            val mutable Methods: RepeatedBuilder<Google.Protobuf.MethodDescriptorProto> // (2)
            val mutable Options: OptionBuilder<Google.Protobuf.ServiceOptions> // (3)
        end
        with
        member x.Put ((tag, reader): int * Reader) =
            match tag with
            | 1 -> x.Name <- ValueCodec.String.ReadValue reader
            | 2 -> x.Methods.Add (ValueCodec.Message<Google.Protobuf.MethodDescriptorProto>.ReadValue reader)
            | 3 -> x.Options.Set (ValueCodec.Message<Google.Protobuf.ServiceOptions>.ReadValue reader)
            | _ -> reader.SkipLastField()
        member x.Build : Google.Protobuf.ServiceDescriptorProto = {
            Name = x.Name |> orEmptyString
            Methods = x.Methods.Build
            Options = x.Options.Build
            }

let private _ServiceDescriptorProtoProto : ProtoDef<ServiceDescriptorProto> =
    // Field Definitions
    let Name = FieldCodec.Primitive ValueCodec.String 1
    let Methods = FieldCodec.Repeated ValueCodec.Message<Google.Protobuf.MethodDescriptorProto> 2
    let Options = FieldCodec.Optional ValueCodec.Message<Google.Protobuf.ServiceOptions> 3
    // Proto Definition Implementation
    { // ProtoDef<ServiceDescriptorProto>
        Name = "ServiceDescriptorProto"
        Empty = {
            Name = Name.GetDefault()
            Methods = Methods.GetDefault()
            Options = Options.GetDefault()
            }
        Size = fun (m: ServiceDescriptorProto) ->
            0
            + Name.CalcFieldSize m.Name
            + Methods.CalcFieldSize m.Methods
            + Options.CalcFieldSize m.Options
        Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: ServiceDescriptorProto) ->
            Name.WriteField w m.Name
            Methods.WriteField w m.Methods
            Options.WriteField w m.Options
        Decode = fun (r: Google.Protobuf.CodedInputStream) ->
            let mutable builder = new Google.Protobuf.ServiceDescriptorProto.Builder()
            let mutable tag = 0
            while read r &tag do
                builder.Put (tag, r)
            builder.Build
        }
type ServiceDescriptorProto = {
    // Field Declarations
    Name: string // (1)
    Methods: Google.Protobuf.MethodDescriptorProto seq // (2)
    Options: Google.Protobuf.ServiceOptions option // (3)
    }
    with
    static member empty = _ServiceDescriptorProtoProto.Empty
    static member Proto = lazy _ServiceDescriptorProtoProto

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module MethodDescriptorProto =

    [<System.Runtime.CompilerServices.IsByRefLike>]
    type Builder =
        struct
            val mutable Name: string // (1)
            val mutable InputType: string // (2)
            val mutable OutputType: string // (3)
            val mutable Options: OptionBuilder<Google.Protobuf.MethodOptions> // (4)
            val mutable ClientStreaming: bool // (5)
            val mutable ServerStreaming: bool // (6)
        end
        with
        member x.Put ((tag, reader): int * Reader) =
            match tag with
            | 1 -> x.Name <- ValueCodec.String.ReadValue reader
            | 2 -> x.InputType <- ValueCodec.String.ReadValue reader
            | 3 -> x.OutputType <- ValueCodec.String.ReadValue reader
            | 4 -> x.Options.Set (ValueCodec.Message<Google.Protobuf.MethodOptions>.ReadValue reader)
            | 5 -> x.ClientStreaming <- ValueCodec.Bool.ReadValue reader
            | 6 -> x.ServerStreaming <- ValueCodec.Bool.ReadValue reader
            | _ -> reader.SkipLastField()
        member x.Build : Google.Protobuf.MethodDescriptorProto = {
            Name = x.Name |> orEmptyString
            InputType = x.InputType |> orEmptyString
            OutputType = x.OutputType |> orEmptyString
            Options = x.Options.Build
            ClientStreaming = x.ClientStreaming
            ServerStreaming = x.ServerStreaming
            }

let private _MethodDescriptorProtoProto : ProtoDef<MethodDescriptorProto> =
    // Field Definitions
    let Name = FieldCodec.Primitive ValueCodec.String 1
    let InputType = FieldCodec.Primitive ValueCodec.String 2
    let OutputType = FieldCodec.Primitive ValueCodec.String 3
    let Options = FieldCodec.Optional ValueCodec.Message<Google.Protobuf.MethodOptions> 4
    let ClientStreaming = FieldCodec.Primitive ValueCodec.Bool 5
    let ServerStreaming = FieldCodec.Primitive ValueCodec.Bool 6
    // Proto Definition Implementation
    { // ProtoDef<MethodDescriptorProto>
        Name = "MethodDescriptorProto"
        Empty = {
            Name = Name.GetDefault()
            InputType = InputType.GetDefault()
            OutputType = OutputType.GetDefault()
            Options = Options.GetDefault()
            ClientStreaming = ClientStreaming.GetDefault()
            ServerStreaming = ServerStreaming.GetDefault()
            }
        Size = fun (m: MethodDescriptorProto) ->
            0
            + Name.CalcFieldSize m.Name
            + InputType.CalcFieldSize m.InputType
            + OutputType.CalcFieldSize m.OutputType
            + Options.CalcFieldSize m.Options
            + ClientStreaming.CalcFieldSize m.ClientStreaming
            + ServerStreaming.CalcFieldSize m.ServerStreaming
        Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: MethodDescriptorProto) ->
            Name.WriteField w m.Name
            InputType.WriteField w m.InputType
            OutputType.WriteField w m.OutputType
            Options.WriteField w m.Options
            ClientStreaming.WriteField w m.ClientStreaming
            ServerStreaming.WriteField w m.ServerStreaming
        Decode = fun (r: Google.Protobuf.CodedInputStream) ->
            let mutable builder = new Google.Protobuf.MethodDescriptorProto.Builder()
            let mutable tag = 0
            while read r &tag do
                builder.Put (tag, r)
            builder.Build
        }
type MethodDescriptorProto = {
    // Field Declarations
    Name: string // (1)
    InputType: string // (2)
    OutputType: string // (3)
    Options: Google.Protobuf.MethodOptions option // (4)
    ClientStreaming: bool // (5)
    ServerStreaming: bool // (6)
    }
    with
    static member empty = _MethodDescriptorProtoProto.Empty
    static member Proto = lazy _MethodDescriptorProtoProto

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module FileOptions =

    type OptimizeMode =
    | Unspecified = 0
    | Speed = 1
    | CodeSize = 2
    | LiteRuntime = 3

    [<System.Runtime.CompilerServices.IsByRefLike>]
    type Builder =
        struct
            val mutable JavaPackage: string // (1)
            val mutable JavaOuterClassname: string // (8)
            val mutable JavaMultipleFiles: bool // (10)
            val mutable JavaGenerateEqualsAndHash: bool // (20)
            val mutable JavaStringCheckUtf8: bool // (27)
            val mutable OptimizeFor: Google.Protobuf.FileOptions.OptimizeMode // (9)
            val mutable GoPackage: string // (11)
            val mutable CcGenericServices: bool // (16)
            val mutable JavaGenericServices: bool // (17)
            val mutable PyGenericServices: bool // (18)
            val mutable PhpGenericServices: bool // (42)
            val mutable Deprecated: bool // (23)
            val mutable CcEnableArenas: bool // (31)
            val mutable ObjcClassPrefix: string // (36)
            val mutable CsharpNamespace: string // (37)
            val mutable SwiftPrefix: string // (39)
            val mutable PhpClassPrefix: string // (40)
            val mutable PhpNamespace: string // (41)
            val mutable PhpMetadataNamespace: string // (44)
            val mutable RubyPackage: string // (45)
            val mutable UninterpretedOptions: RepeatedBuilder<Google.Protobuf.UninterpretedOption> // (999)
        end
        with
        member x.Put ((tag, reader): int * Reader) =
            match tag with
            | 1 -> x.JavaPackage <- ValueCodec.String.ReadValue reader
            | 8 -> x.JavaOuterClassname <- ValueCodec.String.ReadValue reader
            | 10 -> x.JavaMultipleFiles <- ValueCodec.Bool.ReadValue reader
            | 20 -> x.JavaGenerateEqualsAndHash <- ValueCodec.Bool.ReadValue reader
            | 27 -> x.JavaStringCheckUtf8 <- ValueCodec.Bool.ReadValue reader
            | 9 -> x.OptimizeFor <- ValueCodec.Enum<Google.Protobuf.FileOptions.OptimizeMode>.ReadValue reader
            | 11 -> x.GoPackage <- ValueCodec.String.ReadValue reader
            | 16 -> x.CcGenericServices <- ValueCodec.Bool.ReadValue reader
            | 17 -> x.JavaGenericServices <- ValueCodec.Bool.ReadValue reader
            | 18 -> x.PyGenericServices <- ValueCodec.Bool.ReadValue reader
            | 42 -> x.PhpGenericServices <- ValueCodec.Bool.ReadValue reader
            | 23 -> x.Deprecated <- ValueCodec.Bool.ReadValue reader
            | 31 -> x.CcEnableArenas <- ValueCodec.Bool.ReadValue reader
            | 36 -> x.ObjcClassPrefix <- ValueCodec.String.ReadValue reader
            | 37 -> x.CsharpNamespace <- ValueCodec.String.ReadValue reader
            | 39 -> x.SwiftPrefix <- ValueCodec.String.ReadValue reader
            | 40 -> x.PhpClassPrefix <- ValueCodec.String.ReadValue reader
            | 41 -> x.PhpNamespace <- ValueCodec.String.ReadValue reader
            | 44 -> x.PhpMetadataNamespace <- ValueCodec.String.ReadValue reader
            | 45 -> x.RubyPackage <- ValueCodec.String.ReadValue reader
            | 999 -> x.UninterpretedOptions.Add (ValueCodec.Message<Google.Protobuf.UninterpretedOption>.ReadValue reader)
            | _ -> reader.SkipLastField()
        member x.Build : Google.Protobuf.FileOptions = {
            JavaPackage = x.JavaPackage |> orEmptyString
            JavaOuterClassname = x.JavaOuterClassname |> orEmptyString
            JavaMultipleFiles = x.JavaMultipleFiles
            JavaGenerateEqualsAndHash = x.JavaGenerateEqualsAndHash
            JavaStringCheckUtf8 = x.JavaStringCheckUtf8
            OptimizeFor = x.OptimizeFor
            GoPackage = x.GoPackage |> orEmptyString
            CcGenericServices = x.CcGenericServices
            JavaGenericServices = x.JavaGenericServices
            PyGenericServices = x.PyGenericServices
            PhpGenericServices = x.PhpGenericServices
            Deprecated = x.Deprecated
            CcEnableArenas = x.CcEnableArenas
            ObjcClassPrefix = x.ObjcClassPrefix |> orEmptyString
            CsharpNamespace = x.CsharpNamespace |> orEmptyString
            SwiftPrefix = x.SwiftPrefix |> orEmptyString
            PhpClassPrefix = x.PhpClassPrefix |> orEmptyString
            PhpNamespace = x.PhpNamespace |> orEmptyString
            PhpMetadataNamespace = x.PhpMetadataNamespace |> orEmptyString
            RubyPackage = x.RubyPackage |> orEmptyString
            UninterpretedOptions = x.UninterpretedOptions.Build
            }

let private _FileOptionsProto : ProtoDef<FileOptions> =
    // Field Definitions
    let JavaPackage = FieldCodec.Primitive ValueCodec.String 1
    let JavaOuterClassname = FieldCodec.Primitive ValueCodec.String 8
    let JavaMultipleFiles = FieldCodec.Primitive ValueCodec.Bool 10
    let JavaGenerateEqualsAndHash = FieldCodec.Primitive ValueCodec.Bool 20
    let JavaStringCheckUtf8 = FieldCodec.Primitive ValueCodec.Bool 27
    let OptimizeFor = FieldCodec.Primitive ValueCodec.Enum<Google.Protobuf.FileOptions.OptimizeMode> 9
    let GoPackage = FieldCodec.Primitive ValueCodec.String 11
    let CcGenericServices = FieldCodec.Primitive ValueCodec.Bool 16
    let JavaGenericServices = FieldCodec.Primitive ValueCodec.Bool 17
    let PyGenericServices = FieldCodec.Primitive ValueCodec.Bool 18
    let PhpGenericServices = FieldCodec.Primitive ValueCodec.Bool 42
    let Deprecated = FieldCodec.Primitive ValueCodec.Bool 23
    let CcEnableArenas = FieldCodec.Primitive ValueCodec.Bool 31
    let ObjcClassPrefix = FieldCodec.Primitive ValueCodec.String 36
    let CsharpNamespace = FieldCodec.Primitive ValueCodec.String 37
    let SwiftPrefix = FieldCodec.Primitive ValueCodec.String 39
    let PhpClassPrefix = FieldCodec.Primitive ValueCodec.String 40
    let PhpNamespace = FieldCodec.Primitive ValueCodec.String 41
    let PhpMetadataNamespace = FieldCodec.Primitive ValueCodec.String 44
    let RubyPackage = FieldCodec.Primitive ValueCodec.String 45
    let UninterpretedOptions = FieldCodec.Repeated ValueCodec.Message<Google.Protobuf.UninterpretedOption> 999
    // Proto Definition Implementation
    { // ProtoDef<FileOptions>
        Name = "FileOptions"
        Empty = {
            JavaPackage = JavaPackage.GetDefault()
            JavaOuterClassname = JavaOuterClassname.GetDefault()
            JavaMultipleFiles = JavaMultipleFiles.GetDefault()
            JavaGenerateEqualsAndHash = JavaGenerateEqualsAndHash.GetDefault()
            JavaStringCheckUtf8 = JavaStringCheckUtf8.GetDefault()
            OptimizeFor = OptimizeFor.GetDefault()
            GoPackage = GoPackage.GetDefault()
            CcGenericServices = CcGenericServices.GetDefault()
            JavaGenericServices = JavaGenericServices.GetDefault()
            PyGenericServices = PyGenericServices.GetDefault()
            PhpGenericServices = PhpGenericServices.GetDefault()
            Deprecated = Deprecated.GetDefault()
            CcEnableArenas = CcEnableArenas.GetDefault()
            ObjcClassPrefix = ObjcClassPrefix.GetDefault()
            CsharpNamespace = CsharpNamespace.GetDefault()
            SwiftPrefix = SwiftPrefix.GetDefault()
            PhpClassPrefix = PhpClassPrefix.GetDefault()
            PhpNamespace = PhpNamespace.GetDefault()
            PhpMetadataNamespace = PhpMetadataNamespace.GetDefault()
            RubyPackage = RubyPackage.GetDefault()
            UninterpretedOptions = UninterpretedOptions.GetDefault()
            }
        Size = fun (m: FileOptions) ->
            0
            + JavaPackage.CalcFieldSize m.JavaPackage
            + JavaOuterClassname.CalcFieldSize m.JavaOuterClassname
            + JavaMultipleFiles.CalcFieldSize m.JavaMultipleFiles
            + JavaGenerateEqualsAndHash.CalcFieldSize m.JavaGenerateEqualsAndHash
            + JavaStringCheckUtf8.CalcFieldSize m.JavaStringCheckUtf8
            + OptimizeFor.CalcFieldSize m.OptimizeFor
            + GoPackage.CalcFieldSize m.GoPackage
            + CcGenericServices.CalcFieldSize m.CcGenericServices
            + JavaGenericServices.CalcFieldSize m.JavaGenericServices
            + PyGenericServices.CalcFieldSize m.PyGenericServices
            + PhpGenericServices.CalcFieldSize m.PhpGenericServices
            + Deprecated.CalcFieldSize m.Deprecated
            + CcEnableArenas.CalcFieldSize m.CcEnableArenas
            + ObjcClassPrefix.CalcFieldSize m.ObjcClassPrefix
            + CsharpNamespace.CalcFieldSize m.CsharpNamespace
            + SwiftPrefix.CalcFieldSize m.SwiftPrefix
            + PhpClassPrefix.CalcFieldSize m.PhpClassPrefix
            + PhpNamespace.CalcFieldSize m.PhpNamespace
            + PhpMetadataNamespace.CalcFieldSize m.PhpMetadataNamespace
            + RubyPackage.CalcFieldSize m.RubyPackage
            + UninterpretedOptions.CalcFieldSize m.UninterpretedOptions
        Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: FileOptions) ->
            JavaPackage.WriteField w m.JavaPackage
            JavaOuterClassname.WriteField w m.JavaOuterClassname
            JavaMultipleFiles.WriteField w m.JavaMultipleFiles
            JavaGenerateEqualsAndHash.WriteField w m.JavaGenerateEqualsAndHash
            JavaStringCheckUtf8.WriteField w m.JavaStringCheckUtf8
            OptimizeFor.WriteField w m.OptimizeFor
            GoPackage.WriteField w m.GoPackage
            CcGenericServices.WriteField w m.CcGenericServices
            JavaGenericServices.WriteField w m.JavaGenericServices
            PyGenericServices.WriteField w m.PyGenericServices
            PhpGenericServices.WriteField w m.PhpGenericServices
            Deprecated.WriteField w m.Deprecated
            CcEnableArenas.WriteField w m.CcEnableArenas
            ObjcClassPrefix.WriteField w m.ObjcClassPrefix
            CsharpNamespace.WriteField w m.CsharpNamespace
            SwiftPrefix.WriteField w m.SwiftPrefix
            PhpClassPrefix.WriteField w m.PhpClassPrefix
            PhpNamespace.WriteField w m.PhpNamespace
            PhpMetadataNamespace.WriteField w m.PhpMetadataNamespace
            RubyPackage.WriteField w m.RubyPackage
            UninterpretedOptions.WriteField w m.UninterpretedOptions
        Decode = fun (r: Google.Protobuf.CodedInputStream) ->
            let mutable builder = new Google.Protobuf.FileOptions.Builder()
            let mutable tag = 0
            while read r &tag do
                builder.Put (tag, r)
            builder.Build
        }
type FileOptions = {
    // Field Declarations
    JavaPackage: string // (1)
    JavaOuterClassname: string // (8)
    JavaMultipleFiles: bool // (10)
    JavaGenerateEqualsAndHash: bool // (20)
    JavaStringCheckUtf8: bool // (27)
    OptimizeFor: Google.Protobuf.FileOptions.OptimizeMode // (9)
    GoPackage: string // (11)
    CcGenericServices: bool // (16)
    JavaGenericServices: bool // (17)
    PyGenericServices: bool // (18)
    PhpGenericServices: bool // (42)
    Deprecated: bool // (23)
    CcEnableArenas: bool // (31)
    ObjcClassPrefix: string // (36)
    CsharpNamespace: string // (37)
    SwiftPrefix: string // (39)
    PhpClassPrefix: string // (40)
    PhpNamespace: string // (41)
    PhpMetadataNamespace: string // (44)
    RubyPackage: string // (45)
    UninterpretedOptions: Google.Protobuf.UninterpretedOption seq // (999)
    }
    with
    static member empty = _FileOptionsProto.Empty
    static member Proto = lazy _FileOptionsProto

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module MessageOptions =

    [<System.Runtime.CompilerServices.IsByRefLike>]
    type Builder =
        struct
            val mutable MessageSetWireFormat: bool // (1)
            val mutable NoStandardDescriptorAccessor: bool // (2)
            val mutable Deprecated: bool // (3)
            val mutable MapEntry: bool // (7)
            val mutable UninterpretedOptions: RepeatedBuilder<Google.Protobuf.UninterpretedOption> // (999)
        end
        with
        member x.Put ((tag, reader): int * Reader) =
            match tag with
            | 1 -> x.MessageSetWireFormat <- ValueCodec.Bool.ReadValue reader
            | 2 -> x.NoStandardDescriptorAccessor <- ValueCodec.Bool.ReadValue reader
            | 3 -> x.Deprecated <- ValueCodec.Bool.ReadValue reader
            | 7 -> x.MapEntry <- ValueCodec.Bool.ReadValue reader
            | 999 -> x.UninterpretedOptions.Add (ValueCodec.Message<Google.Protobuf.UninterpretedOption>.ReadValue reader)
            | _ -> reader.SkipLastField()
        member x.Build : Google.Protobuf.MessageOptions = {
            MessageSetWireFormat = x.MessageSetWireFormat
            NoStandardDescriptorAccessor = x.NoStandardDescriptorAccessor
            Deprecated = x.Deprecated
            MapEntry = x.MapEntry
            UninterpretedOptions = x.UninterpretedOptions.Build
            }

let private _MessageOptionsProto : ProtoDef<MessageOptions> =
    // Field Definitions
    let MessageSetWireFormat = FieldCodec.Primitive ValueCodec.Bool 1
    let NoStandardDescriptorAccessor = FieldCodec.Primitive ValueCodec.Bool 2
    let Deprecated = FieldCodec.Primitive ValueCodec.Bool 3
    let MapEntry = FieldCodec.Primitive ValueCodec.Bool 7
    let UninterpretedOptions = FieldCodec.Repeated ValueCodec.Message<Google.Protobuf.UninterpretedOption> 999
    // Proto Definition Implementation
    { // ProtoDef<MessageOptions>
        Name = "MessageOptions"
        Empty = {
            MessageSetWireFormat = MessageSetWireFormat.GetDefault()
            NoStandardDescriptorAccessor = NoStandardDescriptorAccessor.GetDefault()
            Deprecated = Deprecated.GetDefault()
            MapEntry = MapEntry.GetDefault()
            UninterpretedOptions = UninterpretedOptions.GetDefault()
            }
        Size = fun (m: MessageOptions) ->
            0
            + MessageSetWireFormat.CalcFieldSize m.MessageSetWireFormat
            + NoStandardDescriptorAccessor.CalcFieldSize m.NoStandardDescriptorAccessor
            + Deprecated.CalcFieldSize m.Deprecated
            + MapEntry.CalcFieldSize m.MapEntry
            + UninterpretedOptions.CalcFieldSize m.UninterpretedOptions
        Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: MessageOptions) ->
            MessageSetWireFormat.WriteField w m.MessageSetWireFormat
            NoStandardDescriptorAccessor.WriteField w m.NoStandardDescriptorAccessor
            Deprecated.WriteField w m.Deprecated
            MapEntry.WriteField w m.MapEntry
            UninterpretedOptions.WriteField w m.UninterpretedOptions
        Decode = fun (r: Google.Protobuf.CodedInputStream) ->
            let mutable builder = new Google.Protobuf.MessageOptions.Builder()
            let mutable tag = 0
            while read r &tag do
                builder.Put (tag, r)
            builder.Build
        }
type MessageOptions = {
    // Field Declarations
    MessageSetWireFormat: bool // (1)
    NoStandardDescriptorAccessor: bool // (2)
    Deprecated: bool // (3)
    MapEntry: bool // (7)
    UninterpretedOptions: Google.Protobuf.UninterpretedOption seq // (999)
    }
    with
    static member empty = _MessageOptionsProto.Empty
    static member Proto = lazy _MessageOptionsProto

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module FieldOptions =

    type CType =
    | String = 0
    | Cord = 1
    | StringPiece = 2

    type JSType =
    | JsNormal = 0
    | JsString = 1
    | JsNumber = 2

    [<System.Runtime.CompilerServices.IsByRefLike>]
    type Builder =
        struct
            val mutable Ctype: Google.Protobuf.FieldOptions.CType // (1)
            val mutable Packed: bool // (2)
            val mutable Jstype: Google.Protobuf.FieldOptions.JSType // (6)
            val mutable Lazy: bool // (5)
            val mutable Deprecated: bool // (3)
            val mutable Weak: bool // (10)
            val mutable UninterpretedOptions: RepeatedBuilder<Google.Protobuf.UninterpretedOption> // (999)
        end
        with
        member x.Put ((tag, reader): int * Reader) =
            match tag with
            | 1 -> x.Ctype <- ValueCodec.Enum<Google.Protobuf.FieldOptions.CType>.ReadValue reader
            | 2 -> x.Packed <- ValueCodec.Bool.ReadValue reader
            | 6 -> x.Jstype <- ValueCodec.Enum<Google.Protobuf.FieldOptions.JSType>.ReadValue reader
            | 5 -> x.Lazy <- ValueCodec.Bool.ReadValue reader
            | 3 -> x.Deprecated <- ValueCodec.Bool.ReadValue reader
            | 10 -> x.Weak <- ValueCodec.Bool.ReadValue reader
            | 999 -> x.UninterpretedOptions.Add (ValueCodec.Message<Google.Protobuf.UninterpretedOption>.ReadValue reader)
            | _ -> reader.SkipLastField()
        member x.Build : Google.Protobuf.FieldOptions = {
            Ctype = x.Ctype
            Packed = x.Packed
            Jstype = x.Jstype
            Lazy = x.Lazy
            Deprecated = x.Deprecated
            Weak = x.Weak
            UninterpretedOptions = x.UninterpretedOptions.Build
            }

let private _FieldOptionsProto : ProtoDef<FieldOptions> =
    // Field Definitions
    let Ctype = FieldCodec.Primitive ValueCodec.Enum<Google.Protobuf.FieldOptions.CType> 1
    let Packed = FieldCodec.Primitive ValueCodec.Bool 2
    let Jstype = FieldCodec.Primitive ValueCodec.Enum<Google.Protobuf.FieldOptions.JSType> 6
    let Lazy = FieldCodec.Primitive ValueCodec.Bool 5
    let Deprecated = FieldCodec.Primitive ValueCodec.Bool 3
    let Weak = FieldCodec.Primitive ValueCodec.Bool 10
    let UninterpretedOptions = FieldCodec.Repeated ValueCodec.Message<Google.Protobuf.UninterpretedOption> 999
    // Proto Definition Implementation
    { // ProtoDef<FieldOptions>
        Name = "FieldOptions"
        Empty = {
            Ctype = Ctype.GetDefault()
            Packed = Packed.GetDefault()
            Jstype = Jstype.GetDefault()
            Lazy = Lazy.GetDefault()
            Deprecated = Deprecated.GetDefault()
            Weak = Weak.GetDefault()
            UninterpretedOptions = UninterpretedOptions.GetDefault()
            }
        Size = fun (m: FieldOptions) ->
            0
            + Ctype.CalcFieldSize m.Ctype
            + Packed.CalcFieldSize m.Packed
            + Jstype.CalcFieldSize m.Jstype
            + Lazy.CalcFieldSize m.Lazy
            + Deprecated.CalcFieldSize m.Deprecated
            + Weak.CalcFieldSize m.Weak
            + UninterpretedOptions.CalcFieldSize m.UninterpretedOptions
        Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: FieldOptions) ->
            Ctype.WriteField w m.Ctype
            Packed.WriteField w m.Packed
            Jstype.WriteField w m.Jstype
            Lazy.WriteField w m.Lazy
            Deprecated.WriteField w m.Deprecated
            Weak.WriteField w m.Weak
            UninterpretedOptions.WriteField w m.UninterpretedOptions
        Decode = fun (r: Google.Protobuf.CodedInputStream) ->
            let mutable builder = new Google.Protobuf.FieldOptions.Builder()
            let mutable tag = 0
            while read r &tag do
                builder.Put (tag, r)
            builder.Build
        }
type FieldOptions = {
    // Field Declarations
    Ctype: Google.Protobuf.FieldOptions.CType // (1)
    Packed: bool // (2)
    Jstype: Google.Protobuf.FieldOptions.JSType // (6)
    Lazy: bool // (5)
    Deprecated: bool // (3)
    Weak: bool // (10)
    UninterpretedOptions: Google.Protobuf.UninterpretedOption seq // (999)
    }
    with
    static member empty = _FieldOptionsProto.Empty
    static member Proto = lazy _FieldOptionsProto

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module OneofOptions =

    [<System.Runtime.CompilerServices.IsByRefLike>]
    type Builder =
        struct
            val mutable UninterpretedOptions: RepeatedBuilder<Google.Protobuf.UninterpretedOption> // (999)
        end
        with
        member x.Put ((tag, reader): int * Reader) =
            match tag with
            | 999 -> x.UninterpretedOptions.Add (ValueCodec.Message<Google.Protobuf.UninterpretedOption>.ReadValue reader)
            | _ -> reader.SkipLastField()
        member x.Build : Google.Protobuf.OneofOptions = {
            UninterpretedOptions = x.UninterpretedOptions.Build
            }

let private _OneofOptionsProto : ProtoDef<OneofOptions> =
    // Field Definitions
    let UninterpretedOptions = FieldCodec.Repeated ValueCodec.Message<Google.Protobuf.UninterpretedOption> 999
    // Proto Definition Implementation
    { // ProtoDef<OneofOptions>
        Name = "OneofOptions"
        Empty = {
            UninterpretedOptions = UninterpretedOptions.GetDefault()
            }
        Size = fun (m: OneofOptions) ->
            0
            + UninterpretedOptions.CalcFieldSize m.UninterpretedOptions
        Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: OneofOptions) ->
            UninterpretedOptions.WriteField w m.UninterpretedOptions
        Decode = fun (r: Google.Protobuf.CodedInputStream) ->
            let mutable builder = new Google.Protobuf.OneofOptions.Builder()
            let mutable tag = 0
            while read r &tag do
                builder.Put (tag, r)
            builder.Build
        }
type OneofOptions = {
    // Field Declarations
    UninterpretedOptions: Google.Protobuf.UninterpretedOption seq // (999)
    }
    with
    static member empty = _OneofOptionsProto.Empty
    static member Proto = lazy _OneofOptionsProto

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module EnumOptions =

    [<System.Runtime.CompilerServices.IsByRefLike>]
    type Builder =
        struct
            val mutable AllowAlias: bool // (2)
            val mutable Deprecated: bool // (3)
            val mutable UninterpretedOptions: RepeatedBuilder<Google.Protobuf.UninterpretedOption> // (999)
        end
        with
        member x.Put ((tag, reader): int * Reader) =
            match tag with
            | 2 -> x.AllowAlias <- ValueCodec.Bool.ReadValue reader
            | 3 -> x.Deprecated <- ValueCodec.Bool.ReadValue reader
            | 999 -> x.UninterpretedOptions.Add (ValueCodec.Message<Google.Protobuf.UninterpretedOption>.ReadValue reader)
            | _ -> reader.SkipLastField()
        member x.Build : Google.Protobuf.EnumOptions = {
            AllowAlias = x.AllowAlias
            Deprecated = x.Deprecated
            UninterpretedOptions = x.UninterpretedOptions.Build
            }

let private _EnumOptionsProto : ProtoDef<EnumOptions> =
    // Field Definitions
    let AllowAlias = FieldCodec.Primitive ValueCodec.Bool 2
    let Deprecated = FieldCodec.Primitive ValueCodec.Bool 3
    let UninterpretedOptions = FieldCodec.Repeated ValueCodec.Message<Google.Protobuf.UninterpretedOption> 999
    // Proto Definition Implementation
    { // ProtoDef<EnumOptions>
        Name = "EnumOptions"
        Empty = {
            AllowAlias = AllowAlias.GetDefault()
            Deprecated = Deprecated.GetDefault()
            UninterpretedOptions = UninterpretedOptions.GetDefault()
            }
        Size = fun (m: EnumOptions) ->
            0
            + AllowAlias.CalcFieldSize m.AllowAlias
            + Deprecated.CalcFieldSize m.Deprecated
            + UninterpretedOptions.CalcFieldSize m.UninterpretedOptions
        Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: EnumOptions) ->
            AllowAlias.WriteField w m.AllowAlias
            Deprecated.WriteField w m.Deprecated
            UninterpretedOptions.WriteField w m.UninterpretedOptions
        Decode = fun (r: Google.Protobuf.CodedInputStream) ->
            let mutable builder = new Google.Protobuf.EnumOptions.Builder()
            let mutable tag = 0
            while read r &tag do
                builder.Put (tag, r)
            builder.Build
        }
type EnumOptions = {
    // Field Declarations
    AllowAlias: bool // (2)
    Deprecated: bool // (3)
    UninterpretedOptions: Google.Protobuf.UninterpretedOption seq // (999)
    }
    with
    static member empty = _EnumOptionsProto.Empty
    static member Proto = lazy _EnumOptionsProto

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module EnumValueOptions =

    [<System.Runtime.CompilerServices.IsByRefLike>]
    type Builder =
        struct
            val mutable Deprecated: bool // (1)
            val mutable UninterpretedOptions: RepeatedBuilder<Google.Protobuf.UninterpretedOption> // (999)
        end
        with
        member x.Put ((tag, reader): int * Reader) =
            match tag with
            | 1 -> x.Deprecated <- ValueCodec.Bool.ReadValue reader
            | 999 -> x.UninterpretedOptions.Add (ValueCodec.Message<Google.Protobuf.UninterpretedOption>.ReadValue reader)
            | _ -> reader.SkipLastField()
        member x.Build : Google.Protobuf.EnumValueOptions = {
            Deprecated = x.Deprecated
            UninterpretedOptions = x.UninterpretedOptions.Build
            }

let private _EnumValueOptionsProto : ProtoDef<EnumValueOptions> =
    // Field Definitions
    let Deprecated = FieldCodec.Primitive ValueCodec.Bool 1
    let UninterpretedOptions = FieldCodec.Repeated ValueCodec.Message<Google.Protobuf.UninterpretedOption> 999
    // Proto Definition Implementation
    { // ProtoDef<EnumValueOptions>
        Name = "EnumValueOptions"
        Empty = {
            Deprecated = Deprecated.GetDefault()
            UninterpretedOptions = UninterpretedOptions.GetDefault()
            }
        Size = fun (m: EnumValueOptions) ->
            0
            + Deprecated.CalcFieldSize m.Deprecated
            + UninterpretedOptions.CalcFieldSize m.UninterpretedOptions
        Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: EnumValueOptions) ->
            Deprecated.WriteField w m.Deprecated
            UninterpretedOptions.WriteField w m.UninterpretedOptions
        Decode = fun (r: Google.Protobuf.CodedInputStream) ->
            let mutable builder = new Google.Protobuf.EnumValueOptions.Builder()
            let mutable tag = 0
            while read r &tag do
                builder.Put (tag, r)
            builder.Build
        }
type EnumValueOptions = {
    // Field Declarations
    Deprecated: bool // (1)
    UninterpretedOptions: Google.Protobuf.UninterpretedOption seq // (999)
    }
    with
    static member empty = _EnumValueOptionsProto.Empty
    static member Proto = lazy _EnumValueOptionsProto

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module ServiceOptions =

    [<System.Runtime.CompilerServices.IsByRefLike>]
    type Builder =
        struct
            val mutable Deprecated: bool // (33)
            val mutable UninterpretedOptions: RepeatedBuilder<Google.Protobuf.UninterpretedOption> // (999)
        end
        with
        member x.Put ((tag, reader): int * Reader) =
            match tag with
            | 33 -> x.Deprecated <- ValueCodec.Bool.ReadValue reader
            | 999 -> x.UninterpretedOptions.Add (ValueCodec.Message<Google.Protobuf.UninterpretedOption>.ReadValue reader)
            | _ -> reader.SkipLastField()
        member x.Build : Google.Protobuf.ServiceOptions = {
            Deprecated = x.Deprecated
            UninterpretedOptions = x.UninterpretedOptions.Build
            }

let private _ServiceOptionsProto : ProtoDef<ServiceOptions> =
    // Field Definitions
    let Deprecated = FieldCodec.Primitive ValueCodec.Bool 33
    let UninterpretedOptions = FieldCodec.Repeated ValueCodec.Message<Google.Protobuf.UninterpretedOption> 999
    // Proto Definition Implementation
    { // ProtoDef<ServiceOptions>
        Name = "ServiceOptions"
        Empty = {
            Deprecated = Deprecated.GetDefault()
            UninterpretedOptions = UninterpretedOptions.GetDefault()
            }
        Size = fun (m: ServiceOptions) ->
            0
            + Deprecated.CalcFieldSize m.Deprecated
            + UninterpretedOptions.CalcFieldSize m.UninterpretedOptions
        Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: ServiceOptions) ->
            Deprecated.WriteField w m.Deprecated
            UninterpretedOptions.WriteField w m.UninterpretedOptions
        Decode = fun (r: Google.Protobuf.CodedInputStream) ->
            let mutable builder = new Google.Protobuf.ServiceOptions.Builder()
            let mutable tag = 0
            while read r &tag do
                builder.Put (tag, r)
            builder.Build
        }
type ServiceOptions = {
    // Field Declarations
    Deprecated: bool // (33)
    UninterpretedOptions: Google.Protobuf.UninterpretedOption seq // (999)
    }
    with
    static member empty = _ServiceOptionsProto.Empty
    static member Proto = lazy _ServiceOptionsProto

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module MethodOptions =

    type IdempotencyLevel =
    | IdempotencyUnknown = 0
    | NoSideEffects = 1
    | Idempotent = 2

    [<System.Runtime.CompilerServices.IsByRefLike>]
    type Builder =
        struct
            val mutable Deprecated: bool // (33)
            val mutable IdempotencyLevel: Google.Protobuf.MethodOptions.IdempotencyLevel // (34)
            val mutable UninterpretedOptions: RepeatedBuilder<Google.Protobuf.UninterpretedOption> // (999)
        end
        with
        member x.Put ((tag, reader): int * Reader) =
            match tag with
            | 33 -> x.Deprecated <- ValueCodec.Bool.ReadValue reader
            | 34 -> x.IdempotencyLevel <- ValueCodec.Enum<Google.Protobuf.MethodOptions.IdempotencyLevel>.ReadValue reader
            | 999 -> x.UninterpretedOptions.Add (ValueCodec.Message<Google.Protobuf.UninterpretedOption>.ReadValue reader)
            | _ -> reader.SkipLastField()
        member x.Build : Google.Protobuf.MethodOptions = {
            Deprecated = x.Deprecated
            IdempotencyLevel = x.IdempotencyLevel
            UninterpretedOptions = x.UninterpretedOptions.Build
            }

let private _MethodOptionsProto : ProtoDef<MethodOptions> =
    // Field Definitions
    let Deprecated = FieldCodec.Primitive ValueCodec.Bool 33
    let IdempotencyLevel = FieldCodec.Primitive ValueCodec.Enum<Google.Protobuf.MethodOptions.IdempotencyLevel> 34
    let UninterpretedOptions = FieldCodec.Repeated ValueCodec.Message<Google.Protobuf.UninterpretedOption> 999
    // Proto Definition Implementation
    { // ProtoDef<MethodOptions>
        Name = "MethodOptions"
        Empty = {
            Deprecated = Deprecated.GetDefault()
            IdempotencyLevel = IdempotencyLevel.GetDefault()
            UninterpretedOptions = UninterpretedOptions.GetDefault()
            }
        Size = fun (m: MethodOptions) ->
            0
            + Deprecated.CalcFieldSize m.Deprecated
            + IdempotencyLevel.CalcFieldSize m.IdempotencyLevel
            + UninterpretedOptions.CalcFieldSize m.UninterpretedOptions
        Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: MethodOptions) ->
            Deprecated.WriteField w m.Deprecated
            IdempotencyLevel.WriteField w m.IdempotencyLevel
            UninterpretedOptions.WriteField w m.UninterpretedOptions
        Decode = fun (r: Google.Protobuf.CodedInputStream) ->
            let mutable builder = new Google.Protobuf.MethodOptions.Builder()
            let mutable tag = 0
            while read r &tag do
                builder.Put (tag, r)
            builder.Build
        }
type MethodOptions = {
    // Field Declarations
    Deprecated: bool // (33)
    IdempotencyLevel: Google.Protobuf.MethodOptions.IdempotencyLevel // (34)
    UninterpretedOptions: Google.Protobuf.UninterpretedOption seq // (999)
    }
    with
    static member empty = _MethodOptionsProto.Empty
    static member Proto = lazy _MethodOptionsProto

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module UninterpretedOption =

    [<RequireQualifiedAccess>]
    type ValueCase =
    | None
    | IdentifierValue of string
    | PositiveIntValue of uint64
    | NegativeIntValue of int64
    | DoubleValue of double
    | StringValue of Google.Protobuf.ByteString
    | AggregateValue of string

    [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
    module NamePart =

        [<System.Runtime.CompilerServices.IsByRefLike>]
        type Builder =
            struct
                val mutable NamePart: string // (1)
                val mutable IsExtension: bool // (2)
            end
            with
            member x.Put ((tag, reader): int * Reader) =
                match tag with
                | 1 -> x.NamePart <- ValueCodec.String.ReadValue reader
                | 2 -> x.IsExtension <- ValueCodec.Bool.ReadValue reader
                | _ -> reader.SkipLastField()
            member x.Build : Google.Protobuf.UninterpretedOption.NamePart = {
                NamePart = x.NamePart |> orEmptyString
                IsExtension = x.IsExtension
                }

    let private _NamePartProto : ProtoDef<NamePart> =
        // Field Definitions
        let NamePart = FieldCodec.Primitive ValueCodec.String 1
        let IsExtension = FieldCodec.Primitive ValueCodec.Bool 2
        // Proto Definition Implementation
        { // ProtoDef<NamePart>
            Name = "NamePart"
            Empty = {
                NamePart = NamePart.GetDefault()
                IsExtension = IsExtension.GetDefault()
                }
            Size = fun (m: NamePart) ->
                0
                + NamePart.CalcFieldSize m.NamePart
                + IsExtension.CalcFieldSize m.IsExtension
            Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: NamePart) ->
                NamePart.WriteField w m.NamePart
                IsExtension.WriteField w m.IsExtension
            Decode = fun (r: Google.Protobuf.CodedInputStream) ->
                let mutable builder = new Google.Protobuf.UninterpretedOption.NamePart.Builder()
                let mutable tag = 0
                while read r &tag do
                    builder.Put (tag, r)
                builder.Build
            }
    type NamePart = {
        // Field Declarations
        NamePart: string // (1)
        IsExtension: bool // (2)
        }
        with
        static member empty = _NamePartProto.Empty
        static member Proto = lazy _NamePartProto

    [<System.Runtime.CompilerServices.IsByRefLike>]
    type Builder =
        struct
            val mutable Names: RepeatedBuilder<Google.Protobuf.UninterpretedOption.NamePart> // (2)
            val mutable Value: OptionBuilder<Google.Protobuf.UninterpretedOption.ValueCase>
        end
        with
        member x.Put ((tag, reader): int * Reader) =
            match tag with
            | 2 -> x.Names.Add (ValueCodec.Message<Google.Protobuf.UninterpretedOption.NamePart>.ReadValue reader)
            | 3 -> x.Value.Set (ValueCase.IdentifierValue (ValueCodec.String.ReadValue reader))
            | 4 -> x.Value.Set (ValueCase.PositiveIntValue (ValueCodec.UInt64.ReadValue reader))
            | 5 -> x.Value.Set (ValueCase.NegativeIntValue (ValueCodec.Int64.ReadValue reader))
            | 6 -> x.Value.Set (ValueCase.DoubleValue (ValueCodec.Double.ReadValue reader))
            | 7 -> x.Value.Set (ValueCase.StringValue (ValueCodec.Bytes.ReadValue reader))
            | 8 -> x.Value.Set (ValueCase.AggregateValue (ValueCodec.String.ReadValue reader))
            | _ -> reader.SkipLastField()
        member x.Build : Google.Protobuf.UninterpretedOption = {
            Names = x.Names.Build
            Value = x.Value.Build |> (Option.defaultValue ValueCase.None)
            }

let private _UninterpretedOptionProto : ProtoDef<UninterpretedOption> =
    // Field Definitions
    let Names = FieldCodec.Repeated ValueCodec.Message<Google.Protobuf.UninterpretedOption.NamePart> 2
    let IdentifierValue = FieldCodec.Optional ValueCodec.String (* oneof value *) 3
    let PositiveIntValue = FieldCodec.Optional ValueCodec.UInt64 (* oneof value *) 4
    let NegativeIntValue = FieldCodec.Optional ValueCodec.Int64 (* oneof value *) 5
    let DoubleValue = FieldCodec.Optional ValueCodec.Double (* oneof value *) 6
    let StringValue = FieldCodec.Optional ValueCodec.Bytes (* oneof value *) 7
    let AggregateValue = FieldCodec.Optional ValueCodec.String (* oneof value *) 8
    // Proto Definition Implementation
    { // ProtoDef<UninterpretedOption>
        Name = "UninterpretedOption"
        Empty = {
            Names = Names.GetDefault()
            Value = Google.Protobuf.UninterpretedOption.ValueCase.None
            }
        Size = fun (m: UninterpretedOption) ->
            0
            + Names.CalcFieldSize m.Names
            + match m.Value with
                | Google.Protobuf.UninterpretedOption.ValueCase.None -> 0
                | Google.Protobuf.UninterpretedOption.ValueCase.IdentifierValue v -> IdentifierValue.CalcFieldSize (Some v)
                | Google.Protobuf.UninterpretedOption.ValueCase.PositiveIntValue v -> PositiveIntValue.CalcFieldSize (Some v)
                | Google.Protobuf.UninterpretedOption.ValueCase.NegativeIntValue v -> NegativeIntValue.CalcFieldSize (Some v)
                | Google.Protobuf.UninterpretedOption.ValueCase.DoubleValue v -> DoubleValue.CalcFieldSize (Some v)
                | Google.Protobuf.UninterpretedOption.ValueCase.StringValue v -> StringValue.CalcFieldSize (Some v)
                | Google.Protobuf.UninterpretedOption.ValueCase.AggregateValue v -> AggregateValue.CalcFieldSize (Some v)
        Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: UninterpretedOption) ->
            Names.WriteField w m.Names
            (match m.Value with
            | Google.Protobuf.UninterpretedOption.ValueCase.None -> ()
            | Google.Protobuf.UninterpretedOption.ValueCase.IdentifierValue v -> IdentifierValue.WriteField w (Some v)
            | Google.Protobuf.UninterpretedOption.ValueCase.PositiveIntValue v -> PositiveIntValue.WriteField w (Some v)
            | Google.Protobuf.UninterpretedOption.ValueCase.NegativeIntValue v -> NegativeIntValue.WriteField w (Some v)
            | Google.Protobuf.UninterpretedOption.ValueCase.DoubleValue v -> DoubleValue.WriteField w (Some v)
            | Google.Protobuf.UninterpretedOption.ValueCase.StringValue v -> StringValue.WriteField w (Some v)
            | Google.Protobuf.UninterpretedOption.ValueCase.AggregateValue v -> AggregateValue.WriteField w (Some v)
            )
        Decode = fun (r: Google.Protobuf.CodedInputStream) ->
            let mutable builder = new Google.Protobuf.UninterpretedOption.Builder()
            let mutable tag = 0
            while read r &tag do
                builder.Put (tag, r)
            builder.Build
        }
type UninterpretedOption = {
    // Field Declarations
    Names: Google.Protobuf.UninterpretedOption.NamePart seq // (2)
    Value: Google.Protobuf.UninterpretedOption.ValueCase
    }
    with
    static member empty = _UninterpretedOptionProto.Empty
    static member Proto = lazy _UninterpretedOptionProto

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module SourceCodeInfo =

    [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
    module Location =

        [<System.Runtime.CompilerServices.IsByRefLike>]
        type Builder =
            struct
                val mutable Paths: RepeatedBuilder<int> // (1)
                val mutable Spans: RepeatedBuilder<int> // (2)
                val mutable LeadingComments: string // (3)
                val mutable TrailingComments: string // (4)
                val mutable LeadingDetachedComments: RepeatedBuilder<string> // (6)
            end
            with
            member x.Put ((tag, reader): int * Reader) =
                match tag with
                | 1 -> x.Paths.AddRange ((ValueCodec.Packed ValueCodec.Int32).ReadValue reader)
                | 2 -> x.Spans.AddRange ((ValueCodec.Packed ValueCodec.Int32).ReadValue reader)
                | 3 -> x.LeadingComments <- ValueCodec.String.ReadValue reader
                | 4 -> x.TrailingComments <- ValueCodec.String.ReadValue reader
                | 6 -> x.LeadingDetachedComments.Add (ValueCodec.String.ReadValue reader)
                | _ -> reader.SkipLastField()
            member x.Build : Google.Protobuf.SourceCodeInfo.Location = {
                Paths = x.Paths.Build
                Spans = x.Spans.Build
                LeadingComments = x.LeadingComments |> orEmptyString
                TrailingComments = x.TrailingComments |> orEmptyString
                LeadingDetachedComments = x.LeadingDetachedComments.Build
                }

    let private _LocationProto : ProtoDef<Location> =
        // Field Definitions
        let Paths = FieldCodec.Primitive (ValueCodec.Packed ValueCodec.Int32) 1
        let Spans = FieldCodec.Primitive (ValueCodec.Packed ValueCodec.Int32) 2
        let LeadingComments = FieldCodec.Primitive ValueCodec.String 3
        let TrailingComments = FieldCodec.Primitive ValueCodec.String 4
        let LeadingDetachedComments = FieldCodec.Repeated ValueCodec.String 6
        // Proto Definition Implementation
        { // ProtoDef<Location>
            Name = "Location"
            Empty = {
                Paths = Paths.GetDefault()
                Spans = Spans.GetDefault()
                LeadingComments = LeadingComments.GetDefault()
                TrailingComments = TrailingComments.GetDefault()
                LeadingDetachedComments = LeadingDetachedComments.GetDefault()
                }
            Size = fun (m: Location) ->
                0
                + Paths.CalcFieldSize m.Paths
                + Spans.CalcFieldSize m.Spans
                + LeadingComments.CalcFieldSize m.LeadingComments
                + TrailingComments.CalcFieldSize m.TrailingComments
                + LeadingDetachedComments.CalcFieldSize m.LeadingDetachedComments
            Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: Location) ->
                Paths.WriteField w m.Paths
                Spans.WriteField w m.Spans
                LeadingComments.WriteField w m.LeadingComments
                TrailingComments.WriteField w m.TrailingComments
                LeadingDetachedComments.WriteField w m.LeadingDetachedComments
            Decode = fun (r: Google.Protobuf.CodedInputStream) ->
                let mutable builder = new Google.Protobuf.SourceCodeInfo.Location.Builder()
                let mutable tag = 0
                while read r &tag do
                    builder.Put (tag, r)
                builder.Build
            }
    type Location = {
        // Field Declarations
        Paths: int seq // (1)
        Spans: int seq // (2)
        LeadingComments: string // (3)
        TrailingComments: string // (4)
        LeadingDetachedComments: string seq // (6)
        }
        with
        static member empty = _LocationProto.Empty
        static member Proto = lazy _LocationProto

    [<System.Runtime.CompilerServices.IsByRefLike>]
    type Builder =
        struct
            val mutable Location: RepeatedBuilder<Google.Protobuf.SourceCodeInfo.Location> // (1)
        end
        with
        member x.Put ((tag, reader): int * Reader) =
            match tag with
            | 1 -> x.Location.Add (ValueCodec.Message<Google.Protobuf.SourceCodeInfo.Location>.ReadValue reader)
            | _ -> reader.SkipLastField()
        member x.Build : Google.Protobuf.SourceCodeInfo = {
            Location = x.Location.Build
            }

let private _SourceCodeInfoProto : ProtoDef<SourceCodeInfo> =
    // Field Definitions
    let Location = FieldCodec.Repeated ValueCodec.Message<Google.Protobuf.SourceCodeInfo.Location> 1
    // Proto Definition Implementation
    { // ProtoDef<SourceCodeInfo>
        Name = "SourceCodeInfo"
        Empty = {
            Location = Location.GetDefault()
            }
        Size = fun (m: SourceCodeInfo) ->
            0
            + Location.CalcFieldSize m.Location
        Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: SourceCodeInfo) ->
            Location.WriteField w m.Location
        Decode = fun (r: Google.Protobuf.CodedInputStream) ->
            let mutable builder = new Google.Protobuf.SourceCodeInfo.Builder()
            let mutable tag = 0
            while read r &tag do
                builder.Put (tag, r)
            builder.Build
        }
type SourceCodeInfo = {
    // Field Declarations
    Location: Google.Protobuf.SourceCodeInfo.Location seq // (1)
    }
    with
    static member empty = _SourceCodeInfoProto.Empty
    static member Proto = lazy _SourceCodeInfoProto

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module GeneratedCodeInfo =

    [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
    module Annotation =

        [<System.Runtime.CompilerServices.IsByRefLike>]
        type Builder =
            struct
                val mutable Paths: RepeatedBuilder<int> // (1)
                val mutable SourceFile: string // (2)
                val mutable Begin: int // (3)
                val mutable End: int // (4)
            end
            with
            member x.Put ((tag, reader): int * Reader) =
                match tag with
                | 1 -> x.Paths.AddRange ((ValueCodec.Packed ValueCodec.Int32).ReadValue reader)
                | 2 -> x.SourceFile <- ValueCodec.String.ReadValue reader
                | 3 -> x.Begin <- ValueCodec.Int32.ReadValue reader
                | 4 -> x.End <- ValueCodec.Int32.ReadValue reader
                | _ -> reader.SkipLastField()
            member x.Build : Google.Protobuf.GeneratedCodeInfo.Annotation = {
                Paths = x.Paths.Build
                SourceFile = x.SourceFile |> orEmptyString
                Begin = x.Begin
                End = x.End
                }

    let private _AnnotationProto : ProtoDef<Annotation> =
        // Field Definitions
        let Paths = FieldCodec.Primitive (ValueCodec.Packed ValueCodec.Int32) 1
        let SourceFile = FieldCodec.Primitive ValueCodec.String 2
        let Begin = FieldCodec.Primitive ValueCodec.Int32 3
        let End = FieldCodec.Primitive ValueCodec.Int32 4
        // Proto Definition Implementation
        { // ProtoDef<Annotation>
            Name = "Annotation"
            Empty = {
                Paths = Paths.GetDefault()
                SourceFile = SourceFile.GetDefault()
                Begin = Begin.GetDefault()
                End = End.GetDefault()
                }
            Size = fun (m: Annotation) ->
                0
                + Paths.CalcFieldSize m.Paths
                + SourceFile.CalcFieldSize m.SourceFile
                + Begin.CalcFieldSize m.Begin
                + End.CalcFieldSize m.End
            Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: Annotation) ->
                Paths.WriteField w m.Paths
                SourceFile.WriteField w m.SourceFile
                Begin.WriteField w m.Begin
                End.WriteField w m.End
            Decode = fun (r: Google.Protobuf.CodedInputStream) ->
                let mutable builder = new Google.Protobuf.GeneratedCodeInfo.Annotation.Builder()
                let mutable tag = 0
                while read r &tag do
                    builder.Put (tag, r)
                builder.Build
            }
    type Annotation = {
        // Field Declarations
        Paths: int seq // (1)
        SourceFile: string // (2)
        Begin: int // (3)
        End: int // (4)
        }
        with
        static member empty = _AnnotationProto.Empty
        static member Proto = lazy _AnnotationProto

    [<System.Runtime.CompilerServices.IsByRefLike>]
    type Builder =
        struct
            val mutable Annotations: RepeatedBuilder<Google.Protobuf.GeneratedCodeInfo.Annotation> // (1)
        end
        with
        member x.Put ((tag, reader): int * Reader) =
            match tag with
            | 1 -> x.Annotations.Add (ValueCodec.Message<Google.Protobuf.GeneratedCodeInfo.Annotation>.ReadValue reader)
            | _ -> reader.SkipLastField()
        member x.Build : Google.Protobuf.GeneratedCodeInfo = {
            Annotations = x.Annotations.Build
            }

let private _GeneratedCodeInfoProto : ProtoDef<GeneratedCodeInfo> =
    // Field Definitions
    let Annotations = FieldCodec.Repeated ValueCodec.Message<Google.Protobuf.GeneratedCodeInfo.Annotation> 1
    // Proto Definition Implementation
    { // ProtoDef<GeneratedCodeInfo>
        Name = "GeneratedCodeInfo"
        Empty = {
            Annotations = Annotations.GetDefault()
            }
        Size = fun (m: GeneratedCodeInfo) ->
            0
            + Annotations.CalcFieldSize m.Annotations
        Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: GeneratedCodeInfo) ->
            Annotations.WriteField w m.Annotations
        Decode = fun (r: Google.Protobuf.CodedInputStream) ->
            let mutable builder = new Google.Protobuf.GeneratedCodeInfo.Builder()
            let mutable tag = 0
            while read r &tag do
                builder.Put (tag, r)
            builder.Build
        }
type GeneratedCodeInfo = {
    // Field Declarations
    Annotations: Google.Protobuf.GeneratedCodeInfo.Annotation seq // (1)
    }
    with
    static member empty = _GeneratedCodeInfoProto.Empty
    static member Proto = lazy _GeneratedCodeInfoProto
