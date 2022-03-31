[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module rec Google.Protobuf.Compiler
open FsGrpc.Protobuf
#nowarn "40"


[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Version =

    [<System.Runtime.CompilerServices.IsByRefLike>]
    type Builder =
        struct
            val mutable Major: int // (1)
            val mutable Minor: int // (2)
            val mutable Patch: int // (3)
            val mutable Suffix: string // (4)
        end
        with
        member x.Put ((tag, reader): int * Reader) =
            match tag with
            | 1 -> x.Major <- ValueCodec.Int32.ReadValue reader
            | 2 -> x.Minor <- ValueCodec.Int32.ReadValue reader
            | 3 -> x.Patch <- ValueCodec.Int32.ReadValue reader
            | 4 -> x.Suffix <- ValueCodec.String.ReadValue reader
            | _ -> reader.SkipLastField()
        member x.Build : Google.Protobuf.Compiler.Version = {
            Major = x.Major
            Minor = x.Minor
            Patch = x.Patch
            Suffix = x.Suffix |> orEmptyString
            }

let private _VersionProto : ProtoDef<Version> =
    // Field Definitions
    let Major = FieldCodec.Primitive ValueCodec.Int32 1
    let Minor = FieldCodec.Primitive ValueCodec.Int32 2
    let Patch = FieldCodec.Primitive ValueCodec.Int32 3
    let Suffix = FieldCodec.Primitive ValueCodec.String 4
    // Proto Definition Implementation
    { // ProtoDef<Version>
        Name = "Version"
        Empty = {
            Major = Major.GetDefault()
            Minor = Minor.GetDefault()
            Patch = Patch.GetDefault()
            Suffix = Suffix.GetDefault()
            }
        Size = fun (m: Version) ->
            0
            + Major.CalcFieldSize m.Major
            + Minor.CalcFieldSize m.Minor
            + Patch.CalcFieldSize m.Patch
            + Suffix.CalcFieldSize m.Suffix
        Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: Version) ->
            Major.WriteField w m.Major
            Minor.WriteField w m.Minor
            Patch.WriteField w m.Patch
            Suffix.WriteField w m.Suffix
        Decode = fun (r: Google.Protobuf.CodedInputStream) ->
            let mutable builder = new Google.Protobuf.Compiler.Version.Builder()
            let mutable tag = 0
            while read r &tag do
                builder.Put (tag, r)
            builder.Build
        }
type Version = {
    // Field Declarations
    Major: int // (1)
    Minor: int // (2)
    Patch: int // (3)
    Suffix: string // (4)
    }
    with
    static member empty = _VersionProto.Empty
    static member Proto = lazy _VersionProto

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module CodeGeneratorRequest =

    [<System.Runtime.CompilerServices.IsByRefLike>]
    type Builder =
        struct
            val mutable FilesToGenerate: RepeatedBuilder<string> // (1)
            val mutable Parameter: string // (2)
            val mutable ProtoFiles: RepeatedBuilder<Google.Protobuf.FileDescriptorProto> // (15)
            val mutable CompilerVersion: OptionBuilder<Google.Protobuf.Compiler.Version> // (3)
        end
        with
        member x.Put ((tag, reader): int * Reader) =
            match tag with
            | 1 -> x.FilesToGenerate.Add (ValueCodec.String.ReadValue reader)
            | 2 -> x.Parameter <- ValueCodec.String.ReadValue reader
            | 15 -> x.ProtoFiles.Add (ValueCodec.Message<Google.Protobuf.FileDescriptorProto>.ReadValue reader)
            | 3 -> x.CompilerVersion.Set (ValueCodec.Message<Google.Protobuf.Compiler.Version>.ReadValue reader)
            | _ -> reader.SkipLastField()
        member x.Build : Google.Protobuf.Compiler.CodeGeneratorRequest = {
            FilesToGenerate = x.FilesToGenerate.Build
            Parameter = x.Parameter |> orEmptyString
            ProtoFiles = x.ProtoFiles.Build
            CompilerVersion = x.CompilerVersion.Build
            }

let private _CodeGeneratorRequestProto : ProtoDef<CodeGeneratorRequest> =
    // Field Definitions
    let FilesToGenerate = FieldCodec.Repeated ValueCodec.String 1
    let Parameter = FieldCodec.Primitive ValueCodec.String 2
    let ProtoFiles = FieldCodec.Repeated ValueCodec.Message<Google.Protobuf.FileDescriptorProto> 15
    let CompilerVersion = FieldCodec.Optional ValueCodec.Message<Google.Protobuf.Compiler.Version> 3
    // Proto Definition Implementation
    { // ProtoDef<CodeGeneratorRequest>
        Name = "CodeGeneratorRequest"
        Empty = {
            FilesToGenerate = FilesToGenerate.GetDefault()
            Parameter = Parameter.GetDefault()
            ProtoFiles = ProtoFiles.GetDefault()
            CompilerVersion = CompilerVersion.GetDefault()
            }
        Size = fun (m: CodeGeneratorRequest) ->
            0
            + FilesToGenerate.CalcFieldSize m.FilesToGenerate
            + Parameter.CalcFieldSize m.Parameter
            + ProtoFiles.CalcFieldSize m.ProtoFiles
            + CompilerVersion.CalcFieldSize m.CompilerVersion
        Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: CodeGeneratorRequest) ->
            FilesToGenerate.WriteField w m.FilesToGenerate
            Parameter.WriteField w m.Parameter
            ProtoFiles.WriteField w m.ProtoFiles
            CompilerVersion.WriteField w m.CompilerVersion
        Decode = fun (r: Google.Protobuf.CodedInputStream) ->
            let mutable builder = new Google.Protobuf.Compiler.CodeGeneratorRequest.Builder()
            let mutable tag = 0
            while read r &tag do
                builder.Put (tag, r)
            builder.Build
        }
