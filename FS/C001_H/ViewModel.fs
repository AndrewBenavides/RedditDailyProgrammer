namespace RedditDailyProgrammer.C001_H
open System
open System.Collections.ObjectModel
open System.Windows

type ViewModel() =
    let mutable dateField: string = ""
    let mutable descriptionField: string = ""
    let events = new ObservableCollection<Model.Event>()

    let alwaysExecute = fun (_: obj) -> true

    let postEvent date =
        let event = new Model.Event(date, descriptionField)
        let index = Seq.tryFindIndex (fun (i: Model.Event) -> i.DateTime > date) events
        if index.IsSome then
            events.Insert(index.Value, event)
        else
            events.Add(event)

    let removedSelectedEvents = fun (_: obj) ->
        let selectedEvents = List.ofSeq (Seq.filter(fun (i: Model.Event) -> i.IsSelected = true) events)
        for event in selectedEvents do events.Remove(event) |> ignore

    let validateDate = fun (_: obj) ->
        let valid, date = DateTimeOffset.TryParse(dateField)
        if valid then 
            postEvent date
        else
            MessageBox.Show("Input date is not in a recognized format.") |> ignore

    member this.DateField
        with get() = dateField
        and set(value) =
            dateField <- value

    member this.DescriptionField
        with get() = descriptionField
        and set(value) =
            descriptionField <- value

    member this.Events = events

    member this.PostEvent =
        new RelayCommand(
            alwaysExecute
            ,validateDate
            )

    member this.RemoveEvents =
        new RelayCommand(
            alwaysExecute
            ,removedSelectedEvents
            )
