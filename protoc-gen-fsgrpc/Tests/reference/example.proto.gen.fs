[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module rec Ex.Ample
open FsGrpc.Protobuf
#nowarn "40"


/// <summary>This is an enumeration</summary>
[<System.Text.Json.Serialization.JsonConverter(typeof<FsGrpc.Json.EnumConverter<EnumType>>)>]
type EnumType =
/// <summary>This is a (default) enumeraton option</summary>
| [<FsGrpc.Json.ProtobufName("ENUM_TYPE_NONE")>] None = 0
/// <summary>This is another enumeration option</summary>
| [<FsGrpc.Json.ProtobufName("ENUM_TYPE_ONE")>] One = 1
| [<FsGrpc.Json.ProtobufName("ENUM_TYPE_TWO")>] Two = 2

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Inner =

    [<System.Runtime.CompilerServices.IsByRefLike>]
    type Builder =
        struct
            val mutable IntFixed: int // (13)
            val mutable LongFixed: int64 // (14)
            val mutable ZigzagInt: int // (15)
            val mutable ZigzagLong: int64 // (16)
            val mutable Nested: OptionBuilder<Ex.Ample.Outer.Nested> // (17)
            val mutable NestedEnum: Ex.Ample.Outer.NestEnumeration // (18)
        end
        with
        member x.Put ((tag, reader): int * Reader) =
            match tag with
            | 13 -> x.IntFixed <- ValueCodec.SFixed32.ReadValue reader
            | 14 -> x.LongFixed <- ValueCodec.SFixed64.ReadValue reader
            | 15 -> x.ZigzagInt <- ValueCodec.SInt32.ReadValue reader
            | 16 -> x.ZigzagLong <- ValueCodec.SInt64.ReadValue reader
            | 17 -> x.Nested.Set (ValueCodec.Message<Ex.Ample.Outer.Nested>.ReadValue reader)
            | 18 -> x.NestedEnum <- ValueCodec.Enum<Ex.Ample.Outer.NestEnumeration>.ReadValue reader
            | _ -> reader.SkipLastField()
        member x.Build : Ex.Ample.Inner = {
            IntFixed = x.IntFixed
            LongFixed = x.LongFixed
            ZigzagInt = x.ZigzagInt
            ZigzagLong = x.ZigzagLong
            Nested = x.Nested.Build
            NestedEnum = x.NestedEnum
            }

let private _InnerProto : ProtoDef<Inner> =
    // Field Definitions
    let IntFixed = FieldCodec.Primitive ValueCodec.SFixed32 (13, "intFixed")
    let LongFixed = FieldCodec.Primitive ValueCodec.SFixed64 (14, "longFixed")
    let ZigzagInt = FieldCodec.Primitive ValueCodec.SInt32 (15, "zigzagInt")
    let ZigzagLong = FieldCodec.Primitive ValueCodec.SInt64 (16, "zigzagLong")
    let Nested = FieldCodec.Optional ValueCodec.Message<Ex.Ample.Outer.Nested> (17, "nested")
    let NestedEnum = FieldCodec.Primitive ValueCodec.Enum<Ex.Ample.Outer.NestEnumeration> (18, "nestedEnum")
    // Proto Definition Implementation
    { // ProtoDef<Inner>
        Name = "Inner"
        Empty = {
            IntFixed = IntFixed.GetDefault()
            LongFixed = LongFixed.GetDefault()
            ZigzagInt = ZigzagInt.GetDefault()
            ZigzagLong = ZigzagLong.GetDefault()
            Nested = Nested.GetDefault()
            NestedEnum = NestedEnum.GetDefault()
            }
        Size = fun (m: Inner) ->
            0
            + IntFixed.CalcFieldSize m.IntFixed
            + LongFixed.CalcFieldSize m.LongFixed
            + ZigzagInt.CalcFieldSize m.ZigzagInt
            + ZigzagLong.CalcFieldSize m.ZigzagLong
            + Nested.CalcFieldSize m.Nested
            + NestedEnum.CalcFieldSize m.NestedEnum
        Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: Inner) ->
            IntFixed.WriteField w m.IntFixed
            LongFixed.WriteField w m.LongFixed
            ZigzagInt.WriteField w m.ZigzagInt
            ZigzagLong.WriteField w m.ZigzagLong
            Nested.WriteField w m.Nested
            NestedEnum.WriteField w m.NestedEnum
        Decode = fun (r: Google.Protobuf.CodedInputStream) ->
            let mutable builder = new Ex.Ample.Inner.Builder()
            let mutable tag = 0
            while read r &tag do
                builder.Put (tag, r)
            builder.Build
        EncodeJson = fun (o: System.Text.Json.JsonSerializerOptions) ->
            let writeIntFixed = IntFixed.WriteJsonField o
            let writeLongFixed = LongFixed.WriteJsonField o
            let writeZigzagInt = ZigzagInt.WriteJsonField o
            let writeZigzagLong = ZigzagLong.WriteJsonField o
            let writeNested = Nested.WriteJsonField o
            let writeNestedEnum = NestedEnum.WriteJsonField o
            let encode (w: System.Text.Json.Utf8JsonWriter) (m: Inner) =
                writeIntFixed w m.IntFixed
                writeLongFixed w m.LongFixed
                writeZigzagInt w m.ZigzagInt
                writeZigzagLong w m.ZigzagLong
                writeNested w m.Nested
                writeNestedEnum w m.NestedEnum
            encode
    }
