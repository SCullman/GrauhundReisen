namespace GrauhundReisen.Funktional

open Grauhundreisen.Infrastructure
open Newtonsoft.Json

module EventStore = 
    let retrieve (c : EventStoreClient) (id : string) = 
        c.RetrieveFor id
        |> Seq.map (fun o-> o :?> Events.Event)
        |> Seq.toList
    
    let store (c : EventStoreClient) (id : string) (event : Events.Event) = c.Store(id, event)
    
    type StoreAdapter(c : EventStoreClient) = 
        member x.Store = store c
        member x.RetrieveFor = retrieve c
