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
