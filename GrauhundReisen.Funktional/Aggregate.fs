namespace GrauhundReisen.Funktional

open Events

module Aggregate = 
    type History = Event list
    
    type Changes = Event list
    
    type Aggregate = History * Changes
    
    let getChanges aggregate = 
        let _, c = aggregate
        c
    
    let append list event = list |> List.append [ event ]
    let (<&) = append
    let fromHistory events = events, []
    
    let rec first (p : 'e -> 'a option) (l : 'e list) : 'a  = 
        match l with
        | head :: tail -> 
            p head |> function 
            | Some x ->  x
            | _ -> first p tail
        | [] -> None.Value
    
    let latest p l = List.rev l |> first p
