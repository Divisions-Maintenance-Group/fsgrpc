module DependencySort

type GetDeps<'T> = 'T -> 'T seq

type DepthVal =
| Depth of int
| Recursive

let depSort<'T when 'T : equality> (depOf: GetDeps<'T>) =
    let dependencyLevel =
        let rec dependencyLevel (path: 'T list) (thing: 'T) : DepthVal  =
            if path |> Seq.contains thing then
                Recursive
            else
                let recurse = (dependencyLevel (thing :: path))
                let deps = depOf thing
                if deps |> Seq.isEmpty then
                    Depth 0
                else
                    let maxdep = deps |> Seq.maxBy recurse |> recurse
                    match maxdep with
                    | Recursive -> Recursive
                    | Depth n -> Depth (n + 1)
        dependencyLevel []
    let sortBy (thing: 'T) =
        (dependencyLevel thing, thing)
    sortBy