module StringCalculator.Main

open StringCalculator.Core
open Chessie.ErrorHandling


let private sendErrorStub error =
    let sendError error = printfn "Error %A notified" error
    failureTee sendError error

let private logger = log' logWriter

let private  logAndNotify = logger >> sendErrorStub

let internal calc' loggerNotifier =
    parseDelimiter 
    >> bind parseNumbers
    >> bind filterNegs
    >> lift filterSize
    >> lift Array.sum
    >> loggerNotifier

let calc = calc' logAndNotify
