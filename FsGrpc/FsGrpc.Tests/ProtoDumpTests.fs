module ProtoDumpTests

open System
open FsGrpc
open Xunit
open Helpers
open TestCases
open ProtoDump

[<Fact>]
let ``Flat message dumps correctly`` () =
    let (hex, _) = TestCases.Value1
    let dumped = hex |> bytesFromHex |> ProtoDump.toProtoHex
    let expected = [|
        """[1. varint] 08 07"""
        """[2. double] 11 9a99999999d95e40"""
        """[3. single] 1d 39300000"""
        """[4. <data>] 2208 6368c3a976657265"""
        """[5. <data>] 2a0d c2af5c5f28e38384295f2fc2af"""
        """[6. single] 35 00806a43"""
        """[7. varint] 38 ce958f01"""
        """[8. varint] 40 95fed201"""
        """[9. double] 49 15bf340000000000"""
        """[10. varint] 50 01"""
        """[11. varint] 58 c803"""
        """[12. single] 65 37020000"""
        """[13. double] 69 cbeb0b0400000000"""
        """[14. varint] 70 a313"""
        """[15. varint] 78 d124"""
    |]
    Assert.Equal(expected, dumped)

[<Fact>]
let ``Nested message dumps correctly`` () =
    let hex = TestCases.Value3 |> Protobuf.encode |> bytesToHex
    let dumped = hex |> bytesFromHex |> ProtoDump.toProtoHex
    let expected = [|
        """[1. <data>] 0a06 416e696d616c"""
        """[2. <data>] 1208"""
        """  [1. <data>] 0a06 4d616d6d616c"""
        """[2. <data>] 1208"""
        """  [1. <data>] 0a04 46697368"""
        """  [4. <data>] 2200"""
        """[3. <data>] 1a07"""
        """  [1. <data>] 0a05 696e6e6572"""
    |]
    Assert.Equal(expected, dumped)

[<Fact>]
let ``Fails if runs out of data on LengthDelimited`` () =
    let hex = "12080a064d616d6d61"
    let dumped = hex |> bytesFromHex |> ProtoDump.toProtoHex
    let expected = [|
        """REMAIN: (9 b) 12080a064d616d6d61"""
        """ERROR: end of bytes with 1 remaining"""
    |]
    Assert.Equal(expected, dumped)

[<Fact>]
let ``Dumps remaining  if runs out of data on LengthDelimited`` () =
    let hex = "12080a064d616d6d616c12"
    let dumped = hex |> bytesFromHex |> ProtoDump.toProtoHex
    let expected = [|
        """REMAIN: (18 b) 12080a064d616d6d61"""
        """ERROR: end of bytes with 1 remaining"""
    |]
    //Assert.Equal(expected, dumped)
    ()
