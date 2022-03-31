module ProtocGenFsgrpc.Main

open FsGrpc
open Google.Protobuf
open System.IO

let doDump toFile : Google.Protobuf.Compiler.CodeGeneratorResponse =
    eprintfn $"Dumping to {toFile}"
    use stream = System.Console.OpenStandardInput(0)
    use output = new MemoryStream();
    stream.CopyTo(output)
    output.Seek(0, SeekOrigin.Begin) |> ignore
    File.WriteAllBytes(toFile, output.ToArray())
    Compiler.CodeGeneratorResponse.empty

let doGeneration (fromFile: string option) : Google.Protobuf.Compiler.CodeGeneratorResponse =
    eprintfn $"Generating..."
    use stream =
        match fromFile with
        | None ->
            System.Console.OpenStandardInput(0)
        | Some fromFile ->
            eprintfn $"Reading from {fromFile}"
            File.OpenRead(fromFile) :> System.IO.Stream
    use cis = new CodedInputStream(stream)
    let request = Compiler.CodeGeneratorRequest.Proto.Force().Decode cis
    let protoFiles = request.ProtoFiles |> Seq.map ProtoGenFsgrpc.Model.FileDef.From
    let typeMap = ProtoCodeGen.createTypeMap protoFiles
    let isFileToGen file = Seq.exists (fun g -> g = file) request.FilesToGenerate
    let filesToGen =
        protoFiles
        |> Seq.filter (fun f -> isFileToGen f.Name)

    let targetsPath = $"Protobuf.targets"
    eprintf $"Generating: {targetsPath}..."
    let targetsContents = ProtoCodeGen.generateTargetsFile filesToGen request
    let targets = (targetsPath, targetsContents)
    eprintfn $" Done"

    let codeFiles =
        filesToGen
        |> Seq.map (fun inFile ->
            let outFilename = $"{inFile.Name}.gen.fs"
            let outPath = $"{outFilename}"
            eprintf $"Generating: {outPath}..."
            let contents = ProtoCodeGen.generateFile inFile typeMap request
            eprintfn $" Done"
            (outFilename, contents)
        )
        |> Seq.toList
    
    let outFiles =
        [targets] @ codeFiles
    
    let outFiles =
        outFiles
        |> Seq.map (fun (path, contents) ->
            let file =
                { Compiler.CodeGeneratorResponse.File.empty with
                    Name = path
                    Content = contents }
            file
        )
    
    let response =
        { Compiler.CodeGeneratorResponse.empty with
            Files = outFiles }
    response

let parseDebug (debugStr: string) =
    match debugStr with
    | null -> None
    | str ->
        let parts = str.Split([|','|], 2) |> Seq.map (fun s -> s.Trim()) |> List.ofSeq
        match parts with
        | [] -> None
        | inFile :: outPath :: _ ->
            Some (inFile, outPath)
        | inFile :: _ ->
            Some (inFile, ".")

let createFolderOf (path: string) =
    let parent = System.IO.Path.GetDirectoryName path
    if not (Directory.Exists parent) then
        Directory.CreateDirectory parent |> ignore

let createFile (path: string) (contents: string) =
    createFolderOf path
    System.IO.File.WriteAllText(path, contents)
    
(*
    Behavior can be controlled by some environment variables to aid in debugging

    DUMP=file
        Dump the received CodeGeneratorRequest to a file to later run with an attached debugger
    DEBUG=file,folder
        Use the CodeGeneratorRequest dumped to <file> and generate the output to <folder>
*)
let main =
    let dump = System.Environment.GetEnvironmentVariable("DUMP")
    let debug = System.Environment.GetEnvironmentVariable("DEBUG");
    let args = parseDebug debug
    let response =
        match dump with
        | null ->
            let (fromFile, outFolder) =
                match args with
                | None -> (None, None)
                | Some (fromFile, outFolder) -> (Some fromFile, Some outFolder)
            let response = doGeneration fromFile
            let response =
                match outFolder with
                | None -> response
                | Some outFolder ->
                    for file in response.Files do
                        let path = System.IO.Path.Combine(outFolder, file.Name)
                        createFile path file.Content
                    { response with Files = seq []}
            response
        | _ -> doDump dump
    let response =
        { response with SupportedFeatures = 1UL }
    let encoded = Protobuf.encode response
    use stdout = System.Console.OpenStandardOutput()
    stdout.Write(encoded)

//main