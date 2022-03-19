namespace ProtocGenFsgrpc

module CodeTree =
    type CodeNode =
        // a line is the simplest code node
        | Line of string
        // a fragment is a collection of code nodes
        | Frag of CodeNode seq
        // a block is just a code node, but is one that is indented one level
        | Block of CodeNode seq

    let rec private recursiveRender (indentStr: string) (indent: int) (node: CodeNode) =
        let indentation = String.replicate indent indentStr
        let fragRender = recursiveRender indentStr indent
        let blockRender = recursiveRender indentStr (indent + 1)
        match node with
        | Line "" -> "\n"
        | Line line -> $"{indentation}{line}\n"
        | Frag frag -> frag |> Seq.map fragRender |> String.concat ""
        | Block block -> blockRender (Frag block)

    let public createRenderer (indentStr: string) =
        recursiveRender indentStr