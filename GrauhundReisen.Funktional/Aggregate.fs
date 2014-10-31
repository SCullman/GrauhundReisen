namespace GrauhundReisen.Funktional

module Aggregate = 
    module Projection = 
        let first = List.pick
        let latest p l = List.rev l |> first p
    
    type Aggregate<'e> = 
        { History : list<'e>
          Changes : list<'e> }
    
    let getChanges aggregate = aggregate.Changes
    
    let fromHistory events = 
        { History = events
          Changes = [] }
    
    let startWith e = 
        { History = [ e ]
          Changes = [ e ] }
    
    let append e a = 
        { History = List.append a.History [ e ]
          Changes = List.append a.Changes [ e ] }
    
    let latest p a = a.History |> Projection.latest p
    let first p a = a.History |> Projection.first p
