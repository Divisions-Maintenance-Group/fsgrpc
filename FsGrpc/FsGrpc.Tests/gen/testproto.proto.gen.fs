[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module rec Test.Name.Space
open FsGrpc.Protobuf
#nowarn "40"


[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module TestMessage =

    [<System.Runtime.CompilerServices.IsByRefLike>]
    type Builder =
        struct
            val mutable TestInt: int // (1)
            val mutable TestDouble: double // (2)
            val mutable TestFixed32: uint // (3)
            val mutable TestString: string // (4)
            val mutable TestBytes: FsGrpc.Bytes // (5)
            val mutable TestFloat: float32 // (6)
            val mutable TestInt64: int64 // (7)
            val mutable TestUint64: uint64 // (8)
            val mutable TestFixed64: uint64 // (9)
            val mutable TestBool: bool // (10)
            val mutable TestUint32: uint32 // (11)
            val mutable TestSfixed32: int // (12)
            val mutable TestSfixed64: int64 // (13)
            val mutable TestSint32: int // (14)
            val mutable TestSint64: int64 // (15)
        end
        with
        member x.Put ((tag, reader): int * Reader) =
            match tag with
            | 1 -> x.TestInt <- ValueCodec.Int32.ReadValue reader
            | 2 -> x.TestDouble <- ValueCodec.Double.ReadValue reader
            | 3 -> x.TestFixed32 <- ValueCodec.Fixed32.ReadValue reader
            | 4 -> x.TestString <- ValueCodec.String.ReadValue reader
            | 5 -> x.TestBytes <- ValueCodec.Bytes.ReadValue reader
            | 6 -> x.TestFloat <- ValueCodec.Float.ReadValue reader
            | 7 -> x.TestInt64 <- ValueCodec.Int64.ReadValue reader
            | 8 -> x.TestUint64 <- ValueCodec.UInt64.ReadValue reader
            | 9 -> x.TestFixed64 <- ValueCodec.Fixed64.ReadValue reader
            | 10 -> x.TestBool <- ValueCodec.Bool.ReadValue reader
            | 11 -> x.TestUint32 <- ValueCodec.UInt32.ReadValue reader
            | 12 -> x.TestSfixed32 <- ValueCodec.SFixed32.ReadValue reader
            | 13 -> x.TestSfixed64 <- ValueCodec.SFixed64.ReadValue reader
            | 14 -> x.TestSint32 <- ValueCodec.SInt32.ReadValue reader
            | 15 -> x.TestSint64 <- ValueCodec.SInt64.ReadValue reader
            | _ -> reader.SkipLastField()
        member x.Build : Test.Name.Space.TestMessage = {
            TestInt = x.TestInt
            TestDouble = x.TestDouble
            TestFixed32 = x.TestFixed32
            TestString = x.TestString |> orEmptyString
            TestBytes = x.TestBytes
            TestFloat = x.TestFloat
            TestInt64 = x.TestInt64
            TestUint64 = x.TestUint64
            TestFixed64 = x.TestFixed64
            TestBool = x.TestBool
            TestUint32 = x.TestUint32
            TestSfixed32 = x.TestSfixed32
            TestSfixed64 = x.TestSfixed64
            TestSint32 = x.TestSint32
            TestSint64 = x.TestSint64
            }

let private _TestMessageProto : ProtoDef<TestMessage> =
    // Field Definitions
    let TestInt = FieldCodec.Primitive ValueCodec.Int32 (1, "testInt")
    let TestDouble = FieldCodec.Primitive ValueCodec.Double (2, "testDouble")
    let TestFixed32 = FieldCodec.Primitive ValueCodec.Fixed32 (3, "testFixed32")
    let TestString = FieldCodec.Primitive ValueCodec.String (4, "testString")
    let TestBytes = FieldCodec.Primitive ValueCodec.Bytes (5, "testBytes")
    let TestFloat = FieldCodec.Primitive ValueCodec.Float (6, "testFloat")
    let TestInt64 = FieldCodec.Primitive ValueCodec.Int64 (7, "testInt64")
    let TestUint64 = FieldCodec.Primitive ValueCodec.UInt64 (8, "testUint64")
    let TestFixed64 = FieldCodec.Primitive ValueCodec.Fixed64 (9, "testFixed64")
    let TestBool = FieldCodec.Primitive ValueCodec.Bool (10, "testBool")
    let TestUint32 = FieldCodec.Primitive ValueCodec.UInt32 (11, "testUint32")
    let TestSfixed32 = FieldCodec.Primitive ValueCodec.SFixed32 (12, "testSfixed32")
    let TestSfixed64 = FieldCodec.Primitive ValueCodec.SFixed64 (13, "testSfixed64")
    let TestSint32 = FieldCodec.Primitive ValueCodec.SInt32 (14, "testSint32")
    let TestSint64 = FieldCodec.Primitive ValueCodec.SInt64 (15, "testSint64")
    // Proto Definition Implementation
    { // ProtoDef<TestMessage>
        Name = "TestMessage"
        Empty = {
            TestInt = TestInt.GetDefault()
            TestDouble = TestDouble.GetDefault()
            TestFixed32 = TestFixed32.GetDefault()
            TestString = TestString.GetDefault()
            TestBytes = TestBytes.GetDefault()
            TestFloat = TestFloat.GetDefault()
            TestInt64 = TestInt64.GetDefault()
            TestUint64 = TestUint64.GetDefault()
            TestFixed64 = TestFixed64.GetDefault()
            TestBool = TestBool.GetDefault()
            TestUint32 = TestUint32.GetDefault()
            TestSfixed32 = TestSfixed32.GetDefault()
            TestSfixed64 = TestSfixed64.GetDefault()
            TestSint32 = TestSint32.GetDefault()
            TestSint64 = TestSint64.GetDefault()
            }
        Size = fun (m: TestMessage) ->
            0
            + TestInt.CalcFieldSize m.TestInt
            + TestDouble.CalcFieldSize m.TestDouble
            + TestFixed32.CalcFieldSize m.TestFixed32
            + TestString.CalcFieldSize m.TestString
            + TestBytes.CalcFieldSize m.TestBytes
            + TestFloat.CalcFieldSize m.TestFloat
            + TestInt64.CalcFieldSize m.TestInt64
            + TestUint64.CalcFieldSize m.TestUint64
            + TestFixed64.CalcFieldSize m.TestFixed64
            + TestBool.CalcFieldSize m.TestBool
            + TestUint32.CalcFieldSize m.TestUint32
            + TestSfixed32.CalcFieldSize m.TestSfixed32
            + TestSfixed64.CalcFieldSize m.TestSfixed64
            + TestSint32.CalcFieldSize m.TestSint32
            + TestSint64.CalcFieldSize m.TestSint64
        Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: TestMessage) ->
            TestInt.WriteField w m.TestInt
            TestDouble.WriteField w m.TestDouble
            TestFixed32.WriteField w m.TestFixed32
            TestString.WriteField w m.TestString
            TestBytes.WriteField w m.TestBytes
            TestFloat.WriteField w m.TestFloat
            TestInt64.WriteField w m.TestInt64
            TestUint64.WriteField w m.TestUint64
            TestFixed64.WriteField w m.TestFixed64
            TestBool.WriteField w m.TestBool
            TestUint32.WriteField w m.TestUint32
            TestSfixed32.WriteField w m.TestSfixed32
            TestSfixed64.WriteField w m.TestSfixed64
            TestSint32.WriteField w m.TestSint32
            TestSint64.WriteField w m.TestSint64
        Decode = fun (r: Google.Protobuf.CodedInputStream) ->
            let mutable builder = new Test.Name.Space.TestMessage.Builder()
            let mutable tag = 0
            while read r &tag do
                builder.Put (tag, r)
            builder.Build
        EncodeJson = fun (o: JsonOptions) ->
            let writeTestInt = TestInt.WriteJsonField o
            let writeTestDouble = TestDouble.WriteJsonField o
            let writeTestFixed32 = TestFixed32.WriteJsonField o
            let writeTestString = TestString.WriteJsonField o
            let writeTestBytes = TestBytes.WriteJsonField o
            let writeTestFloat = TestFloat.WriteJsonField o
            let writeTestInt64 = TestInt64.WriteJsonField o
            let writeTestUint64 = TestUint64.WriteJsonField o
            let writeTestFixed64 = TestFixed64.WriteJsonField o
            let writeTestBool = TestBool.WriteJsonField o
            let writeTestUint32 = TestUint32.WriteJsonField o
            let writeTestSfixed32 = TestSfixed32.WriteJsonField o
            let writeTestSfixed64 = TestSfixed64.WriteJsonField o
            let writeTestSint32 = TestSint32.WriteJsonField o
            let writeTestSint64 = TestSint64.WriteJsonField o
            let encode (w: System.Text.Json.Utf8JsonWriter) (m: TestMessage) =
                writeTestInt w m.TestInt
                writeTestDouble w m.TestDouble
                writeTestFixed32 w m.TestFixed32
                writeTestString w m.TestString
                writeTestBytes w m.TestBytes
                writeTestFloat w m.TestFloat
                writeTestInt64 w m.TestInt64
                writeTestUint64 w m.TestUint64
                writeTestFixed64 w m.TestFixed64
                writeTestBool w m.TestBool
                writeTestUint32 w m.TestUint32
                writeTestSfixed32 w m.TestSfixed32
                writeTestSfixed64 w m.TestSfixed64
                writeTestSint32 w m.TestSint32
                writeTestSint64 w m.TestSint64
            encode
    }
[<System.Text.Json.Serialization.JsonConverter(typeof<FsGrpc.Json.MessageConverter>)>]
[<FsGrpc.Protobuf.Message>]
type TestMessage = {
    // Field Declarations
    [<System.Text.Json.Serialization.JsonPropertyName("testInt")>] TestInt: int // (1)
    [<System.Text.Json.Serialization.JsonPropertyName("testDouble")>] TestDouble: double // (2)
    [<System.Text.Json.Serialization.JsonPropertyName("testFixed32")>] TestFixed32: uint // (3)
    [<System.Text.Json.Serialization.JsonPropertyName("testString")>] TestString: string // (4)
    [<System.Text.Json.Serialization.JsonPropertyName("testBytes")>] TestBytes: FsGrpc.Bytes // (5)
    [<System.Text.Json.Serialization.JsonPropertyName("testFloat")>] TestFloat: float32 // (6)
    [<System.Text.Json.Serialization.JsonPropertyName("testInt64")>] TestInt64: int64 // (7)
    [<System.Text.Json.Serialization.JsonPropertyName("testUint64")>] TestUint64: uint64 // (8)
    [<System.Text.Json.Serialization.JsonPropertyName("testFixed64")>] TestFixed64: uint64 // (9)
    [<System.Text.Json.Serialization.JsonPropertyName("testBool")>] TestBool: bool // (10)
    [<System.Text.Json.Serialization.JsonPropertyName("testUint32")>] TestUint32: uint32 // (11)
    [<System.Text.Json.Serialization.JsonPropertyName("testSfixed32")>] TestSfixed32: int // (12)
    [<System.Text.Json.Serialization.JsonPropertyName("testSfixed64")>] TestSfixed64: int64 // (13)
    [<System.Text.Json.Serialization.JsonPropertyName("testSint32")>] TestSint32: int // (14)
    [<System.Text.Json.Serialization.JsonPropertyName("testSint64")>] TestSint64: int64 // (15)
    }
    with
    static member empty = _TestMessageProto.Empty
    static member Proto = lazy _TestMessageProto

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Nest =

    [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
    module Inner =

        [<System.Runtime.CompilerServices.IsByRefLike>]
        type Builder =
            struct
                val mutable InnerName: string // (1)
            end
            with
            member x.Put ((tag, reader): int * Reader) =
                match tag with
                | 1 -> x.InnerName <- ValueCodec.String.ReadValue reader
                | _ -> reader.SkipLastField()
            member x.Build : Test.Name.Space.Nest.Inner = {
                InnerName = x.InnerName |> orEmptyString
                }

    let private _InnerProto : ProtoDef<Inner> =
        // Field Definitions
        let InnerName = FieldCodec.Primitive ValueCodec.String (1, "innerName")
        // Proto Definition Implementation
        { // ProtoDef<Inner>
            Name = "Inner"
            Empty = {
                InnerName = InnerName.GetDefault()
                }
            Size = fun (m: Inner) ->
                0
                + InnerName.CalcFieldSize m.InnerName
            Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: Inner) ->
                InnerName.WriteField w m.InnerName
            Decode = fun (r: Google.Protobuf.CodedInputStream) ->
                let mutable builder = new Test.Name.Space.Nest.Inner.Builder()
                let mutable tag = 0
                while read r &tag do
                    builder.Put (tag, r)
                builder.Build
            EncodeJson = fun (o: JsonOptions) ->
                let writeInnerName = InnerName.WriteJsonField o
                let encode (w: System.Text.Json.Utf8JsonWriter) (m: Inner) =
                    writeInnerName w m.InnerName
                encode
        }
    [<System.Text.Json.Serialization.JsonConverter(typeof<FsGrpc.Json.MessageConverter>)>]
    [<FsGrpc.Protobuf.Message>]
    type Inner = {
        // Field Declarations
        [<System.Text.Json.Serialization.JsonPropertyName("innerName")>] InnerName: string // (1)
        }
        with
        static member empty = _InnerProto.Empty
        static member Proto = lazy _InnerProto

    [<System.Runtime.CompilerServices.IsByRefLike>]
    type Builder =
        struct
            val mutable Name: string // (1)
            val mutable Children: RepeatedBuilder<Test.Name.Space.Nest> // (2)
            val mutable Inner: OptionBuilder<Test.Name.Space.Nest.Inner> // (3)
            val mutable Special: OptionBuilder<Test.Name.Space.Special> // (4)
        end
        with
        member x.Put ((tag, reader): int * Reader) =
            match tag with
            | 1 -> x.Name <- ValueCodec.String.ReadValue reader
            | 2 -> x.Children.Add (ValueCodec.Message<Test.Name.Space.Nest>.ReadValue reader)
            | 3 -> x.Inner.Set (ValueCodec.Message<Test.Name.Space.Nest.Inner>.ReadValue reader)
            | 4 -> x.Special.Set (ValueCodec.Message<Test.Name.Space.Special>.ReadValue reader)
            | _ -> reader.SkipLastField()
        member x.Build : Test.Name.Space.Nest = {
            Name = x.Name |> orEmptyString
            Children = x.Children.Build
            Inner = x.Inner.Build
            Special = x.Special.Build
            }

let private _NestProto : ProtoDef<Nest> =
    // Field Definitions
    let Name = FieldCodec.Primitive ValueCodec.String (1, "name")
    let Children = FieldCodec.Repeated ValueCodec.Message<Test.Name.Space.Nest> (2, "children")
    let Inner = FieldCodec.Optional ValueCodec.Message<Test.Name.Space.Nest.Inner> (3, "inner")
    let Special = FieldCodec.Optional ValueCodec.Message<Test.Name.Space.Special> (4, "special")
    // Proto Definition Implementation
    { // ProtoDef<Nest>
        Name = "Nest"
        Empty = {
            Name = Name.GetDefault()
            Children = Children.GetDefault()
            Inner = Inner.GetDefault()
            Special = Special.GetDefault()
            }
        Size = fun (m: Nest) ->
            0
            + Name.CalcFieldSize m.Name
            + Children.CalcFieldSize m.Children
            + Inner.CalcFieldSize m.Inner
            + Special.CalcFieldSize m.Special
        Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: Nest) ->
            Name.WriteField w m.Name
            Children.WriteField w m.Children
            Inner.WriteField w m.Inner
            Special.WriteField w m.Special
        Decode = fun (r: Google.Protobuf.CodedInputStream) ->
            let mutable builder = new Test.Name.Space.Nest.Builder()
            let mutable tag = 0
            while read r &tag do
                builder.Put (tag, r)
            builder.Build
        EncodeJson = fun (o: JsonOptions) ->
            let writeName = Name.WriteJsonField o
            let writeChildren = Children.WriteJsonField o
            let writeInner = Inner.WriteJsonField o
            let writeSpecial = Special.WriteJsonField o
            let encode (w: System.Text.Json.Utf8JsonWriter) (m: Nest) =
                writeName w m.Name
                writeChildren w m.Children
                writeInner w m.Inner
                writeSpecial w m.Special
            encode
    }
