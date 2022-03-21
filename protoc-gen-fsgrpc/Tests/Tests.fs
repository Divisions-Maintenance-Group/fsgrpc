module Tests

open System
open Xunit

type LaunchResult = {
    ExitCode: int
    Output: string
    Error: string
}

let run cmd args : Result<LaunchResult, string> =
    let start =
        System.Diagnostics.ProcessStartInfo(
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            FileName = cmd,
            Arguments = args
        )
    let outputs = System.Collections.Generic.List<string>()
    let errors = System.Collections.Generic.List<string>()
    let outHandler f (_sender:obj) (args:System.Diagnostics.DataReceivedEventArgs) = f args.Data
    let p = new System.Diagnostics.Process(StartInfo = start)
    p.OutputDataReceived.AddHandler(System.Diagnostics.DataReceivedEventHandler (outHandler outputs.Add))
    p.ErrorDataReceived.AddHandler(System.Diagnostics.DataReceivedEventHandler (outHandler errors.Add))
    let started = p.Start()
    if not started then
        Error $"Failed to start process"
    else
        p.BeginOutputReadLine()
        p.BeginErrorReadLine()
        p.WaitForExit()
        let result = {
            ExitCode = p.ExitCode
            Output = outputs |> String.concat "\n"
            Error = errors |> String.concat "\n"
        }
        Ok result

let hashOf (file: string) =
    use md5 = System.Security.Cryptography.MD5.Create()
    use stream = System.IO.File.OpenRead(file)
    let hash = md5.ComputeHash(stream)
    BitConverter.ToString(hash).Replace("-","").ToLower()

let contentsOfFolder folder =
    let combine p1 p2 =
        System.IO.Path.Combine(p1, p2)
    let essentials (short, path, full, hash) =
        (path, hash)
    let rec contentsOfFolder root folder =
        let fileInfo (full: string) =
            let short = System.IO.Path.GetFileName(full)
            let path = combine folder short
            let hash = hashOf full
            (short, path, full, hash)
        let combined = System.IO.Path.Combine(root, folder)
        let files =
            System.IO.Directory.GetFiles(combined)
            |> Seq.map fileInfo
            |> List.ofSeq
            |> List.sort
        let subdirs =
            System.IO.Directory.GetDirectories(combined)
            |> Seq.map System.IO.Path.GetFileName
            |> Seq.map (combine folder)
            |> List.ofSeq
            |> List.sort
        let result =
            match subdirs with
            | [] ->
                files
            | subdirs ->
                files @
                (subdirs |> List.map (contentsOfFolder root) |> List.concat)
        result
    contentsOfFolder folder "" |> Seq.map essentials

[<Fact>]
let ``Generates correct files`` () =
    // check that we can run protoc
    let result = run "inputs/protoc" "--version"
    let result =
        match result with
        | Error e -> failwith e
        | Ok result -> result
    Assert.Equal(0, result.ExitCode)
    // Note: other versions are probably fine, but for now we assert that it works with this version
    Assert.Equal("libprotoc 3.19.1\n", result.Output)

    // lookup where the assembly is of the main project's output that we have as a dependency
    // this will give us the dll output, but the exe should be next to it
    let assembly = System.Reflection.Assembly.GetAssembly (typeof<ProtocGenFsgrpc.ProtoCodeGen.TypeInfo>)
    Assert.NotNull(assembly)
    Assert.NotNull(assembly.Location)
    Assert.NotEmpty(assembly.Location)

    let fullPathToPlugin = System.IO.Path.Combine [|(System.IO.Path.GetDirectoryName assembly.Location); "protoc-gen-fsgrpc"|]

    let outFolder = System.IO.Path.Combine [|(System.IO.Path.GetTempPath ());  "actual"|]

    if System.IO.Directory.Exists (outFolder) then
        System.IO.Directory.Delete (outFolder, true)

    System.IO.Directory.CreateDirectory(outFolder) |> ignore

    let result = run "inputs/protoc" $"--plugin=protoc-gen-fsgrpc={fullPathToPlugin} -Iinclude -Iinputs/proto --fsgrpc_out={outFolder} example.proto importable/importMe.proto"
    let result =
        match result with
        | Error e -> failwith e
        | Ok result -> result
    match result.ExitCode with
    | 0 -> ()
    | code ->
        printfn "Process returned: %d" code
        printfn "%s" result.Error
        printfn "%s" result.Output
    Assert.Equal(0, result.ExitCode)
    
    let reference = contentsOfFolder "reference"
    let actual = contentsOfFolder outFolder

    // make sure the actual files match the reference files
    Assert.Equal<string * string>(reference, actual)

    if System.IO.Directory.Exists (outFolder) then
        System.IO.Directory.Delete (outFolder, true)

