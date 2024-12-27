module src.ThothExtension

open Thoth.Json.Net
open src.Models

module InputWord =
    let decode jv  =
        jv |> Decode.object (fun get ->
            {
                Word = get.Required.Field "word" Decode.string
                Hint = get.Required.Field "hint" Decode.string
        })
module InputWords =
    let decode jv =
        jv |> Decode.object (fun get -> get.Required.Field "words" (Decode.list InputWord.decode))
        

let loadAndDecodeJson json =
    json
    |> Decode.fromString InputWords.decode 
    |> Result.toOption
        