[<System.Text.Json.Serialization.JsonConverter(typeof<FsGrpc.Json.MessageConverter>)>]
[<FsGrpc.Protobuf.Message>]
type Nest = {
    // Field Declarations
    [<System.Text.Json.Serialization.JsonPropertyName("name")>] Name: string // (1)
    [<System.Text.Json.Serialization.JsonPropertyName("children")>] Children: Test.Name.Space.Nest seq // (2)
    [<System.Text.Json.Serialization.JsonPropertyName("inner")>] Inner: Test.Name.Space.Nest.Inner option // (3)
    [<System.Text.Json.Serialization.JsonPropertyName("special")>] Special: Test.Name.Space.Special option // (4)
    }
    with
    static member empty = _NestProto.Empty
    static member Proto = lazy _NestProto

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Special =

    [<System.Runtime.CompilerServices.IsByRefLike>]
    type Builder =
        struct
            val mutable IntList: RepeatedBuilder<int> // (1)
            val mutable DoubleList: RepeatedBuilder<double> // (2)
            val mutable Fixed32List: RepeatedBuilder<uint> // (3)
            val mutable StringList: RepeatedBuilder<string> // (4)
            val mutable Dictionary: MapBuilder<string, string> // (16)
        end
        with
        member x.Put ((tag, reader): int * Reader) =
            match tag with
            | 1 -> x.IntList.AddRange ((ValueCodec.Packed ValueCodec.Int32).ReadValue reader)
            | 2 -> x.DoubleList.AddRange ((ValueCodec.Packed ValueCodec.Double).ReadValue reader)
            | 3 -> x.Fixed32List.AddRange ((ValueCodec.Packed ValueCodec.Fixed32).ReadValue reader)
            | 4 -> x.StringList.Add (ValueCodec.String.ReadValue reader)
            | 16 -> x.Dictionary.Add ((ValueCodec.MapRecord ValueCodec.String ValueCodec.String).ReadValue reader)
            | _ -> reader.SkipLastField()
        member x.Build : Test.Name.Space.Special = {
            IntList = x.IntList.Build
            DoubleList = x.DoubleList.Build
            Fixed32List = x.Fixed32List.Build
            StringList = x.StringList.Build
            Dictionary = x.Dictionary.Build
            }