/// <summary>
/// This is a comment
///    that has multiple lines, where subsequent lines
///    exhibit indentation
/// 
/// We want to ensure that the indentation
///    of comments like these
///    is preserved
/// </summary>
[<System.Text.Json.Serialization.JsonConverter(typeof<FsGrpc.Json.MessageConverter>)>]
type Inner = {
    // Field Declarations
    [<System.Text.Json.Serialization.JsonPropertyName("intFixed")>] IntFixed: int // (13)
    [<System.Text.Json.Serialization.JsonPropertyName("longFixed")>] LongFixed: int64 // (14)
    [<System.Text.Json.Serialization.JsonPropertyName("zigzagInt")>] ZigzagInt: int // (15)
    [<System.Text.Json.Serialization.JsonPropertyName("zigzagLong")>] ZigzagLong: int64 // (16)
    [<System.Text.Json.Serialization.JsonPropertyName("nested")>] Nested: Ex.Ample.Outer.Nested option // (17)
    [<System.Text.Json.Serialization.JsonPropertyName("nestedEnum")>] NestedEnum: Ex.Ample.Outer.NestEnumeration // (18)
    }
    with
    static member empty = _InnerProto.Empty
    static member Proto = lazy _InnerProto

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Outer =

    [<System.Text.Json.Serialization.JsonConverter(typeof<FsGrpc.Json.OneofConverter<UnionCase>>)>]
    [<CompilationRepresentation(CompilationRepresentationFlags.UseNullAsTrueValue)>]
    [<StructuralEquality;NoComparison>]
    [<RequireQualifiedAccess>]
    type UnionCase =
    | None
    /// <summary>a oneof option that is a message</summary>
    | [<System.Text.Json.Serialization.JsonPropertyName("innerOption")>] InnerOption of Ex.Ample.Inner
    /// <summary>a oneof option that is a string</summary>
    | [<System.Text.Json.Serialization.JsonPropertyName("stringOption")>] StringOption of string
    /// <summary>a message type from another file</summary>
    | [<System.Text.Json.Serialization.JsonPropertyName("importedOption")>] ImportedOption of Ex.Ample.Importable.Args

    [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
    module Nested =

        [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
        module DoubleNested =

            [<System.Runtime.CompilerServices.IsByRefLike>]
            type Builder =
                struct
                end
                with
                member x.Put ((tag, reader): int * Reader) =
                    match tag with
                    | _ -> reader.SkipLastField()
                member x.Build = _DoubleNestedProto.Empty

        let private _DoubleNestedProto : ProtoDef<DoubleNested> =
            // Field Definitions
            // Proto Definition Implementation
            { // ProtoDef<DoubleNested>
                Name = "DoubleNested"
                Empty = DoubleNested.empty
                Size = fun (m: DoubleNested) ->
                    0
                Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: DoubleNested) ->
                    ()
                Decode = fun (r: Google.Protobuf.CodedInputStream) ->
                    let mutable tag = 0
                    while read r &tag do
                        r.SkipLastField()
                    DoubleNested.empty
                EncodeJson = fun _ _ _ -> ()
            }
        type DoubleNested private() =
            override _.Equals other : bool = other :? DoubleNested
            override _.GetHashCode() : int = 424431930
            static member empty = new DoubleNested()

        [<System.Runtime.CompilerServices.IsByRefLike>]
        type Builder =
            struct
                val mutable Enums: RepeatedBuilder<Ex.Ample.Outer.NestEnumeration> // (1)
                val mutable Inner: OptionBuilder<Ex.Ample.Inner> // (2)
            end
            with
            member x.Put ((tag, reader): int * Reader) =
                match tag with
                | 1 -> x.Enums.AddRange ((ValueCodec.Packed ValueCodec.Enum<Ex.Ample.Outer.NestEnumeration>).ReadValue reader)
                | 2 -> x.Inner.Set (ValueCodec.Message<Ex.Ample.Inner>.ReadValue reader)
                | _ -> reader.SkipLastField()
            member x.Build : Ex.Ample.Outer.Nested = {
                Enums = x.Enums.Build
                Inner = x.Inner.Build
                }

    let private _NestedProto : ProtoDef<Nested> =
        // Field Definitions
        let Enums = FieldCodec.Primitive (ValueCodec.Packed ValueCodec.Enum<Ex.Ample.Outer.NestEnumeration>) (1, "enums")
        let Inner = FieldCodec.Optional ValueCodec.Message<Ex.Ample.Inner> (2, "inner")
        // Proto Definition Implementation
        { // ProtoDef<Nested>
            Name = "Nested"
            Empty = {
                Enums = Enums.GetDefault()
                Inner = Inner.GetDefault()
                }
            Size = fun (m: Nested) ->
                0
                + Enums.CalcFieldSize m.Enums
                + Inner.CalcFieldSize m.Inner
            Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: Nested) ->
                Enums.WriteField w m.Enums
                Inner.WriteField w m.Inner
            Decode = fun (r: Google.Protobuf.CodedInputStream) ->
                let mutable builder = new Ex.Ample.Outer.Nested.Builder()
                let mutable tag = 0
                while read r &tag do
                    builder.Put (tag, r)
                builder.Build
            EncodeJson = fun (o: System.Text.Json.JsonSerializerOptions) ->
                let writeEnums = Enums.WriteJsonField o
                let writeInner = Inner.WriteJsonField o
                let encode (w: System.Text.Json.Utf8JsonWriter) (m: Nested) =
                    writeEnums w m.Enums
                    writeInner w m.Inner
                encode
        }
    [<System.Text.Json.Serialization.JsonConverter(typeof<FsGrpc.Json.MessageConverter>)>]
    type Nested = {
        // Field Declarations
        [<System.Text.Json.Serialization.JsonPropertyName("enums")>] Enums: Ex.Ample.Outer.NestEnumeration seq // (1)
        [<System.Text.Json.Serialization.JsonPropertyName("inner")>] Inner: Ex.Ample.Inner option // (2)
        }
        with
        static member empty = _NestedProto.Empty
        static member Proto = lazy _NestedProto

    /// <summary>this enumeration is nested under another class</summary>
    [<System.Text.Json.Serialization.JsonConverter(typeof<FsGrpc.Json.EnumConverter<NestEnumeration>>)>]
    type NestEnumeration =
    | [<FsGrpc.Json.ProtobufName("NEST_ENUMERATION_BLACK")>] Black = 0
    | [<FsGrpc.Json.ProtobufName("NEST_ENUMERATION_RED")>] Red = 1
    | [<FsGrpc.Json.ProtobufName("NEST_ENUMERATION_BLUE")>] Blue = 2

    [<System.Runtime.CompilerServices.IsByRefLike>]
    type Builder =
        struct
            val mutable DoubleVal: double // (1)
            val mutable FloatVal: float32 // (2)
            val mutable LongVal: int64 // (3)
            val mutable UlongVal: uint64 // (4)
            val mutable IntVal: int // (5)
            val mutable UlongFixed: uint64 // (6)
            val mutable UintFixed: uint // (7)
            val mutable BoolVal: bool // (8)
            val mutable StringVal: string // (9)
            val mutable BytesVal: FsGrpc.Bytes // (10)
            val mutable UintVal: uint32 // (11)
            val mutable EnumVal: Ex.Ample.EnumType // (12)
            val mutable Inner: OptionBuilder<Ex.Ample.Inner> // (17)
            val mutable Doubles: RepeatedBuilder<double> // (18)
            val mutable Inners: RepeatedBuilder<Ex.Ample.Inner> // (19)
            val mutable Map: MapBuilder<string, string> // (20)
            val mutable MapInner: MapBuilder<string, Ex.Ample.Inner> // (21)
            val mutable MapInts: MapBuilder<int64, int> // (22)
            val mutable MapBool: MapBuilder<bool, string> // (23)
            val mutable Recursive: OptionBuilder<Ex.Ample.Outer> // (24)
            val mutable Union: OptionBuilder<Ex.Ample.Outer.UnionCase>
            val mutable Nested: OptionBuilder<Ex.Ample.Outer.Nested> // (27)
            val mutable Imported: OptionBuilder<Ex.Ample.Importable.Imported> // (28)
            val mutable EnumImported: Ex.Ample.Importable.Imported.EnumForImport // (29)
            val mutable MaybeDouble: OptionBuilder<double> // (33)
            val mutable MaybeFloat: OptionBuilder<float32> // (34)
            val mutable MaybeInt64: OptionBuilder<int64> // (35)
            val mutable MaybeUint64: OptionBuilder<uint64> // (36)
            val mutable MaybeInt32: OptionBuilder<int> // (37)
            val mutable MaybeUint32: OptionBuilder<uint32> // (38)
            val mutable MaybeBool: OptionBuilder<bool> // (39)
            val mutable MaybeString: OptionBuilder<string> // (40)
            val mutable MaybeBytes: OptionBuilder<FsGrpc.Bytes> // (41)
            val mutable Timestamp: OptionBuilder<NodaTime.Instant> // (42)
            val mutable Duration: OptionBuilder<NodaTime.Duration> // (43)
            val mutable OptionalInt32: OptionBuilder<int> // (44)
            val mutable MaybesInt64: RepeatedBuilder<int64> // (45)
            val mutable Timestamps: RepeatedBuilder<NodaTime.Instant> // (46)
        end
        with
        member x.Put ((tag, reader): int * Reader) =
            match tag with
            | 1 -> x.DoubleVal <- ValueCodec.Double.ReadValue reader
            | 2 -> x.FloatVal <- ValueCodec.Float.ReadValue reader
            | 3 -> x.LongVal <- ValueCodec.Int64.ReadValue reader
            | 4 -> x.UlongVal <- ValueCodec.UInt64.ReadValue reader
            | 5 -> x.IntVal <- ValueCodec.Int32.ReadValue reader
            | 6 -> x.UlongFixed <- ValueCodec.Fixed64.ReadValue reader
            | 7 -> x.UintFixed <- ValueCodec.Fixed32.ReadValue reader
            | 8 -> x.BoolVal <- ValueCodec.Bool.ReadValue reader
            | 9 -> x.StringVal <- ValueCodec.String.ReadValue reader
            | 10 -> x.BytesVal <- ValueCodec.Bytes.ReadValue reader
            | 11 -> x.UintVal <- ValueCodec.UInt32.ReadValue reader
            | 12 -> x.EnumVal <- ValueCodec.Enum<Ex.Ample.EnumType>.ReadValue reader
            | 17 -> x.Inner.Set (ValueCodec.Message<Ex.Ample.Inner>.ReadValue reader)
            | 18 -> x.Doubles.AddRange ((ValueCodec.Packed ValueCodec.Double).ReadValue reader)
            | 19 -> x.Inners.Add (ValueCodec.Message<Ex.Ample.Inner>.ReadValue reader)
            | 20 -> x.Map.Add ((ValueCodec.MapRecord ValueCodec.String ValueCodec.String).ReadValue reader)
            | 21 -> x.MapInner.Add ((ValueCodec.MapRecord ValueCodec.String ValueCodec.Message<Ex.Ample.Inner>).ReadValue reader)
            | 22 -> x.MapInts.Add ((ValueCodec.MapRecord ValueCodec.Int64 ValueCodec.Int32).ReadValue reader)
            | 23 -> x.MapBool.Add ((ValueCodec.MapRecord ValueCodec.Bool ValueCodec.String).ReadValue reader)
            | 24 -> x.Recursive.Set (ValueCodec.Message<Ex.Ample.Outer>.ReadValue reader)
            | 25 -> x.Union.Set (UnionCase.InnerOption (ValueCodec.Message<Ex.Ample.Inner>.ReadValue reader))
            | 26 -> x.Union.Set (UnionCase.StringOption (ValueCodec.String.ReadValue reader))
            | 30 -> x.Union.Set (UnionCase.ImportedOption (ValueCodec.Message<Ex.Ample.Importable.Args>.ReadValue reader))
            | 27 -> x.Nested.Set (ValueCodec.Message<Ex.Ample.Outer.Nested>.ReadValue reader)
            | 28 -> x.Imported.Set (ValueCodec.Message<Ex.Ample.Importable.Imported>.ReadValue reader)
            | 29 -> x.EnumImported <- ValueCodec.Enum<Ex.Ample.Importable.Imported.EnumForImport>.ReadValue reader
            | 33 -> x.MaybeDouble.Set ((ValueCodec.Wrap ValueCodec.Double).ReadValue reader)
            | 34 -> x.MaybeFloat.Set ((ValueCodec.Wrap ValueCodec.Float).ReadValue reader)
            | 35 -> x.MaybeInt64.Set ((ValueCodec.Wrap ValueCodec.Int64).ReadValue reader)
            | 36 -> x.MaybeUint64.Set ((ValueCodec.Wrap ValueCodec.UInt64).ReadValue reader)
            | 37 -> x.MaybeInt32.Set ((ValueCodec.Wrap ValueCodec.Int32).ReadValue reader)
            | 38 -> x.MaybeUint32.Set ((ValueCodec.Wrap ValueCodec.UInt32).ReadValue reader)
            | 39 -> x.MaybeBool.Set ((ValueCodec.Wrap ValueCodec.Bool).ReadValue reader)
            | 40 -> x.MaybeString.Set ((ValueCodec.Wrap ValueCodec.String).ReadValue reader)
            | 41 -> x.MaybeBytes.Set ((ValueCodec.Wrap ValueCodec.Bytes).ReadValue reader)
            | 42 -> x.Timestamp.Set (ValueCodec.Timestamp.ReadValue reader)
            | 43 -> x.Duration.Set (ValueCodec.Duration.ReadValue reader)
            | 44 -> x.OptionalInt32.Set (ValueCodec.Int32.ReadValue reader)
            | 45 -> x.MaybesInt64.Add ((ValueCodec.Wrap ValueCodec.Int64).ReadValue reader)
            | 46 -> x.Timestamps.Add (ValueCodec.Timestamp.ReadValue reader)
            | _ -> reader.SkipLastField()
        member x.Build : Ex.Ample.Outer = {
            DoubleVal = x.DoubleVal
            FloatVal = x.FloatVal
            LongVal = x.LongVal
            UlongVal = x.UlongVal
            IntVal = x.IntVal
            UlongFixed = x.UlongFixed
            UintFixed = x.UintFixed
            BoolVal = x.BoolVal
            StringVal = x.StringVal |> orEmptyString
            BytesVal = x.BytesVal
            UintVal = x.UintVal
            EnumVal = x.EnumVal
            Inner = x.Inner.Build
            Doubles = x.Doubles.Build
            Inners = x.Inners.Build
            Map = x.Map.Build
            MapInner = x.MapInner.Build
            MapInts = x.MapInts.Build
            MapBool = x.MapBool.Build
            Recursive = x.Recursive.Build
            Union = x.Union.Build |> (Option.defaultValue UnionCase.None)
            Nested = x.Nested.Build
            Imported = x.Imported.Build
            EnumImported = x.EnumImported
            MaybeDouble = x.MaybeDouble.Build
            MaybeFloat = x.MaybeFloat.Build
            MaybeInt64 = x.MaybeInt64.Build
            MaybeUint64 = x.MaybeUint64.Build
            MaybeInt32 = x.MaybeInt32.Build
            MaybeUint32 = x.MaybeUint32.Build
            MaybeBool = x.MaybeBool.Build
            MaybeString = x.MaybeString.Build
            MaybeBytes = x.MaybeBytes.Build
            Timestamp = x.Timestamp.Build
            Duration = x.Duration.Build
            OptionalInt32 = x.OptionalInt32.Build
            MaybesInt64 = x.MaybesInt64.Build
            Timestamps = x.Timestamps.Build
            }

