module internal StringCalculator.Core

open System
open System.Text.RegularExpressions
open Chessie.ErrorHandling

let parseNumbers ((delims: string[]),numStr) = 
    if String.IsNullOrEmpty(numStr)
    then ok [|0|]
    else try 
             numStr.Split(Seq.toArray delims, StringSplitOptions.RemoveEmptyEntries) 
             |> Seq.map(fun num -> Convert.ToInt32(num))
             |> Seq.toArray         
             |> ok
         with
          | e -> fail e.Message



let trimDelimiters str =         
    let rec trimDelimiter accumulator currentIndex (currentString:string) =        
        if currentIndex = currentString.Length
            then List.rev accumulator |> List.toArray
        else
            match currentString.[currentIndex] with
            | ']' -> let delim = currentString.Substring(1,currentIndex-1)                 
                     trimDelimiter (delim::accumulator) 0 (currentString.Substring(currentIndex+1))
            | _ ->   trimDelimiter accumulator (currentIndex+1) currentString    
    trimDelimiter [] 0 str
    
let parseDelimiter input : Result<string[]*string,string>=     
    let delimRx = "^\/\/(\[.+\])*\n(.*)$"
    if (Regex.IsMatch(input, delimRx, RegexOptions.Multiline))
    then let matches = Regex.Match(input, delimRx, RegexOptions.Multiline)
         let delim = matches.Groups.[1].Value |> trimDelimiters   
         printfn "Delim: %A" delim
         let inputRemainder = matches.Groups.[2].Value
         printfn "Remainder: %s" inputRemainder
         ok (delim,inputRemainder)
    else ok ([|",";"\n"|],input)

let filterNegs numbers =
    let pos, neg = Array.partition (fun n -> n >= 0) numbers
    if Array.isEmpty neg
    then ok pos
    else neg 
         |> Array.map (fun n -> Convert.ToString(n))
         |> String.concat " "
         |> sprintf "Cannot add this negative numbers: %s" 
         |> fail


let filterSize =
    Array.filter (fun n -> n < 1000)

let logWriter =
    printfn "%s"

let log' writer input =
    let logSuccess item = writer (sprintf "Success: %A" item)
    let logError item = writer (sprintf "Error: %A" item)
    try
        eitherTee logSuccess logError input
    with
    | e -> fail e.Message

let errorSender' (sender: string list -> unit) error = 
    failureTee sender error