let private _SpecialProto : ProtoDef<Special> =
    // Field Definitions
    let IntList = FieldCodec.Primitive (ValueCodec.Packed ValueCodec.Int32) (1, "intList")
    let DoubleList = FieldCodec.Primitive (ValueCodec.Packed ValueCodec.Double) (2, "doubleList")
    let Fixed32List = FieldCodec.Primitive (ValueCodec.Packed ValueCodec.Fixed32) (3, "fixed32List")
    let StringList = FieldCodec.Repeated ValueCodec.String (4, "stringList")
    let Dictionary = FieldCodec.Map ValueCodec.String ValueCodec.String (16, "dictionary")
    // Proto Definition Implementation
    { // ProtoDef<Special>
        Name = "Special"
        Empty = {
            IntList = IntList.GetDefault()
            DoubleList = DoubleList.GetDefault()
            Fixed32List = Fixed32List.GetDefault()
            StringList = StringList.GetDefault()
            Dictionary = Dictionary.GetDefault()
            }
        Size = fun (m: Special) ->
            0
            + IntList.CalcFieldSize m.IntList
            + DoubleList.CalcFieldSize m.DoubleList
            + Fixed32List.CalcFieldSize m.Fixed32List
            + StringList.CalcFieldSize m.StringList
            + Dictionary.CalcFieldSize m.Dictionary
        Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: Special) ->
            IntList.WriteField w m.IntList
            DoubleList.WriteField w m.DoubleList
            Fixed32List.WriteField w m.Fixed32List
            StringList.WriteField w m.StringList
            Dictionary.WriteField w m.Dictionary
        Decode = fun (r: Google.Protobuf.CodedInputStream) ->
            let mutable builder = new Test.Name.Space.Special.Builder()
            let mutable tag = 0
            while read r &tag do
                builder.Put (tag, r)
            builder.Build
        EncodeJson = fun (o: JsonOptions) ->
            let writeIntList = IntList.WriteJsonField o
            let writeDoubleList = DoubleList.WriteJsonField o
            let writeFixed32List = Fixed32List.WriteJsonField o
            let writeStringList = StringList.WriteJsonField o
            let writeDictionary = Dictionary.WriteJsonField o
            let encode (w: System.Text.Json.Utf8JsonWriter) (m: Special) =
                writeIntList w m.IntList
                writeDoubleList w m.DoubleList
                writeFixed32List w m.Fixed32List
                writeStringList w m.StringList
                writeDictionary w m.Dictionary
            encode
    }
