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
            val mutable TestBytes: BytesBuilder // (5)
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
            | 5 -> x.TestBytes.Set (ValueCodec.Bytes.ReadValue reader)
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
            TestBytes = x.TestBytes.Build
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
    let TestInt = FieldCodec.Primitive ValueCodec.Int32 1
    let TestDouble = FieldCodec.Primitive ValueCodec.Double 2
    let TestFixed32 = FieldCodec.Primitive ValueCodec.Fixed32 3
    let TestString = FieldCodec.Primitive ValueCodec.String 4
    let TestBytes = FieldCodec.Primitive ValueCodec.Bytes 5
    let TestFloat = FieldCodec.Primitive ValueCodec.Float 6
    let TestInt64 = FieldCodec.Primitive ValueCodec.Int64 7
    let TestUint64 = FieldCodec.Primitive ValueCodec.UInt64 8
    let TestFixed64 = FieldCodec.Primitive ValueCodec.Fixed64 9
    let TestBool = FieldCodec.Primitive ValueCodec.Bool 10
    let TestUint32 = FieldCodec.Primitive ValueCodec.UInt32 11
    let TestSfixed32 = FieldCodec.Primitive ValueCodec.SFixed32 12
    let TestSfixed64 = FieldCodec.Primitive ValueCodec.SFixed64 13
    let TestSint32 = FieldCodec.Primitive ValueCodec.SInt32 14
    let TestSint64 = FieldCodec.Primitive ValueCodec.SInt64 15
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
        }
type TestMessage = {
    // Field Declarations
    TestInt: int // (1)
    TestDouble: double // (2)
    TestFixed32: uint // (3)
    TestString: string // (4)
    TestBytes: Google.Protobuf.ByteString // (5)
    TestFloat: float32 // (6)
    TestInt64: int64 // (7)
    TestUint64: uint64 // (8)
    TestFixed64: uint64 // (9)
    TestBool: bool // (10)
    TestUint32: uint32 // (11)
    TestSfixed32: int // (12)
    TestSfixed64: int64 // (13)
    TestSint32: int // (14)
    TestSint64: int64 // (15)
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
        let InnerName = FieldCodec.Primitive ValueCodec.String 1
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
            }
    type Inner = {
        // Field Declarations
        InnerName: string // (1)
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
    let Name = FieldCodec.Primitive ValueCodec.String 1
    let Children = FieldCodec.Repeated ValueCodec.Message<Test.Name.Space.Nest> 2
    let Inner = FieldCodec.Optional ValueCodec.Message<Test.Name.Space.Nest.Inner> 3
    let Special = FieldCodec.Optional ValueCodec.Message<Test.Name.Space.Special> 4
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
        }
type Nest = {
    // Field Declarations
    Name: string // (1)
    Children: Test.Name.Space.Nest seq // (2)
    Inner: Test.Name.Space.Nest.Inner option // (3)
    Special: Test.Name.Space.Special option // (4)
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
    let IntList = FieldCodec.Primitive (ValueCodec.Packed ValueCodec.Int32) 1
    let DoubleList = FieldCodec.Primitive (ValueCodec.Packed ValueCodec.Double) 2
    let Fixed32List = FieldCodec.Primitive (ValueCodec.Packed ValueCodec.Fixed32) 3
    let StringList = FieldCodec.Repeated ValueCodec.String 4
    let Dictionary = FieldCodec.Map ValueCodec.String ValueCodec.String 16
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
        }
type Special = {
    // Field Declarations
    IntList: int seq // (1)
    DoubleList: double seq // (2)
    Fixed32List: uint seq // (3)
    StringList: string seq // (4)
    Dictionary: Map<string, string> // (16)
    }
    with
    static member empty = _SpecialProto.Empty
    static member Proto = lazy _SpecialProto

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Enums =

    [<CompilationRepresentation(CompilationRepresentationFlags.UseNullAsTrueValue)>]
    [<StructuralEquality;NoComparison>]
    [<RequireQualifiedAccess>]
    type UnionCase =
    | None
    | Color of Test.Name.Space.Enums.Color
    | Name of string

    type Color =
    | Black = 0
    | Red = 1
    | Green = 2
    | Blue = 3

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
    let MainColor = FieldCodec.Primitive ValueCodec.Enum<Test.Name.Space.Enums.Color> 1
    let OtherColors = FieldCodec.Primitive (ValueCodec.Packed ValueCodec.Enum<Test.Name.Space.Enums.Color>) 2
    let ByName = FieldCodec.Map ValueCodec.String ValueCodec.Enum<Test.Name.Space.Enums.Color> 3
    let Color = FieldCodec.Optional ValueCodec.Enum<Test.Name.Space.Enums.Color> (* oneof union *) 4
    let Name = FieldCodec.Optional ValueCodec.String (* oneof union *) 5
    let MaybeColor = FieldCodec.Optional ValueCodec.Enum<Test.Name.Space.Enums.Color> 6
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
                | Test.Name.Space.Enums.UnionCase.Color v -> Color.CalcFieldSize (Some v)
                | Test.Name.Space.Enums.UnionCase.Name v -> Name.CalcFieldSize (Some v)
            + MaybeColor.CalcFieldSize m.MaybeColor
        Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: Enums) ->
            MainColor.WriteField w m.MainColor
            OtherColors.WriteField w m.OtherColors
            ByName.WriteField w m.ByName
            (match m.Union with
            | Test.Name.Space.Enums.UnionCase.None -> ()
            | Test.Name.Space.Enums.UnionCase.Color v -> Color.WriteField w (Some v)
            | Test.Name.Space.Enums.UnionCase.Name v -> Name.WriteField w (Some v)
            )
            MaybeColor.WriteField w m.MaybeColor
        Decode = fun (r: Google.Protobuf.CodedInputStream) ->
            let mutable builder = new Test.Name.Space.Enums.Builder()
            let mutable tag = 0
            while read r &tag do
                builder.Put (tag, r)
            builder.Build
        }
type Enums = {
    // Field Declarations
    MainColor: Test.Name.Space.Enums.Color // (1)
    OtherColors: Test.Name.Space.Enums.Color seq // (2)
    ByName: Map<string, Test.Name.Space.Enums.Color> // (3)
    Union: Test.Name.Space.Enums.UnionCase
    MaybeColor: Test.Name.Space.Enums.Color option // (6)
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
    let Int32Val = FieldCodec.Optional (ValueCodec.Wrap ValueCodec.Int32) 1
    let StringVal = FieldCodec.Optional (ValueCodec.Wrap ValueCodec.String) 2
    let Timestamp = FieldCodec.Optional ValueCodec.Timestamp 3
    let Duration = FieldCodec.Optional ValueCodec.Duration 4
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
        }
type Google = {
    // Field Declarations
    Int32Val: int option // (1)
    StringVal: string option // (2)
    Timestamp: NodaTime.Instant option // (3)
    Duration: NodaTime.Duration option // (4)
    }
    with
    static member empty = _GoogleProto.Empty
    static member Proto = lazy _GoogleProto
