
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

The above example will generate .fs files in the "gen" directory, and also a Protobuf.targets file in that directory which includes those files in correct dependency order.

You then add the following line to your .fsproj:
```xml
<Import Project="gen/Protobuf.targets" />
```

Reading a message of type `MyMessage`  from a `CodedInputStream` named `cis` looks like this:

```fsharp
let message = MyMessage.Proto.Decode cis
```

Creating a new message uses with syntax like this:

```fsharp
let message =
	{ MyMessage.empty with
	    Name = "a name value"
	    Description = "some string here" }
```

Serializing a message to bytes looks like this:
```fsharp
let bytes = FsGrpc.encode message
```

## Status
Note: This is currently a work in progress.  Code generation for protocol buffers is currently working but considered an alpha version.  gRPC and other features (such as code comments and reflection) are not complete.

The major features intended are:
- [x] Protobuf Messages as immutable F# record types
- [x] Oneofs as Discriminated Unions
- [x] proto3 optional keyword support
- [x] Support for optional wrapper types (e.g. google.protobuf.UInt32Val)
- [x] Support for well-known types Duration and Timestamp (represented using NodaTime)
- [x] Automatic dependency-sorted inclusion of generated .fs files
- [x] Buf.build integration
- [ ] Comment pass-through
- [ ] Protocol Buffer reflection
- [ ] Idiomatic functional implementation for gRPC endpoints


