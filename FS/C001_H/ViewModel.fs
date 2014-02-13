namespace RedditDailyProgrammer.C001_H
open System
open System.Collections.ObjectModel
open System.Windows

type ViewModel() =
    inherit ViewModelBase()
    
    let mutable title: string = "ViewModel Default Title"
    let mutable dateField: string = ""
    let mutable descriptionField: string = ""
    let events = new ObservableCollection<DateTimeOffset>()

    let alwaysExecute = fun (_: obj) -> true

    let validateDate = fun (_: obj) ->
        let valid, date = DateTimeOffset.TryParse(dateField)
        if valid then 
            events.Add(date)
        else
            MessageBox.Show("Input date is not in a recognized format.") |> ignore

    member this.Title
        with get() = title
        and set(value) =
            if value <> title then
                title <- value
                this.OnPropertyChanged(<@ this.Title @>)

    member this.DateField
        with get() = dateField
        and set(value) =
            dateField <- value

    member this.DescriptionField
        with get() = descriptionField
        and set(value) =
            descriptionField <- value

    member this.Events =
        events

//    member this.ShowMessage() =
//        MessageBox.Show("???") |> ignore
//        ()

    member this.PostEvent =
        new RelayCommand(
            alwaysExecute
            ,validateDate
            )

//    member this.MyCommand =
//        new RelayCommand((fun _ -> true), (fun _ -> this.ShowMessage()))
    