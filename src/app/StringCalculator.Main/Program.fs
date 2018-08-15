module Calculator


open StringCalculator.Main
open Chessie.ErrorHandling
open System

[<EntryPoint>]
let main argv =
    let rec readLine (calculator: string -> Result<int,string>) =            
        Console.ReadLine()
        |> function
        | "" -> exit 0
        | input -> input 
                   |> calculator 
                   |> function
                   | Ok (res, _) -> printfn "The result is %i" res
                   | Bad(_) -> printfn "That didn't go well. See the logs"
        printf "Another input please: "
        readLine calculator
    System.Console.Write("Input please: ")
    calc |> readLine 
    0 // return an integer exit code
