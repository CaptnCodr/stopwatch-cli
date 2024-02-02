namespace StopWatch

open System
open System.IO

module FileHandler =

    [<Literal>]
    let fileName = "stopwatch.json"

    [<Literal>]
    let dateFormat = "dd.MM.yyyy HH:mm:ss.fff"

    let getConfigurationFilePath =
        Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), fileName)

    let setTime (startTime: DateTime) =
        let file = getConfigurationFilePath

        if file |> File.Exists |> not then
            file |> File.Create |> _.Close()

        use stream = new StreamWriter(file)
        startTime |> _.ToString(dateFormat) |> stream.Write

    let getTime () : Option<DateTime> =
        let file = getConfigurationFilePath

        match file |> File.Exists with
        | true ->
            use stream = new StreamReader(file)

            match (stream.ReadLine() |> DateTime.TryParse) with
            | true, dt -> Some(dt)
            | false, _ -> None
        | false -> None

    let stopTime () =
        let startTime = getTime ()

        if getConfigurationFilePath |> File.Exists then
            File.WriteAllText(getConfigurationFilePath, "")

        (startTime, DateTime.Now)
