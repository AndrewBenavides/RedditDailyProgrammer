namespace RedditDailyProgrammer.C001_H
module Program =
    open FSharpx
    open System
    open System.Windows
    open System.Windows.Controls

    type MainWindow = XAML<"MainWindow.xaml">
    
    let LoadWindow() =
        let window = MainWindow()
        window.Root
    
    [<STAThread>]
    (new Application()).Run(LoadWindow()) |> ignore