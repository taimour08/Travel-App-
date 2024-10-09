(*App.fs: Contains the main Elmish application logic for the 
front-end, including the model, update, and view functions.*)

module App

open Elmish
open Elmish.React

open Fable.Core.JsInterop

importSideEffects "./index.css"

#if DEBUG
open Elmish.HMR
#endif

Program.mkProgram Index.init Index.update Index.view
#if DEBUG
|> Program.withConsoleTrace
#endif
|> Program.withReactSynchronous "elmish-app"

|> Program.run


(*
Elmish is a simple way to manage the state of web applications using F#. It follows the Model-View-Update (MVU) pattern, which helps organize your code by separating how the app looks (the view), how the app's state changes (the update), and what the current state is (the model).

Key Concepts:
Model: This is the current state of your app. For example, whether a button has been clicked or not.
View: This is how your app looks, based on the current state. For example, if the button was clicked, it might show some new text.
Update: This is the logic that updates your model when something happens, like a button click.
*)