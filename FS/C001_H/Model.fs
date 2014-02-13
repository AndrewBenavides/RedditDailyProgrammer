namespace RedditDailyProgrammer.C001_H
module Model =
    open System

    type Event(date: DateTimeOffset, description: string) =
        let mutable isSelected = false

        member this.DateTime = date
        member this.Description = description
        member this.IsSelected
            with get() = isSelected
            and set(value) = isSelected <- value