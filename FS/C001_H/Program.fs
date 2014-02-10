namespace RedditDailyProgrammer.C001_H
module Program =
    open System
    open System.Windows
    open System.Windows.Controls
    open FSharpx

    type MainWindow = XAML<"MainWindow.xaml">
    
    let LoadWindow() =
        let window = MainWindow()
        window.Button1.Click.Add(fun _ ->
            MessageBox.Show("Hello World!") |> ignore
            )
        window.Root
    
    [<STAThread>]
    (new Application()).Run(LoadWindow()) |> ignore
//    let main argv = 
//        printfn "%A" argv
//        0 // return an integer exit code
