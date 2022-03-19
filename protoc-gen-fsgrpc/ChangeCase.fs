module ChangeCase

let private WordSplit = new System.Text.RegularExpressions.Regex(@"[^a-zA-Z0-9]+|(?<=[a-z])(?=[A-Z])|(?<=[A-Z])(?=[A-Z][a-z])|(?<=[0-9])(?=[^0-9])|(?<=[a-zA-Z])(?=[0-9])")

let public toWords (input: string) =
    WordSplit.Split(input) :> seq<string>

let public toLower (input: string) =
    input.ToLower()

let public capitalizeFirst (input: string) =
    match input.Length with
    | 0 -> ""
    | 1 -> input.ToUpper()
    | _ -> input[0].ToString().ToUpper() + input[1..]

let public toPascalCase (name: string) =
    let join = String.concat ""
    name |> toWords |> Seq.map (toLower >> capitalizeFirst) |> join