type Nested = Ex.Ample.Outer.Nested

[<Fact>]
let ``Basic roundtrip decoding and encoding work from generated file`` () =
    let expected : Ex.Ample.Outer =
        {
            BoolVal = true
            BytesVal = Google.Protobuf.ByteString.CopyFromUtf8("test")
            Doubles = [|1.1; 2.2|]
            DoubleVal = 3.3;
            Duration = Some (NodaTime.Duration.FromDays 1)
            EnumImported = Ex.Ample.Importable.Imported.EnumForImport.Yes
            EnumVal = Ex.Ample.EnumType.One
            FloatVal = 123.45f(* float32 *)
            Imported = Some
                { Ex.Ample.Importable.Imported.empty with
                    Value = "chévere" } (* Ex.Ample.Importable.Imported option *)
            Inner = Some
                { Ex.Ample.Inner.empty with
                    IntFixed = 123
                    LongFixed = 234L
                    ZigzagInt = -456
                    ZigzagLong = -567L
                    Nested = Some
                        { Nested.empty with
                            Enums = [| Ex.Ample.Outer.NestEnumeration.Blue; Ex.Ample.Outer.NestEnumeration.Red |]
                            Inner = Some
                                { Ex.Ample.Inner.empty with IntFixed = 789 }
                            }
                    NestedEnum = Ex.Ample.Outer.NestEnumeration.Blue
                    }
            Inners = [|
                { Ex.Ample.Inner.empty with
                    IntFixed = 123 }
                { Ex.Ample.Inner.empty with
                    IntFixed = 321 }
                |]
            IntVal = 1234(* int *)
            LongVal = 4567L (* int64 *)
            Map = Map [
                ("1", "one")
                ("2", "two")
                ]
            MapBool = Map [
                (true, "yep")
                (false, "nope")
                ]
            MapInner = Map [
                ("one", { Ex.Ample.Inner.empty with IntFixed = 1 })
                ("two", { Ex.Ample.Inner.empty with IntFixed = 2 })
                ]
            MapInts = Map [
                (1, 1)
                (2, 2)
                ]
            MaybeBool = Some false
            MaybeBytes = None
            MaybeDouble = Some 1.111
            MaybeFloat = None
            MaybeInt32 = Some 0
            MaybeInt64 = None
            MaybesInt64 = [|1234; 0|]
            MaybeString = None
            MaybeUint32 = None
            MaybeUint64 = None
            Nested = Some {
                Enums = []
                Inner = Some
                    { Ex.Ample.Inner.empty with IntFixed = 789 }
                }
            OptionalInt32 = Some 0
            Recursive = Some { Ex.Ample.Outer.empty with StringVal = "Hi" }
            StringVal = "There"
            Timestamp = Some (NodaTime.Instant.FromUnixTimeSeconds 100000)
            Timestamps = [|
                (NodaTime.Instant.FromUnixTimeSeconds 100000)
                (NodaTime.Instant.FromUnixTimeSeconds 100001)
                |]
            UintFixed = 123456u
            UintVal = 123456u
            UlongFixed = 12345UL
            UlongVal = 12345UL
            Union = Ex.Ample.Outer.UnionCase.StringOption "World"
            }
    let actual = expected |> FsGrpc.encode |> FsGrpc.decode
    Assert.Equal(expected, actual)