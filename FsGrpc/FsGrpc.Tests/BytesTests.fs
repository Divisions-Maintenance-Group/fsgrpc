module rec FsGrpc.BytesTests
open Helpers

open System
open Xunit
open FsGrpc.Protobuf

open Test.Name.Space

[<Fact>]
let ``Bytes equality works`` () =
    let b1 = FsGrpc.Bytes.FromUtf8 "hello"
    let b2 = FsGrpc.Bytes.FromUtf8 "world"
    let b3 = FsGrpc.Bytes.CopyFrom "hello"B
    let b4 = FsGrpc.Bytes.Empty
    let r1 = {| Bytes = b1 |}
    let r3 = {| Bytes = b3 |}
    Assert.Equal(b1, b3)
    Assert.NotEqual(b1, b2)
    Assert.NotEqual(b1, b4)
    Assert.Equal(b4, Unchecked.defaultof<Bytes>)
    Assert.Equal(Unchecked.defaultof<Bytes>, b4)
    Assert.Equal(b4, FsGrpc.Bytes.CopyFrom ""B)
    Assert.Equal(b4, FsGrpc.Bytes.FromUtf8 "")
    Assert.Equal(b4, FsGrpc.Bytes.FromBase64 "")
    Assert.Equal(r1, r3)
    // force obj.equals
    Assert.True((b1 :> obj).Equals(b3))
    // force obj.equals with non-bytes object
    Assert.False((b1 :> obj).Equals("hello"B))

[<Fact>]
let ``Bytes to base64 works`` () =
    let b1 = FsGrpc.Bytes.CopyFrom "hello"B
    let expected = "aGVsbG8="
    let actual = b1.Base64
    Assert.Equal(expected, actual)

[<Fact>]
let ``Bytes from base64 works`` () =
    let expected = FsGrpc.Bytes.CopyFrom "hello"B
    let actual = Bytes.FromBase64("aGVsbG8=")
    Assert.Equal(expected, actual)

[<Fact>]
let ``Bytes hash code works`` () =
    let b1 = FsGrpc.Bytes.FromUtf8 "hello"
    let b2 = FsGrpc.Bytes.FromUtf8 "world"
    let b3 = FsGrpc.Bytes.CopyFrom "hello"B
    Assert.Equal(b1.GetHashCode(), b3.GetHashCode())
    // Note: it is possible that these legitimately give the same hash code
    //       but it is improbable enough that if it does, you should check to make sure the hash function is good
    Assert.NotEqual(b1.GetHashCode(), b2.GetHashCode())
