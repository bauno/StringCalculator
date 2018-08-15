module Acceptance.Steps

open StringCalculator.Core
open StringCalculator.Main
open System.Collections.Generic

open Chessie.ErrorHandling
open FsUnit

let mutable (res: int option) = None
let mutable (message: string option) = None
let mutable logList = new List<string>();

let logLine line =
    logList.Add(line)

let logAndNotifyStub message = 
    log' logLine

let calc = calc' (log' logLine)

let ``Given I have entered`` numStr continuation =
    continuation(numStr)

let ``into the calculator`` numStr = 
    numStr
    |> calc
    |> function
    | Ok (sum,_) -> res <- Some sum                    
    | Bad msgs -> message <- Some msgs.[0]

let ``Then the result is`` (result) =
    res.Value |> should equal result
    message.IsNone |> should be True
    logList.[0] |> should equal (sprintf "Success: (%i, [])" res.Value)
    

let ``Then I fail with`` exceptionMessage =
    message.Value |> should equal exceptionMessage
    res.IsNone |> should equal true
    logList.[0] |> should equal (sprintf "Error: [\"%s\"]" message.Value)
    