type CodeGeneratorRequest = {
    // Field Declarations
    FilesToGenerate: string seq // (1)
    Parameter: string // (2)
    ProtoFiles: Google.Protobuf.FileDescriptorProto seq // (15)
    CompilerVersion: Google.Protobuf.Compiler.Version option // (3)
    }
    with
    static member empty = _CodeGeneratorRequestProto.Empty
    static member Proto = lazy _CodeGeneratorRequestProto

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module CodeGeneratorResponse =

    [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
    module File =

        [<System.Runtime.CompilerServices.IsByRefLike>]
        type Builder =
            struct
                val mutable Name: string // (1)
                val mutable InsertionPoint: string // (2)
                val mutable Content: string // (15)
            end
            with
            member x.Put ((tag, reader): int * Reader) =
                match tag with
                | 1 -> x.Name <- ValueCodec.String.ReadValue reader
                | 2 -> x.InsertionPoint <- ValueCodec.String.ReadValue reader
                | 15 -> x.Content <- ValueCodec.String.ReadValue reader
                | _ -> reader.SkipLastField()
            member x.Build : Google.Protobuf.Compiler.CodeGeneratorResponse.File = {
                Name = x.Name |> orEmptyString
                InsertionPoint = x.InsertionPoint |> orEmptyString
                Content = x.Content |> orEmptyString
                }

    let private _FileProto : ProtoDef<File> =
        // Field Definitions
        let Name = FieldCodec.Primitive ValueCodec.String 1
        let InsertionPoint = FieldCodec.Primitive ValueCodec.String 2
        let Content = FieldCodec.Primitive ValueCodec.String 15
        // Proto Definition Implementation
        { // ProtoDef<File>
            Name = "File"
            Empty = {
                Name = Name.GetDefault()
                InsertionPoint = InsertionPoint.GetDefault()
                Content = Content.GetDefault()
                }
            Size = fun (m: File) ->
                0
                + Name.CalcFieldSize m.Name
                + InsertionPoint.CalcFieldSize m.InsertionPoint
                + Content.CalcFieldSize m.Content
            Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: File) ->
                Name.WriteField w m.Name
                InsertionPoint.WriteField w m.InsertionPoint
                Content.WriteField w m.Content
            Decode = fun (r: Google.Protobuf.CodedInputStream) ->
                let mutable builder = new Google.Protobuf.Compiler.CodeGeneratorResponse.File.Builder()
                let mutable tag = 0
                while read r &tag do
                    builder.Put (tag, r)
                builder.Build
            }
    type File = {
        // Field Declarations
        Name: string // (1)
        InsertionPoint: string // (2)
        Content: string // (15)
        }
        with
        static member empty = _FileProto.Empty
        static member Proto = lazy _FileProto

    type Feature =
    | None = 0
    | Proto3Optional = 1

    [<System.Runtime.CompilerServices.IsByRefLike>]
    type Builder =
        struct
            val mutable Error: string // (1)
            val mutable SupportedFeatures: uint64 // (2)
            val mutable Files: RepeatedBuilder<Google.Protobuf.Compiler.CodeGeneratorResponse.File> // (15)
        end
        with
        member x.Put ((tag, reader): int * Reader) =
            match tag with
            | 1 -> x.Error <- ValueCodec.String.ReadValue reader
            | 2 -> x.SupportedFeatures <- ValueCodec.UInt64.ReadValue reader
            | 15 -> x.Files.Add (ValueCodec.Message<Google.Protobuf.Compiler.CodeGeneratorResponse.File>.ReadValue reader)
            | _ -> reader.SkipLastField()
        member x.Build : Google.Protobuf.Compiler.CodeGeneratorResponse = {
            Error = x.Error |> orEmptyString
            SupportedFeatures = x.SupportedFeatures
            Files = x.Files.Build
            }

let private _CodeGeneratorResponseProto : ProtoDef<CodeGeneratorResponse> =
    // Field Definitions
    let Error = FieldCodec.Primitive ValueCodec.String 1
    let SupportedFeatures = FieldCodec.Primitive ValueCodec.UInt64 2
    let Files = FieldCodec.Repeated ValueCodec.Message<Google.Protobuf.Compiler.CodeGeneratorResponse.File> 15
    // Proto Definition Implementation
    { // ProtoDef<CodeGeneratorResponse>
        Name = "CodeGeneratorResponse"
        Empty = {
            Error = Error.GetDefault()
            SupportedFeatures = SupportedFeatures.GetDefault()
            Files = Files.GetDefault()
            }
        Size = fun (m: CodeGeneratorResponse) ->
            0
            + Error.CalcFieldSize m.Error
            + SupportedFeatures.CalcFieldSize m.SupportedFeatures
            + Files.CalcFieldSize m.Files
        Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: CodeGeneratorResponse) ->
            Error.WriteField w m.Error
            SupportedFeatures.WriteField w m.SupportedFeatures
            Files.WriteField w m.Files
        Decode = fun (r: Google.Protobuf.CodedInputStream) ->
            let mutable builder = new Google.Protobuf.Compiler.CodeGeneratorResponse.Builder()
            let mutable tag = 0
            while read r &tag do
                builder.Put (tag, r)
            builder.Build
        }
type CodeGeneratorResponse = {
    // Field Declarations
    Error: string // (1)
    SupportedFeatures: uint64 // (2)
    Files: Google.Protobuf.Compiler.CodeGeneratorResponse.File seq // (15)
    }
    with
    static member empty = _CodeGeneratorResponseProto.Empty
    static member Proto = lazy _CodeGeneratorResponseProto
