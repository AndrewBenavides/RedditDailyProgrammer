namespace RedditDailyProgrammer.C001_H
open System.Windows.Controls
open System.Windows.Input

type RelayCommand(canExecute: (obj -> bool), action: (obj -> unit)) =
    let canExecuteChangedEvent = new Event<_, _>()

    interface ICommand with
        member this.Execute arg = action(arg)
        member this.CanExecute arg = canExecute(arg)
        [<CLIEvent>]
        member this.CanExecuteChanged = canExecuteChangedEvent.Publish