let private _OuterProto : ProtoDef<Outer> =
    // Field Definitions
    let DoubleVal = FieldCodec.Primitive ValueCodec.Double (1, "doubleVal")
    let FloatVal = FieldCodec.Primitive ValueCodec.Float (2, "floatVal")
    let LongVal = FieldCodec.Primitive ValueCodec.Int64 (3, "longVal")
    let UlongVal = FieldCodec.Primitive ValueCodec.UInt64 (4, "ulongVal")
    let IntVal = FieldCodec.Primitive ValueCodec.Int32 (5, "intVal")
    let UlongFixed = FieldCodec.Primitive ValueCodec.Fixed64 (6, "ulongFixed")
    let UintFixed = FieldCodec.Primitive ValueCodec.Fixed32 (7, "uintFixed")
    let BoolVal = FieldCodec.Primitive ValueCodec.Bool (8, "boolVal")
    let StringVal = FieldCodec.Primitive ValueCodec.String (9, "stringVal")
    let BytesVal = FieldCodec.Primitive ValueCodec.Bytes (10, "bytesVal")
    let UintVal = FieldCodec.Primitive ValueCodec.UInt32 (11, "uintVal")
    let EnumVal = FieldCodec.Primitive ValueCodec.Enum<Ex.Ample.EnumType> (12, "enumVal")
    let Inner = FieldCodec.Optional ValueCodec.Message<Ex.Ample.Inner> (17, "inner")
    let Doubles = FieldCodec.Primitive (ValueCodec.Packed ValueCodec.Double) (18, "doubles")
    let Inners = FieldCodec.Repeated ValueCodec.Message<Ex.Ample.Inner> (19, "inners")
    let Map = FieldCodec.Map ValueCodec.String ValueCodec.String (20, "map")
    let MapInner = FieldCodec.Map ValueCodec.String ValueCodec.Message<Ex.Ample.Inner> (21, "mapInner")
    let MapInts = FieldCodec.Map ValueCodec.Int64 ValueCodec.Int32 (22, "mapInts")
    let MapBool = FieldCodec.Map ValueCodec.Bool ValueCodec.String (23, "mapBool")
    let Recursive = FieldCodec.Optional ValueCodec.Message<Ex.Ample.Outer> (24, "recursive")
    let Union = FieldCodec.Oneof "union"
    let InnerOption = FieldCodec.OneofCase "union" ValueCodec.Message<Ex.Ample.Inner> (25, "innerOption")
    let StringOption = FieldCodec.OneofCase "union" ValueCodec.String (26, "stringOption")
    let ImportedOption = FieldCodec.OneofCase "union" ValueCodec.Message<Ex.Ample.Importable.Args> (30, "importedOption")
    let Nested = FieldCodec.Optional ValueCodec.Message<Ex.Ample.Outer.Nested> (27, "nested")
    let Imported = FieldCodec.Optional ValueCodec.Message<Ex.Ample.Importable.Imported> (28, "imported")
    let EnumImported = FieldCodec.Primitive ValueCodec.Enum<Ex.Ample.Importable.Imported.EnumForImport> (29, "enumImported")
    let MaybeDouble = FieldCodec.Optional (ValueCodec.Wrap ValueCodec.Double) (33, "maybeDouble")
    let MaybeFloat = FieldCodec.Optional (ValueCodec.Wrap ValueCodec.Float) (34, "maybeFloat")
    let MaybeInt64 = FieldCodec.Optional (ValueCodec.Wrap ValueCodec.Int64) (35, "maybeInt64")
    let MaybeUint64 = FieldCodec.Optional (ValueCodec.Wrap ValueCodec.UInt64) (36, "maybeUint64")
    let MaybeInt32 = FieldCodec.Optional (ValueCodec.Wrap ValueCodec.Int32) (37, "maybeInt32")
    let MaybeUint32 = FieldCodec.Optional (ValueCodec.Wrap ValueCodec.UInt32) (38, "maybeUint32")
    let MaybeBool = FieldCodec.Optional (ValueCodec.Wrap ValueCodec.Bool) (39, "maybeBool")
    let MaybeString = FieldCodec.Optional (ValueCodec.Wrap ValueCodec.String) (40, "maybeString")
    let MaybeBytes = FieldCodec.Optional (ValueCodec.Wrap ValueCodec.Bytes) (41, "maybeBytes")
    let Timestamp = FieldCodec.Optional ValueCodec.Timestamp (42, "timestamp")
    let Duration = FieldCodec.Optional ValueCodec.Duration (43, "duration")
    let OptionalInt32 = FieldCodec.Optional ValueCodec.Int32 (44, "optionalInt32")
    let MaybesInt64 = FieldCodec.Repeated (ValueCodec.Wrap ValueCodec.Int64) (45, "maybesInt64")
    let Timestamps = FieldCodec.Repeated ValueCodec.Timestamp (46, "timestamps")
    // Proto Definition Implementation
    { // ProtoDef<Outer>
        Name = "Outer"
        Empty = {
            DoubleVal = DoubleVal.GetDefault()
            FloatVal = FloatVal.GetDefault()
            LongVal = LongVal.GetDefault()
            UlongVal = UlongVal.GetDefault()
            IntVal = IntVal.GetDefault()
            UlongFixed = UlongFixed.GetDefault()
            UintFixed = UintFixed.GetDefault()
            BoolVal = BoolVal.GetDefault()
            StringVal = StringVal.GetDefault()
            BytesVal = BytesVal.GetDefault()
            UintVal = UintVal.GetDefault()
            EnumVal = EnumVal.GetDefault()
            Inner = Inner.GetDefault()
            Doubles = Doubles.GetDefault()
            Inners = Inners.GetDefault()
            Map = Map.GetDefault()
            MapInner = MapInner.GetDefault()
            MapInts = MapInts.GetDefault()
            MapBool = MapBool.GetDefault()
            Recursive = Recursive.GetDefault()
            Union = Ex.Ample.Outer.UnionCase.None
            Nested = Nested.GetDefault()
            Imported = Imported.GetDefault()
            EnumImported = EnumImported.GetDefault()
            MaybeDouble = MaybeDouble.GetDefault()
            MaybeFloat = MaybeFloat.GetDefault()
            MaybeInt64 = MaybeInt64.GetDefault()
            MaybeUint64 = MaybeUint64.GetDefault()
            MaybeInt32 = MaybeInt32.GetDefault()
            MaybeUint32 = MaybeUint32.GetDefault()
            MaybeBool = MaybeBool.GetDefault()
            MaybeString = MaybeString.GetDefault()
            MaybeBytes = MaybeBytes.GetDefault()
            Timestamp = Timestamp.GetDefault()
            Duration = Duration.GetDefault()
            OptionalInt32 = OptionalInt32.GetDefault()
            MaybesInt64 = MaybesInt64.GetDefault()
            Timestamps = Timestamps.GetDefault()
            }
        Size = fun (m: Outer) ->
            0
            + DoubleVal.CalcFieldSize m.DoubleVal
            + FloatVal.CalcFieldSize m.FloatVal
            + LongVal.CalcFieldSize m.LongVal
            + UlongVal.CalcFieldSize m.UlongVal
            + IntVal.CalcFieldSize m.IntVal
            + UlongFixed.CalcFieldSize m.UlongFixed
            + UintFixed.CalcFieldSize m.UintFixed
            + BoolVal.CalcFieldSize m.BoolVal
            + StringVal.CalcFieldSize m.StringVal
            + BytesVal.CalcFieldSize m.BytesVal
            + UintVal.CalcFieldSize m.UintVal
            + EnumVal.CalcFieldSize m.EnumVal
            + Inner.CalcFieldSize m.Inner
            + Doubles.CalcFieldSize m.Doubles
            + Inners.CalcFieldSize m.Inners
            + Map.CalcFieldSize m.Map
            + MapInner.CalcFieldSize m.MapInner
            + MapInts.CalcFieldSize m.MapInts
            + MapBool.CalcFieldSize m.MapBool
            + Recursive.CalcFieldSize m.Recursive
            + match m.Union with
                | Ex.Ample.Outer.UnionCase.None -> 0
                | Ex.Ample.Outer.UnionCase.InnerOption v -> InnerOption.CalcFieldSize v
                | Ex.Ample.Outer.UnionCase.StringOption v -> StringOption.CalcFieldSize v
                | Ex.Ample.Outer.UnionCase.ImportedOption v -> ImportedOption.CalcFieldSize v
            + Nested.CalcFieldSize m.Nested
            + Imported.CalcFieldSize m.Imported
            + EnumImported.CalcFieldSize m.EnumImported
            + MaybeDouble.CalcFieldSize m.MaybeDouble
            + MaybeFloat.CalcFieldSize m.MaybeFloat
            + MaybeInt64.CalcFieldSize m.MaybeInt64
            + MaybeUint64.CalcFieldSize m.MaybeUint64
            + MaybeInt32.CalcFieldSize m.MaybeInt32
            + MaybeUint32.CalcFieldSize m.MaybeUint32
            + MaybeBool.CalcFieldSize m.MaybeBool
            + MaybeString.CalcFieldSize m.MaybeString
            + MaybeBytes.CalcFieldSize m.MaybeBytes
            + Timestamp.CalcFieldSize m.Timestamp
            + Duration.CalcFieldSize m.Duration
            + OptionalInt32.CalcFieldSize m.OptionalInt32
            + MaybesInt64.CalcFieldSize m.MaybesInt64
            + Timestamps.CalcFieldSize m.Timestamps
        Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: Outer) ->
            DoubleVal.WriteField w m.DoubleVal
            FloatVal.WriteField w m.FloatVal
            LongVal.WriteField w m.LongVal
            UlongVal.WriteField w m.UlongVal
            IntVal.WriteField w m.IntVal
            UlongFixed.WriteField w m.UlongFixed
            UintFixed.WriteField w m.UintFixed
            BoolVal.WriteField w m.BoolVal
            StringVal.WriteField w m.StringVal
            BytesVal.WriteField w m.BytesVal
            UintVal.WriteField w m.UintVal
            EnumVal.WriteField w m.EnumVal
            Inner.WriteField w m.Inner
            Doubles.WriteField w m.Doubles
            Inners.WriteField w m.Inners
            Map.WriteField w m.Map
            MapInner.WriteField w m.MapInner
            MapInts.WriteField w m.MapInts
            MapBool.WriteField w m.MapBool
            Recursive.WriteField w m.Recursive
            (match m.Union with
            | Ex.Ample.Outer.UnionCase.None -> ()
            | Ex.Ample.Outer.UnionCase.InnerOption v -> InnerOption.WriteField w v
            | Ex.Ample.Outer.UnionCase.StringOption v -> StringOption.WriteField w v
            | Ex.Ample.Outer.UnionCase.ImportedOption v -> ImportedOption.WriteField w v
            )
            Nested.WriteField w m.Nested
            Imported.WriteField w m.Imported
            EnumImported.WriteField w m.EnumImported
            MaybeDouble.WriteField w m.MaybeDouble
            MaybeFloat.WriteField w m.MaybeFloat
            MaybeInt64.WriteField w m.MaybeInt64
            MaybeUint64.WriteField w m.MaybeUint64
            MaybeInt32.WriteField w m.MaybeInt32
            MaybeUint32.WriteField w m.MaybeUint32
            MaybeBool.WriteField w m.MaybeBool
            MaybeString.WriteField w m.MaybeString
            MaybeBytes.WriteField w m.MaybeBytes
            Timestamp.WriteField w m.Timestamp
            Duration.WriteField w m.Duration
            OptionalInt32.WriteField w m.OptionalInt32
            MaybesInt64.WriteField w m.MaybesInt64
            Timestamps.WriteField w m.Timestamps
        Decode = fun (r: Google.Protobuf.CodedInputStream) ->
            let mutable builder = new Ex.Ample.Outer.Builder()
            let mutable tag = 0
            while read r &tag do
                builder.Put (tag, r)
            builder.Build
        EncodeJson = fun (o: System.Text.Json.JsonSerializerOptions) ->
            let writeDoubleVal = DoubleVal.WriteJsonField o
            let writeFloatVal = FloatVal.WriteJsonField o
            let writeLongVal = LongVal.WriteJsonField o
            let writeUlongVal = UlongVal.WriteJsonField o
            let writeIntVal = IntVal.WriteJsonField o
            let writeUlongFixed = UlongFixed.WriteJsonField o
            let writeUintFixed = UintFixed.WriteJsonField o
            let writeBoolVal = BoolVal.WriteJsonField o
            let writeStringVal = StringVal.WriteJsonField o
            let writeBytesVal = BytesVal.WriteJsonField o
            let writeUintVal = UintVal.WriteJsonField o
            let writeEnumVal = EnumVal.WriteJsonField o
            let writeInner = Inner.WriteJsonField o
            let writeDoubles = Doubles.WriteJsonField o
            let writeInners = Inners.WriteJsonField o
            let writeMap = Map.WriteJsonField o
            let writeMapInner = MapInner.WriteJsonField o
            let writeMapInts = MapInts.WriteJsonField o
            let writeMapBool = MapBool.WriteJsonField o
            let writeRecursive = Recursive.WriteJsonField o
            let writeUnionNone = Union.WriteJsonNoneCase o
            let writeInnerOption = InnerOption.WriteJsonField o
            let writeStringOption = StringOption.WriteJsonField o
            let writeImportedOption = ImportedOption.WriteJsonField o
            let writeNested = Nested.WriteJsonField o
            let writeImported = Imported.WriteJsonField o
            let writeEnumImported = EnumImported.WriteJsonField o
            let writeMaybeDouble = MaybeDouble.WriteJsonField o
            let writeMaybeFloat = MaybeFloat.WriteJsonField o
            let writeMaybeInt64 = MaybeInt64.WriteJsonField o
            let writeMaybeUint64 = MaybeUint64.WriteJsonField o
            let writeMaybeInt32 = MaybeInt32.WriteJsonField o
            let writeMaybeUint32 = MaybeUint32.WriteJsonField o
            let writeMaybeBool = MaybeBool.WriteJsonField o
            let writeMaybeString = MaybeString.WriteJsonField o
            let writeMaybeBytes = MaybeBytes.WriteJsonField o
            let writeTimestamp = Timestamp.WriteJsonField o
            let writeDuration = Duration.WriteJsonField o
            let writeOptionalInt32 = OptionalInt32.WriteJsonField o
            let writeMaybesInt64 = MaybesInt64.WriteJsonField o
            let writeTimestamps = Timestamps.WriteJsonField o
            let encode (w: System.Text.Json.Utf8JsonWriter) (m: Outer) =
                writeDoubleVal w m.DoubleVal
                writeFloatVal w m.FloatVal
                writeLongVal w m.LongVal
                writeUlongVal w m.UlongVal
                writeIntVal w m.IntVal
                writeUlongFixed w m.UlongFixed
                writeUintFixed w m.UintFixed
                writeBoolVal w m.BoolVal
                writeStringVal w m.StringVal
                writeBytesVal w m.BytesVal
                writeUintVal w m.UintVal
                writeEnumVal w m.EnumVal
                writeInner w m.Inner
                writeDoubles w m.Doubles
                writeInners w m.Inners
                writeMap w m.Map
                writeMapInner w m.MapInner
                writeMapInts w m.MapInts
                writeMapBool w m.MapBool
                writeRecursive w m.Recursive
                (match m.Union with
                | Ex.Ample.Outer.UnionCase.None -> writeUnionNone w
                | Ex.Ample.Outer.UnionCase.InnerOption v -> writeInnerOption w v
                | Ex.Ample.Outer.UnionCase.StringOption v -> writeStringOption w v
                | Ex.Ample.Outer.UnionCase.ImportedOption v -> writeImportedOption w v
                )
                writeNested w m.Nested
                writeImported w m.Imported
                writeEnumImported w m.EnumImported
                writeMaybeDouble w m.MaybeDouble
                writeMaybeFloat w m.MaybeFloat
                writeMaybeInt64 w m.MaybeInt64
                writeMaybeUint64 w m.MaybeUint64
                writeMaybeInt32 w m.MaybeInt32
                writeMaybeUint32 w m.MaybeUint32
                writeMaybeBool w m.MaybeBool
                writeMaybeString w m.MaybeString
                writeMaybeBytes w m.MaybeBytes
                writeTimestamp w m.Timestamp
                writeDuration w m.Duration
                writeOptionalInt32 w m.OptionalInt32
                writeMaybesInt64 w m.MaybesInt64
                writeTimestamps w m.Timestamps
            encode
    }