[<System.Text.Json.Serialization.JsonConverter(typeof<FsGrpc.Json.MessageConverter>)>]
[<FsGrpc.Protobuf.Message>]
type Special = {
    // Field Declarations
    [<System.Text.Json.Serialization.JsonPropertyName("intList")>] IntList: int seq // (1)
    [<System.Text.Json.Serialization.JsonPropertyName("doubleList")>] DoubleList: double seq // (2)
    [<System.Text.Json.Serialization.JsonPropertyName("fixed32List")>] Fixed32List: uint seq // (3)
    [<System.Text.Json.Serialization.JsonPropertyName("stringList")>] StringList: string seq // (4)
    [<System.Text.Json.Serialization.JsonPropertyName("dictionary")>] Dictionary: Map<string, string> // (16)
    }
    with
    static member empty = _SpecialProto.Empty
    static member Proto = lazy _SpecialProto

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Enums =

    [<System.Text.Json.Serialization.JsonConverter(typeof<FsGrpc.Json.OneofConverter<UnionCase>>)>]
    [<CompilationRepresentation(CompilationRepresentationFlags.UseNullAsTrueValue)>]
    [<StructuralEquality;NoComparison>]
    [<RequireQualifiedAccess>]
    type UnionCase =
    | None
    | [<System.Text.Json.Serialization.JsonPropertyName("color")>] Color of Test.Name.Space.Enums.Color
    | [<System.Text.Json.Serialization.JsonPropertyName("name")>] Name of string

    [<System.Text.Json.Serialization.JsonConverter(typeof<FsGrpc.Json.EnumConverter<Color>>)>]
    type Color =
    | [<FsGrpc.Protobuf.ProtobufName("COLOR_BLACK")>] Black = 0
    | [<FsGrpc.Protobuf.ProtobufName("COLOR_RED")>] Red = 1
    | [<FsGrpc.Protobuf.ProtobufName("COLOR_GREEN")>] Green = 2
    | [<FsGrpc.Protobuf.ProtobufName("COLOR_BLUE")>] Blue = 3

    [<System.Runtime.CompilerServices.IsByRefLike>]
    type Builder =
        struct
            val mutable MainColor: Test.Name.Space.Enums.Color // (1)
            val mutable OtherColors: RepeatedBuilder<Test.Name.Space.Enums.Color> // (2)
            val mutable ByName: MapBuilder<string, Test.Name.Space.Enums.Color> // (3)
            val mutable Union: OptionBuilder<Test.Name.Space.Enums.UnionCase>
            val mutable MaybeColor: OptionBuilder<Test.Name.Space.Enums.Color> // (6)
        end
        with
        member x.Put ((tag, reader): int * Reader) =
            match tag with
            | 1 -> x.MainColor <- ValueCodec.Enum<Test.Name.Space.Enums.Color>.ReadValue reader
            | 2 -> x.OtherColors.AddRange ((ValueCodec.Packed ValueCodec.Enum<Test.Name.Space.Enums.Color>).ReadValue reader)
            | 3 -> x.ByName.Add ((ValueCodec.MapRecord ValueCodec.String ValueCodec.Enum<Test.Name.Space.Enums.Color>).ReadValue reader)
            | 4 -> x.Union.Set (UnionCase.Color (ValueCodec.Enum<Test.Name.Space.Enums.Color>.ReadValue reader))
            | 5 -> x.Union.Set (UnionCase.Name (ValueCodec.String.ReadValue reader))
            | 6 -> x.MaybeColor.Set (ValueCodec.Enum<Test.Name.Space.Enums.Color>.ReadValue reader)
            | _ -> reader.SkipLastField()
        member x.Build : Test.Name.Space.Enums = {
            MainColor = x.MainColor
            OtherColors = x.OtherColors.Build
            ByName = x.ByName.Build
            Union = x.Union.Build |> (Option.defaultValue UnionCase.None)
            MaybeColor = x.MaybeColor.Build
            }

