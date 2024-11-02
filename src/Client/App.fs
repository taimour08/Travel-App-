open Fable.Core.JsInterop
open Fable.Core
open Browser.Dom
open Browser.Types

type Entry = { Name: string; Instrument: string }

let entries = ResizeArray<Entry>()
let mutable currentIndex: int option = None

// Function to display entries in the table
let displayEntries () =
    let tbody = document.querySelector("#crudTable tbody") :?> HTMLTableSectionElement
    tbody.innerHTML <- ""
    entries |> Seq.iteri (fun index entry ->
        let row = document.createElement("tr") :?> HTMLTableRowElement
        row.innerHTML <- sprintf "<td>%s</td><td>%s</td>" entry.Name entry.Instrument
        row.onclick <- fun _ ->
            let nameInput = document.getElementById("nameInput") :?> HTMLInputElement
            let instrumentInput = document.getElementById("instrumentInput") :?> HTMLInputElement
            nameInput.value <- entry.Name
            instrumentInput.value <- entry.Instrument
            currentIndex <- Some index
        tbody.appendChild(row) |> ignore
    )

// Function to get input values from the HTML form
let getInputValues () =
    let nameInput = document.getElementById("nameInput") :?> HTMLInputElement
    let instrumentInput = document.getElementById("instrumentInput") :?> HTMLInputElement
    nameInput.value, instrumentInput.value

// Function to clear the form fields
let clearForm () =
    let nameInput = document.getElementById("nameInput") :?> HTMLInputElement
    let instrumentInput = document.getElementById("instrumentInput") :?> HTMLInputElement
    nameInput.value <- ""
    instrumentInput.value <- ""

// Function to create a new entry
let createEntry _ =
    let name, instrument = getInputValues()
    if name <> "" && instrument <> "" then
        entries.Add({ Name = name; Instrument = instrument })
        clearForm()
        displayEntries ()

// Function to update an existing entry
let updateEntry _ =
    match currentIndex with
    | Some idx ->
        let name, instrument = getInputValues()
        entries.[idx] <- { Name = name; Instrument = instrument }
        clearForm()
        displayEntries ()
        currentIndex <- None
    | None -> ()

// Function to delete an existing entry
let deleteEntry _ =
    match currentIndex with
    | Some idx ->
        entries.RemoveAt(idx)
        clearForm()
        displayEntries ()
        currentIndex <- None
    | None -> ()

// Event listeners for buttons
let initialize () =
    let createBtn = document.querySelector(".btn-create") :?> HTMLButtonElement
    let updateBtn = document.querySelector(".btn-update") :?> HTMLButtonElement
    let deleteBtn = document.querySelector(".btn-delete") :?> HTMLButtonElement

    createBtn.onclick <- createEntry
    updateBtn.onclick <- updateEntry
    deleteBtn.onclick <- deleteEntry

initialize ()