/// <summary>This is an "outer" message that will contain other messages</summary>
[<System.Text.Json.Serialization.JsonConverter(typeof<FsGrpc.Json.MessageConverter>)>]
type Outer = {
    // Field Declarations
    /// <summary>primitive double value</summary>
    [<System.Text.Json.Serialization.JsonPropertyName("doubleVal")>] DoubleVal: double // (1)
    /// <summary>priviate float value</summary>
    [<System.Text.Json.Serialization.JsonPropertyName("floatVal")>] FloatVal: float32 // (2)
    /// <summary>primitive int64 value</summary>
    [<System.Text.Json.Serialization.JsonPropertyName("longVal")>] LongVal: int64 // (3)
    /// <summary>primitive uint64 value</summary>
    [<System.Text.Json.Serialization.JsonPropertyName("ulongVal")>] UlongVal: uint64 // (4)
    /// <summary>primitive int32 value</summary>
    [<System.Text.Json.Serialization.JsonPropertyName("intVal")>] IntVal: int // (5)
    /// <summary>primitive fixed64 value</summary>
    [<System.Text.Json.Serialization.JsonPropertyName("ulongFixed")>] UlongFixed: uint64 // (6)
    /// <summary>primitive fixed32 value</summary>
    [<System.Text.Json.Serialization.JsonPropertyName("uintFixed")>] UintFixed: uint // (7)
    /// <summary>primitive bool value</summary>
    [<System.Text.Json.Serialization.JsonPropertyName("boolVal")>] BoolVal: bool // (8)
    /// <summary>primitive string value</summary>
    [<System.Text.Json.Serialization.JsonPropertyName("stringVal")>] StringVal: string // (9)
    /// <summary>primitive bytes value</summary>
    [<System.Text.Json.Serialization.JsonPropertyName("bytesVal")>] BytesVal: FsGrpc.Bytes // (10)
    /// <summary>primitive uint32 value</summary>
    [<System.Text.Json.Serialization.JsonPropertyName("uintVal")>] UintVal: uint32 // (11)
    /// <summary>enum value</summary>
    [<System.Text.Json.Serialization.JsonPropertyName("enumVal")>] EnumVal: Ex.Ample.EnumType // (12)
    /// <summary>message value (inner)</summary>
    [<System.Text.Json.Serialization.JsonPropertyName("inner")>] Inner: Ex.Ample.Inner option // (17)
    /// <summary>repeated of packable primitive</summary>
    [<System.Text.Json.Serialization.JsonPropertyName("doubles")>] Doubles: double seq // (18)
    /// <summary>repeated of message</summary>
    [<System.Text.Json.Serialization.JsonPropertyName("inners")>] Inners: Ex.Ample.Inner seq // (19)
    /// <summary>map with string keys</summary>
    [<System.Text.Json.Serialization.JsonPropertyName("map")>] Map: Map<string, string> // (20)
    /// <summary>map with message values</summary>
    [<System.Text.Json.Serialization.JsonPropertyName("mapInner")>] MapInner: Map<string, Ex.Ample.Inner> // (21)
    /// <summary>map with int keys</summary>
    [<System.Text.Json.Serialization.JsonPropertyName("mapInts")>] MapInts: Map<int64, int> // (22)
    /// <summary>map with bool keys (which is allowed 🤷)</summary>
    [<System.Text.Json.Serialization.JsonPropertyName("mapBool")>] MapBool: Map<bool, string> // (23)
    /// <summary>message value of the same type (recursive)</summary>
    [<System.Text.Json.Serialization.JsonPropertyName("recursive")>] Recursive: Ex.Ample.Outer option // (24)
    /// <summary>a oneof value</summary>
    Union: Ex.Ample.Outer.UnionCase
    /// <summary>a message that is defined inside this message</summary>
    [<System.Text.Json.Serialization.JsonPropertyName("nested")>] Nested: Ex.Ample.Outer.Nested option // (27)
    /// <summary>a message type that is imported from another file</summary>
    [<System.Text.Json.Serialization.JsonPropertyName("imported")>] Imported: Ex.Ample.Importable.Imported option // (28)
    /// <summary>an enumeration imported from another file</summary>
    [<System.Text.Json.Serialization.JsonPropertyName("enumImported")>] EnumImported: Ex.Ample.Importable.Imported.EnumForImport // (29)
    /// <summary>a wrapped double value (the old way of doing optional double)</summary>
    [<System.Text.Json.Serialization.JsonPropertyName("maybeDouble")>] MaybeDouble: double option // (33)
    /// <summary>a wrapped float value (the old way of doing optional float)</summary>
    [<System.Text.Json.Serialization.JsonPropertyName("maybeFloat")>] MaybeFloat: float32 option // (34)
    /// <summary>a wrapped int64 value (the old way of doing optional int64)</summary>
    [<System.Text.Json.Serialization.JsonPropertyName("maybeInt64")>] MaybeInt64: int64 option // (35)
    /// <summary>a wrapped uint64 value (the old way of doing optional uint64)</summary>
    [<System.Text.Json.Serialization.JsonPropertyName("maybeUint64")>] MaybeUint64: uint64 option // (36)
    /// <summary>a wrapped int32 value (the old way of doing optional int32)</summary>
    [<System.Text.Json.Serialization.JsonPropertyName("maybeInt32")>] MaybeInt32: int option // (37)
    /// <summary>a wrapped uint32 value (the old way of doing optional uint32)</summary>
    [<System.Text.Json.Serialization.JsonPropertyName("maybeUint32")>] MaybeUint32: uint32 option // (38)
    /// <summary>a wrapped bool value (the old way of doing optional bool)</summary>
    [<System.Text.Json.Serialization.JsonPropertyName("maybeBool")>] MaybeBool: bool option // (39)
    /// <summary>a wrapped string value (the old way of doing optional string)</summary>
    [<System.Text.Json.Serialization.JsonPropertyName("maybeString")>] MaybeString: string option // (40)
    /// <summary>a wrapped bytes value (the old way of doing optional bytes)</summary>
    [<System.Text.Json.Serialization.JsonPropertyName("maybeBytes")>] MaybeBytes: FsGrpc.Bytes option // (41)
    /// <summary>the well-known timestamp</summary>
    [<System.Text.Json.Serialization.JsonPropertyName("timestamp")>] Timestamp: NodaTime.Instant option // (42)
    /// <summary>the well-known duration</summary>
    [<System.Text.Json.Serialization.JsonPropertyName("duration")>] Duration: NodaTime.Duration option // (43)
    /// <summary>a proto3 optional int</summary>
    [<System.Text.Json.Serialization.JsonPropertyName("optionalInt32")>] OptionalInt32: int option // (44)
    /// <summary>a repeated of the old wrapped int64</summary>
    [<System.Text.Json.Serialization.JsonPropertyName("maybesInt64")>] MaybesInt64: int64 seq // (45)
    /// <summary>a repeated of the well-known timestamp</summary>
    [<System.Text.Json.Serialization.JsonPropertyName("timestamps")>] Timestamps: NodaTime.Instant seq // (46)
    }
    with
    static member empty = _OuterProto.Empty
    static member Proto = lazy _OuterProto

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module ResultEvent =

    [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
    module Record =

        [<System.Runtime.CompilerServices.IsByRefLike>]
        type Builder =
            struct
                val mutable Key: string // (1)
                val mutable Value: string // (2)
            end
            with
            member x.Put ((tag, reader): int * Reader) =
                match tag with
                | 1 -> x.Key <- ValueCodec.String.ReadValue reader
                | 2 -> x.Value <- ValueCodec.String.ReadValue reader
                | _ -> reader.SkipLastField()
            member x.Build : Ex.Ample.ResultEvent.Record = {
                Key = x.Key |> orEmptyString
                Value = x.Value |> orEmptyString
                }

    let private _RecordProto : ProtoDef<Record> =
        // Field Definitions
        let Key = FieldCodec.Primitive ValueCodec.String (1, "key")
        let Value = FieldCodec.Primitive ValueCodec.String (2, "value")
        // Proto Definition Implementation
        { // ProtoDef<Record>
            Name = "Record"
            Empty = {
                Key = Key.GetDefault()
                Value = Value.GetDefault()
                }
            Size = fun (m: Record) ->
                0
                + Key.CalcFieldSize m.Key
                + Value.CalcFieldSize m.Value
            Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: Record) ->
                Key.WriteField w m.Key
                Value.WriteField w m.Value
            Decode = fun (r: Google.Protobuf.CodedInputStream) ->
                let mutable builder = new Ex.Ample.ResultEvent.Record.Builder()
                let mutable tag = 0
                while read r &tag do
                    builder.Put (tag, r)
                builder.Build
            EncodeJson = fun (o: System.Text.Json.JsonSerializerOptions) ->
                let writeKey = Key.WriteJsonField o
                let writeValue = Value.WriteJsonField o
                let encode (w: System.Text.Json.Utf8JsonWriter) (m: Record) =
                    writeKey w m.Key
                    writeValue w m.Value
                encode
        }
    [<System.Text.Json.Serialization.JsonConverter(typeof<FsGrpc.Json.MessageConverter>)>]
    type Record = {
        // Field Declarations
        [<System.Text.Json.Serialization.JsonPropertyName("key")>] Key: string // (1)
        [<System.Text.Json.Serialization.JsonPropertyName("value")>] Value: string // (2)
        }
        with
        static member empty = _RecordProto.Empty
        static member Proto = lazy _RecordProto

    [<System.Runtime.CompilerServices.IsByRefLike>]
    type Builder =
        struct
            val mutable SubscriptionState: Ex.Ample.EnumType // (1)
            val mutable Records: RepeatedBuilder<Ex.Ample.ResultEvent.Record> // (2)
        end
        with
        member x.Put ((tag, reader): int * Reader) =
            match tag with
            | 1 -> x.SubscriptionState <- ValueCodec.Enum<Ex.Ample.EnumType>.ReadValue reader
            | 2 -> x.Records.Add (ValueCodec.Message<Ex.Ample.ResultEvent.Record>.ReadValue reader)
            | _ -> reader.SkipLastField()
        member x.Build : Ex.Ample.ResultEvent = {
            SubscriptionState = x.SubscriptionState
            Records = x.Records.Build
            }