let private _EnumsProto : ProtoDef<Enums> =
    // Field Definitions
    let MainColor = FieldCodec.Primitive ValueCodec.Enum<Test.Name.Space.Enums.Color> (1, "mainColor")
    let OtherColors = FieldCodec.Primitive (ValueCodec.Packed ValueCodec.Enum<Test.Name.Space.Enums.Color>) (2, "otherColors")
    let ByName = FieldCodec.Map ValueCodec.String ValueCodec.Enum<Test.Name.Space.Enums.Color> (3, "byName")
    let Union = FieldCodec.Oneof "union"
    let Color = FieldCodec.OneofCase "union" ValueCodec.Enum<Test.Name.Space.Enums.Color> (4, "color")
    let Name = FieldCodec.OneofCase "union" ValueCodec.String (5, "name")
    let MaybeColor = FieldCodec.Optional ValueCodec.Enum<Test.Name.Space.Enums.Color> (6, "maybeColor")
    // Proto Definition Implementation
    { // ProtoDef<Enums>
        Name = "Enums"
        Empty = {
            MainColor = MainColor.GetDefault()
            OtherColors = OtherColors.GetDefault()
            ByName = ByName.GetDefault()
            Union = Test.Name.Space.Enums.UnionCase.None
            MaybeColor = MaybeColor.GetDefault()
            }
        Size = fun (m: Enums) ->
            0
            + MainColor.CalcFieldSize m.MainColor
            + OtherColors.CalcFieldSize m.OtherColors
            + ByName.CalcFieldSize m.ByName
            + match m.Union with
                | Test.Name.Space.Enums.UnionCase.None -> 0
                | Test.Name.Space.Enums.UnionCase.Color v -> Color.CalcFieldSize v
                | Test.Name.Space.Enums.UnionCase.Name v -> Name.CalcFieldSize v
            + MaybeColor.CalcFieldSize m.MaybeColor
        Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: Enums) ->
            MainColor.WriteField w m.MainColor
            OtherColors.WriteField w m.OtherColors
            ByName.WriteField w m.ByName
            (match m.Union with
            | Test.Name.Space.Enums.UnionCase.None -> ()
            | Test.Name.Space.Enums.UnionCase.Color v -> Color.WriteField w v
            | Test.Name.Space.Enums.UnionCase.Name v -> Name.WriteField w v
            )
            MaybeColor.WriteField w m.MaybeColor
        Decode = fun (r: Google.Protobuf.CodedInputStream) ->
            let mutable builder = new Test.Name.Space.Enums.Builder()
            let mutable tag = 0
            while read r &tag do
                builder.Put (tag, r)
            builder.Build
        EncodeJson = fun (o: JsonOptions) ->
            let writeMainColor = MainColor.WriteJsonField o
            let writeOtherColors = OtherColors.WriteJsonField o
            let writeByName = ByName.WriteJsonField o
            let writeUnionNone = Union.WriteJsonNoneCase o
            let writeColor = Color.WriteJsonField o
            let writeName = Name.WriteJsonField o
            let writeMaybeColor = MaybeColor.WriteJsonField o
            let encode (w: System.Text.Json.Utf8JsonWriter) (m: Enums) =
                writeMainColor w m.MainColor
                writeOtherColors w m.OtherColors
                writeByName w m.ByName
                (match m.Union with
                | Test.Name.Space.Enums.UnionCase.None -> writeUnionNone w
                | Test.Name.Space.Enums.UnionCase.Color v -> writeColor w v
                | Test.Name.Space.Enums.UnionCase.Name v -> writeName w v
                )
                writeMaybeColor w m.MaybeColor
            encode
    }
