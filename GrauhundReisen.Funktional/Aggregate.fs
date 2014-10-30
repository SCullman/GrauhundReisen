namespace GrauhundReisen.Funktional

open Events

module Aggregate = 
    module Projection = 
        let rec first (p : 'e -> 'a option) (l : 'e list) : 'a = 
            match l with
            | head :: tail -> 
                p head |> function 
                | Some x -> x
                | _ -> first p tail
            | [] -> None.Value
        
        let latest p l = List.rev l |> first p
    
    type History = Event list
    
    type Changes = Event list
    
    type Aggregate = History * Changes
    
    let getChanges aggregate = 
        let _, c = aggregate
        c
    
    let getHistory aggregate = 
        let h, _ = aggregate
        h
    
    let fromHistory events = events, []
    let startWith (e : Event) = [ e ], [ e ]
    
    let append (e : Event) a = 
        let h, c = a
        List.append h [ e ], List.append c [ e ]
    
    let latest p a = 
        a
        |> getHistory
        |> Projection.latest p
    
    let first p a = 
        a
        |> getHistory
        |> Projection.first p
