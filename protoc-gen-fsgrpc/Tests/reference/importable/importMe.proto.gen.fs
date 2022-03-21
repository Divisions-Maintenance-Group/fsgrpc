[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module rec Ex.Ample.Importable
open FsGrpc
#nowarn "40"


[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Imported =

    type EnumForImport =
    | No = 0
    | Yes = 1

    [<System.Runtime.CompilerServices.IsByRefLike>]
    type Builder =
        struct
            val mutable Value: string // (1)
        end
        with
        member x.Put ((tag, reader): int * Reader) =
            match tag with
            | 1 -> x.Value <- ValueCodec.String.ReadValue reader
            | _ -> reader.SkipLastField()
        member x.Build : Ex.Ample.Importable.Imported = {
            Value = x.Value |> orEmptyString
            }

let private _ImportedProto : ProtoDef<Imported> =
    // Field Definitions
    let Value = FieldCodec.Primitive ValueCodec.String 1
    // Proto Definition Implementation
    { // ProtoDef<Imported>
        Name = "Imported"
        Empty = {
            Value = Value.GetDefault()
            }
        Size = fun (m: Imported) ->
            0
            + Value.CalcFieldSize m.Value
        Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: Imported) ->
            Value.WriteField w m.Value
        Decode = fun (r: Google.Protobuf.CodedInputStream) ->
            let mutable builder = new Ex.Ample.Importable.Imported.Builder()
            let mutable tag = 0
            while read r &tag do
                builder.Put (tag, r)
            builder.Build
        }
type Imported = {
    // Field Declarations
    Value: string // (1)
    }
    with
    static member empty = _ImportedProto.Empty
    static member Proto = lazy _ImportedProto

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Args =

    [<System.Runtime.CompilerServices.IsByRefLike>]
    type Builder =
        struct
            val mutable Value: string // (1)
        end
        with
        member x.Put ((tag, reader): int * Reader) =
            match tag with
            | 1 -> x.Value <- ValueCodec.String.ReadValue reader
            | _ -> reader.SkipLastField()
        member x.Build : Ex.Ample.Importable.Args = {
            Value = x.Value |> orEmptyString
            }

let private _ArgsProto : ProtoDef<Args> =
    // Field Definitions
    let Value = FieldCodec.Primitive ValueCodec.String 1
    // Proto Definition Implementation
    { // ProtoDef<Args>
        Name = "Args"
        Empty = {
            Value = Value.GetDefault()
            }
        Size = fun (m: Args) ->
            0
            + Value.CalcFieldSize m.Value
        Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: Args) ->
            Value.WriteField w m.Value
        Decode = fun (r: Google.Protobuf.CodedInputStream) ->
            let mutable builder = new Ex.Ample.Importable.Args.Builder()
            let mutable tag = 0
            while read r &tag do
                builder.Put (tag, r)
            builder.Build
        }
type Args = {
    // Field Declarations
    Value: string // (1)
    }
    with
    static member empty = _ArgsProto.Empty
    static member Proto = lazy _ArgsProto