[<System.Text.Json.Serialization.JsonConverter(typeof<FsGrpc.Json.MessageConverter>)>]
[<FsGrpc.Protobuf.Message>]
type Enums = {
    // Field Declarations
    [<System.Text.Json.Serialization.JsonPropertyName("mainColor")>] MainColor: Test.Name.Space.Enums.Color // (1)
    [<System.Text.Json.Serialization.JsonPropertyName("otherColors")>] OtherColors: Test.Name.Space.Enums.Color seq // (2)
    [<System.Text.Json.Serialization.JsonPropertyName("byName")>] ByName: Map<string, Test.Name.Space.Enums.Color> // (3)
    Union: Test.Name.Space.Enums.UnionCase
    [<System.Text.Json.Serialization.JsonPropertyName("maybeColor")>] MaybeColor: Test.Name.Space.Enums.Color option // (6)
    }
    with
    static member empty = _EnumsProto.Empty
    static member Proto = lazy _EnumsProto

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Google =

    [<System.Runtime.CompilerServices.IsByRefLike>]
    type Builder =
        struct
            val mutable Int32Val: OptionBuilder<int> // (1)
            val mutable StringVal: OptionBuilder<string> // (2)
            val mutable Timestamp: OptionBuilder<NodaTime.Instant> // (3)
            val mutable Duration: OptionBuilder<NodaTime.Duration> // (4)
        end
        with
        member x.Put ((tag, reader): int * Reader) =
            match tag with
            | 1 -> x.Int32Val.Set ((ValueCodec.Wrap ValueCodec.Int32).ReadValue reader)
            | 2 -> x.StringVal.Set ((ValueCodec.Wrap ValueCodec.String).ReadValue reader)
            | 3 -> x.Timestamp.Set (ValueCodec.Timestamp.ReadValue reader)
            | 4 -> x.Duration.Set (ValueCodec.Duration.ReadValue reader)
            | _ -> reader.SkipLastField()
        member x.Build : Test.Name.Space.Google = {
            Int32Val = x.Int32Val.Build
            StringVal = x.StringVal.Build
            Timestamp = x.Timestamp.Build
            Duration = x.Duration.Build
            }

