namespace RedditDailyProgrammer.C001_H
open Microsoft.FSharp.Quotations
open Microsoft.FSharp.Quotations.Patterns
open System.ComponentModel

type ViewModelBase() =
    let propertyChanged = new Event<_, _>()

    let GetPropertyName(query: Expr) =
        match query with
        | PropertyGet(a, b, list) -> b.Name
        | _ -> ""

    interface INotifyPropertyChanged with
        [<CLIEvent>]
        member this.PropertyChanged = propertyChanged.Publish
    
    abstract member OnPropertyChanged: string -> unit
    
    default this.OnPropertyChanged(propertyName: string) =
        propertyChanged.Trigger(this, new PropertyChangedEventArgs(propertyName))

    member this.OnPropertyChanged(expr: Expr) =
        let propertyName = GetPropertyName(expr)
        this.OnPropertyChanged(propertyName)
