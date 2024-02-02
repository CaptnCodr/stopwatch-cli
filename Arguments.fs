namespace StopWatch

open Argu

module Arguments =

    [<DisableHelpFlags>]
    type CliArguments =
        | [<CliPrefix(CliPrefix.None); AltCommandLine("-s")>] Start
        | [<CliPrefix(CliPrefix.None); AltCommandLine("-b")>] Stop
        | [<CliPrefix(CliPrefix.None); AltCommandLine("-p")>] Peak
        
        | [<CliPrefix(CliPrefix.None); AltCommandLine("-v")>] Version
        
        | [<CliPrefix(CliPrefix.None); AltCommandLine("-h", "--help")>] Help

        interface IArgParserTemplate with
            member this.Usage =
                match this with
                | Start -> "Starts a new stopwatch."
                | Stop -> "Stops running stopwatch."
                | Peak -> "Shows running stopwatch."
                | Version -> "Gets the current version."
                | Help -> "Shows this help."