let private _GoogleProto : ProtoDef<Google> =
    // Field Definitions
    let Int32Val = FieldCodec.Optional (ValueCodec.Wrap ValueCodec.Int32) (1, "int32Val")
    let StringVal = FieldCodec.Optional (ValueCodec.Wrap ValueCodec.String) (2, "stringVal")
    let Timestamp = FieldCodec.Optional ValueCodec.Timestamp (3, "timestamp")
    let Duration = FieldCodec.Optional ValueCodec.Duration (4, "duration")
    // Proto Definition Implementation
    { // ProtoDef<Google>
        Name = "Google"
        Empty = {
            Int32Val = Int32Val.GetDefault()
            StringVal = StringVal.GetDefault()
            Timestamp = Timestamp.GetDefault()
            Duration = Duration.GetDefault()
            }
        Size = fun (m: Google) ->
            0
            + Int32Val.CalcFieldSize m.Int32Val
            + StringVal.CalcFieldSize m.StringVal
            + Timestamp.CalcFieldSize m.Timestamp
            + Duration.CalcFieldSize m.Duration
        Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: Google) ->
            Int32Val.WriteField w m.Int32Val
            StringVal.WriteField w m.StringVal
            Timestamp.WriteField w m.Timestamp
            Duration.WriteField w m.Duration
        Decode = fun (r: Google.Protobuf.CodedInputStream) ->
            let mutable builder = new Test.Name.Space.Google.Builder()
            let mutable tag = 0
            while read r &tag do
                builder.Put (tag, r)
            builder.Build
        EncodeJson = fun (o: JsonOptions) ->
            let writeInt32Val = Int32Val.WriteJsonField o
            let writeStringVal = StringVal.WriteJsonField o
            let writeTimestamp = Timestamp.WriteJsonField o
            let writeDuration = Duration.WriteJsonField o
            let encode (w: System.Text.Json.Utf8JsonWriter) (m: Google) =
                writeInt32Val w m.Int32Val
                writeStringVal w m.StringVal
                writeTimestamp w m.Timestamp
                writeDuration w m.Duration
            encode
    }
