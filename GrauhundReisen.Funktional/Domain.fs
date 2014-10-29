namespace GrauhundReisen.Funktional

open Events
open Aggregate

module Domain = 
    module Booking = 
        type Booking = Aggregate
        
        //    let getId (history) = 
        //        let isOrdered = 
        //            function 
        //            | Event.Ordered _ -> true
        //            | _ -> false
        //        
        //        let id = 
        //            history
        //            |> List.find (isOrdered)
        //            |> function 
        //                | Event.Ordered order -> Some order.Id
        //                | _ -> None
        //        
        //        id.Value
        let getCreditCardNumber history = 
            let modifiesCC = 
                function 
                | Event.Ordered _ 
                | Event.CreditCardNumberChanged _ -> true
                | _ -> false
            
            let number = 
                history
                |> List.rev
                |> List.find modifiesCC
                |> function 
                    | Event.Ordered order -> Some order.CreditCardNumber
                    | Event.CreditCardNumberChanged number -> Some number
                    | _ -> None
            
            number.Value
        
        let fromHistory events : Booking = events, []
        let getChanges = getChanges
        
        let order id firstname lastname email cctype ccnumber destination : Booking = 
            let order = 
                { Id = id
                  FirstName = firstname
                  LastName = lastname
                  Email = email
                  CreditCardNumber = ccnumber
                  CreditCardType = cctype
                  Destination = destination }
            
            let event = Ordered order
            [ event ], [ event ]
        
        let changeEMail email booking : Booking = 
            let h, c = booking
            let event = Events.EmailChanged email
            h <& event, c <& event
        
        let changeCreditCardNumber newNumber booking : Booking = 
            let h, c = booking
            let oldNumber = h |> getCreditCardNumber
            //Zusätzliche Logik: Falls die Kreditkartennummer nicht verändert wurde, bleibt alles beim alten.    
            if oldNumber = newNumber then booking
            else 
                let event = Events.CreditCardNumberChanged newNumber
                h <& event, c <& event
