module FsGrpc.ProtobufTests
open Helpers

open System
open Xunit
open FsGrpc.Protobuf

open Test.Name.Space

[<Fact>]
let ``ValueCodec.Int32 can read multibyte varint`` () =
    let r = readerFromHex "87ad4b"
    let actual = ValueCodec.Int32.ReadValue r
    Assert.Equal(1234567, actual)

[<Fact>]
let ``Can get the size of a message containing a map`` () =
    let size = Special.Proto.Force().Size { Special.empty with Dictionary = Map [("one", "uno"); ("two", "dos")]}
    Assert.Equal(26, size)

[<Fact>]
let ``Can get the size of a message containing a packed`` () =
    let size = Special.Proto.Force().Size { Special.empty with IntList = [|1; 2; 3|] }
    Assert.Equal(5, size)

[<Fact>]
let ``Can get the size of a message containing a repeated`` () =
    let size = Special.Proto.Force().Size { Special.empty with StringList = [|"one"; "two"; "three"|] }
    Assert.Equal(17, size)

[<Fact>]
let ``Can handle unknown fields in Timestamp`` () =
    let expected = { Google.empty with Timestamp = Some (NodaTime.Instant.FromUnixTimeSeconds 1)}
    let hex = "1a 05 0801 980601"
    let decoded : Google = hex |> bytesFromHex |> decode
    Assert.Equal(expected, decoded)

[<Fact>]
let ``Can handle unknown fields in Duration`` () =
    let expected = { Google.empty with Duration = Some (NodaTime.Duration.FromSeconds 1.0)}
    let hex = "22 05 0801 980601"
    let decoded : Google = hex |> bytesFromHex |> decode
    Assert.Equal(expected, decoded)

[<Fact>]
let ``Can handle unknown fields in map record`` () =
    let expected = { Special.empty with Dictionary = Map [("", "")]}
    let hex = "8201 03 980601"
    let decoded : Special = hex |> bytesFromHex |> decode
    Assert.Equal(expected, decoded)

[<Fact>]
let ``Can handle unknown fields in wrapped primitive`` () =
    let expected = { Google.empty with Int32Val = Some 1}
    let hex = "0a 05 0801 980601"
    let decoded : Google = hex |> bytesFromHex |> decode
    Assert.Equal(expected, decoded)

[<Fact>]
let ``Can encode the test message to bytes`` () =
    let (expected, message) = TestCases.Value1
    let encoded = encode message
    let hex = bytesToHex encoded
    Assert.Equal(expected, hex)
    Assert.Equal("2204f09fa494", TestCases.Value2 |> encode |> bytesToHex)

[<Fact>]
let ``Can decode the test message from bytes`` () =
    let (hex, expected) = TestCases.Value1
    let encoded = readerFromHex hex
    let message = TestMessage.Proto.Force().Decode encoded
    Assert.Equal(expected, message)
    Assert.Equal(TestCases.Value2, "2204f09fa494" |> readerFromHex |> TestMessage.Proto.Force().Decode)

[<Fact>]
let ``Can calculate the correct size of a message`` () =
    let (hex, message) = TestCases.Value1
    let expected = (bytesFromHex hex).Length
    Assert.Equal(expected, TestMessage.Proto.Force().Size message)
    Assert.Equal(6, TestMessage.Proto.Force().Size TestCases.Value2)

[<Fact>]
let ``Cannot create Packed of NonPacked`` () =
    Assert.Throws<System.Exception>(fun () -> ValueCodec.Packed ValueCodec.String |> ignore)

[<Fact>]
let ``Cannot create Repeated of Packed`` () =
    Assert.Throws<System.Exception>(fun () -> FieldCodec.Repeated ValueCodec.Int32 (1, "repeat") |> ignore)

[<Fact>]
let ``Can round trip test messages`` () =
    let (_, Value1) = TestCases.Value1
    Assert.Equal(Value1, Value1 |> roundTrip)
    Assert.Equal(TestCases.Value2, TestCases.Value2 |> roundTrip)
    Assert.Equal(TestCases.Value3, TestCases.Value3 |> roundTrip)
    Assert.Equal(TestCases.Value4, TestCases.Value4 |> roundTrip)
    Assert.Equal(TestCases.Value5, TestCases.Value5 |> roundTrip)
    Assert.Equal(TestCases.Value6, TestCases.Value6 |> roundTrip)

[<Fact>]
let ``Can handle split packed fields`` () =
    let hex = "0a030102030 a03040506"
    let actual : Special = hex |> bytesFromHex |> decode
    let expected = { Special.empty with IntList = [|1; 2; 3; 4; 5; 6|]}
    Assert.Equal(expected, actual)

[<Fact>]
let ``Default for a message value is empty`` () =
    let defined = ValueCodec.MessageFrom TestMessage.Proto
    let defVal = defined.GetDefault()
    Assert.Equal(defVal, TestMessage.empty)

[<Fact>]
let ``Nondefault tests for a message value are correct`` () =
    let defined = ValueCodec.MessageFrom TestMessage.Proto
    Assert.False(defined.IsNonDefault TestMessage.empty)
    Assert.True(defined.IsNonDefault {TestMessage.empty with TestInt = 1})

[<Fact>]
let ``Repeated builder`` () =
    let mutable builder: RepeatedBuilder<int> = Unchecked.defaultof<_>
    builder.AddRange [1; 2];
    builder.AddRange (seq [3; 4]);
    builder.AddRange (seq [5]);
    let actual = builder.Build
    let expected = seq [1; 2; 3; 4; 5]
    Assert.Equal<int seq> (expected, actual)
    
    let mutable builder: RepeatedBuilder<int> = Unchecked.defaultof<_>
    builder.AddRange [|1; 2|];
    builder.Add 3
    let actual = builder.Build
    let expected = seq [1; 2; 3]
    Assert.Equal<int seq> (expected, actual)    

[<Fact>]
let ``Nulls handled by orEmptyString`` () =
    let s1 = "" |> orEmptyString
    let s2 = null |> orEmptyString
    let s3 = "A" |> orEmptyString
    Assert.Equal (("", "", "A"), (s1, s2, s3))

[<Fact>]
let ``Size of message with a oneof`` () =
    let actual = Enums.Proto.Force().Size TestCases.Value5
    let expected = 33
    Assert.Equal(expected, actual)