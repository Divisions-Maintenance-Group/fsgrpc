
# FsGrpc
Idiomatic F# code generation for Protocol Buffers and gRPC

> *⚠️ this is currently a work in progress.  See the "Status" section
> for more info*

Generate idiomatic F# records from proto3 message definitions, complete with oneofs as discriminated unions, and serialize/deserialize to and from protocol buffer wire format.

## Getting Started

If using buf.build, simply add the following "remote" line to your buf.gen.yaml:
```yaml
version: v1
plugins:
  - remote: buf.build/divisions-maintenance-group/plugins/fsharp
    out: gen
```

Using `buf generate` with the above example will generate .fs files in the "gen" directory, and also a Protobuf.targets file in that directory which includes those files in correct dependency order.

You then add the following line to your .fsproj:
```xml
<Import Project="gen/Protobuf.targets" />
<ItemGroup>
    <PackageReference Include="FsGrpc" Version="0.9.0-alpha*" />
</ItemGroup>
```

## Usage in F#

You can create a record by specifying all of the fields or using `with` syntax as follows:

```fsharp
let message =
	{ MyMessage.empty with
	    Name = "a name value"
	    Description = "some string here" }
```

Serializing a message to bytes looks like this:
```fsharp
let bytes = message |> FsGrpc.encode
```

And deserializing looks like this:
```fsharp
let message: MyMessage = bytes |> FsGrpc.decode
```

You can also serialize/deserialize from a CodedOutputStream/CodedInputStream using:
```fsharp
// decode from a CodedInputStream named cis
let message = MyMessage.Proto.Decode cis

// encode to a CodedOutputStream named cos
MyMessage.Proto.Encode cos message
```



## Status
Note: This is currently a work in progress.  Code generation for protocol buffers is currently working but considered an alpha version.  gRPC and other features (such as code comments and reflection) are not complete.

The major features intended are:
- [x] Protobuf Messages as immutable F# record types
- [x] Oneofs as Discriminated Unions
- [x] proto3 optional keyword support
- [x] Support for optional wrapper types (e.g. google.protobuf.UInt32Val)
- [x] Support for well-known types Duration and Timestamp (represented using NodaTime types)
- [x] Automatic dependency-sorted inclusion of generated .fs files
- [x] Buf.build integration
- [x] Comment pass-through
- [ ] Protocol Buffer reflection
- [ ] Idiomatic functional implementation for gRPC endpoints


