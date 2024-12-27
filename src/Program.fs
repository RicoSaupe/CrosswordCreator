module Program

open CrosswordCreator
open System.IO
open Argu
open src.ThothExtension


type CLIArguments =
    | [<Mandatory>] File of string
    interface IArgParserTemplate with
        member s.Usage =
            match s with
            | File _ -> "Specify the path to the JSON file."

[<EntryPoint>]
let main argv =

    let parser = ArgumentParser.Create<CLIArguments>()
    let results = parser.Parse(argv)
    let filePath = results.GetResult(<@ File @>)
    let json = File.ReadAllText(filePath)
    let content = loadAndDecodeJson json
    
    match content with
    | Some wordList ->
        let basePuzzle = createEmptyPuzzle wordList
        let finalPuzzle = createPuzzles wordList basePuzzle
        match finalPuzzle with
        | Ok p ->
            match p |> Seq.tryHead with
            | None ->
                printfn "Oops! Unable to create a crossword puzzle with the given inputs!"
                2
            | Some (puzzle) ->
                let content = puzzleToHtml puzzle "Test"
                File.WriteAllText(@"C:\Dev\tmp\cross\index.html", content, System.Text.Encoding.UTF8)
                0
        | Error reason ->
            printfn "Oops! Something unexpected happened while trying to create your puzzle!"
            printfn "Reason: %s" reason
            3
    | None -> 1