let private _ResultEventProto : ProtoDef<ResultEvent> =
    // Field Definitions
    let SubscriptionState = FieldCodec.Primitive ValueCodec.Enum<Ex.Ample.EnumType> (1, "subscriptionState")
    let Records = FieldCodec.Repeated ValueCodec.Message<Ex.Ample.ResultEvent.Record> (2, "records")
    // Proto Definition Implementation
    { // ProtoDef<ResultEvent>
        Name = "ResultEvent"
        Empty = {
            SubscriptionState = SubscriptionState.GetDefault()
            Records = Records.GetDefault()
            }
        Size = fun (m: ResultEvent) ->
            0
            + SubscriptionState.CalcFieldSize m.SubscriptionState
            + Records.CalcFieldSize m.Records
        Encode = fun (w: Google.Protobuf.CodedOutputStream) (m: ResultEvent) ->
            SubscriptionState.WriteField w m.SubscriptionState
            Records.WriteField w m.Records
        Decode = fun (r: Google.Protobuf.CodedInputStream) ->
            let mutable builder = new Ex.Ample.ResultEvent.Builder()
            let mutable tag = 0
            while read r &tag do
                builder.Put (tag, r)
            builder.Build
        EncodeJson = fun (o: System.Text.Json.JsonSerializerOptions) ->
            let writeSubscriptionState = SubscriptionState.WriteJsonField o
            let writeRecords = Records.WriteJsonField o
            let encode (w: System.Text.Json.Utf8JsonWriter) (m: ResultEvent) =
                writeSubscriptionState w m.SubscriptionState
                writeRecords w m.Records
            encode
    }
/// <summary>
/// This is an example of a
/// multiline-style comment
/// </summary>
[<System.Text.Json.Serialization.JsonConverter(typeof<FsGrpc.Json.MessageConverter>)>]
type ResultEvent = {
    // Field Declarations
    [<System.Text.Json.Serialization.JsonPropertyName("subscriptionState")>] SubscriptionState: Ex.Ample.EnumType // (1)
    [<System.Text.Json.Serialization.JsonPropertyName("records")>] Records: Ex.Ample.ResultEvent.Record seq // (2)
    }
    with
    static member empty = _ResultEventProto.Empty
    static member Proto = lazy _ResultEventProto
