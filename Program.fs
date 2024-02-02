namespace StopWatch

open Argu
open System
open System.Reflection
open StopWatch.Arguments

module Program =
    let printDateTime (dateTime: DateTime) =
        dateTime.ToString("dd.MM.yyyy HH:mm:ss.fff")

    let runCommands (parser: ArgumentParser<CliArguments>) (args: string array) =
        let parsingResult = parser.Parse args

        match parsingResult.GetAllResults() with
        | [ Start ] ->
            FileHandler.setTime DateTime.Now
            "Stopwatch started!"

        | [ Stop ] ->
            match FileHandler.stopTime () with
            | Some startTime, stoppingTime ->
                let ts = stoppingTime - startTime
                $"Stopwatch started at: {startTime |> printDateTime}{Environment.NewLine}Stopwatch stopped at: {stoppingTime |> printDateTime}{Environment.NewLine}Elapsed time: {ts}"
            | None, _ -> "Could not stop watch!"

        | [ Peak ] ->
            match FileHandler.getTime () with
            | Some dt ->
                $"Stopwatch started at: {dt |> printDateTime}{Environment.NewLine}Elapsed time: {(DateTime.Now - dt)}"
            | None -> "Stopwatch is not running!"

        | [ Version ] -> Assembly.GetExecutingAssembly().GetName().Version |> string

        | [ Help ]
        | _ -> parser.PrintUsage()

    [<EntryPoint>]
    let main ([<ParamArray>] argv: string[]) : int =

        try
            (ArgumentParser.Create<CliArguments>(), argv) ||> runCommands |> printfn "%s"
        with ex ->
            eprintfn $"{ex.Message}"

        0