[<System.Text.Json.Serialization.JsonConverter(typeof<FsGrpc.Json.MessageConverter>)>]
[<FsGrpc.Protobuf.Message>]
type Google = {
    // Field Declarations
    [<System.Text.Json.Serialization.JsonPropertyName("int32Val")>] Int32Val: int option // (1)
    [<System.Text.Json.Serialization.JsonPropertyName("stringVal")>] StringVal: string option // (2)
    [<System.Text.Json.Serialization.JsonPropertyName("timestamp")>] Timestamp: NodaTime.Instant option // (3)
    [<System.Text.Json.Serialization.JsonPropertyName("duration")>] Duration: NodaTime.Duration option // (4)
    }
    with
    static member empty = _GoogleProto.Empty
    static member Proto = lazy _GoogleProto

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module IntMap =

    [<System.Runtime.CompilerServices.IsByRefLike>]
    type Builder =
        struct
            val mutable IntMap: MapBuilder<int, string> // (1)
        end
        with
        member x.Put ((tag, reader): int * Reader) =
            match tag with
            | 1 -> x.IntMap.Add ((ValueCodec.MapRecord ValueCodec.Int32 ValueCodec.String).ReadValue reader)
            | _ -> reader.SkipLastField()
        member x.Build : Test.Name.Space.IntMap = {
            IntMap = x.IntMap.Build
            }

let private _IntMapProto : ProtoDef<IntMap> =
    // Field Definitions
    let IntMap = FieldCodec.Map ValueCodec.Int32 ValueCodec.String (1, "intMap")
    // Proto Definition Implementation
    { // ProtoDef<IntMap>
        Name = "IntMap"
        Empty = {
            IntMap = IntMap.GetDefault()
            }
        Size = fun (m: IntMap) ->
            0
            + IntMap.CalcFieldSize m.IntMap
        Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: IntMap) ->
            IntMap.WriteField w m.IntMap
        Decode = fun (r: Google.Protobuf.CodedInputStream) ->
            let mutable builder = new Test.Name.Space.IntMap.Builder()
            let mutable tag = 0
            while read r &tag do
                builder.Put (tag, r)
            builder.Build
        EncodeJson = fun (o: JsonOptions) ->
            let writeIntMap = IntMap.WriteJsonField o
            let encode (w: System.Text.Json.Utf8JsonWriter) (m: IntMap) =
                writeIntMap w m.IntMap
            encode
    }
[<System.Text.Json.Serialization.JsonConverter(typeof<FsGrpc.Json.MessageConverter>)>]
[<FsGrpc.Protobuf.Message>]
type IntMap = {
    // Field Declarations
    [<System.Text.Json.Serialization.JsonPropertyName("intMap")>] IntMap: Map<int, string> // (1)
    }
    with
    static member empty = _IntMapProto.Empty
    static member Proto = lazy _IntMapProto
