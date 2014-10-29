namespace GrauhundReisen.Funktional

open Events
open Aggregate

module Domain = 
    module Booking = 
        type Booking = Aggregate
        
        let Id history = 
            let getId = 
                function 
                | Event.Ordered order -> Some order.Id
                | _ -> None
            history |> first getId
        
        let EMail history = 
            let getEmail = 
                function 
                | Event.Ordered order -> Some order.Email
                | Event.EmailChanged email -> Some email
                | _ -> None
            history |> latest getEmail
        
        let CreditCardNumber history = 
            let getNumber = 
                function 
                | Event.Ordered order -> Some order.CreditCardNumber
                | Event.CreditCardNumberChanged number -> Some number
                | _ -> None
            history |> latest getNumber
        
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
            let currentEmail = h |> EMail
            //Zusätzliche Logik: Falls die Email nicht verändert wurde, bleibt alles beim alten.  
            if currentEmail = email then booking
            else 
                let event = Events.EmailChanged email
                h <& event, c <& event
        
        let changeCreditCardNumber newNumber booking : Booking = 
            let h, c = booking
            let currentNumber = h |> CreditCardNumber
            //Zusätzliche Logik: Falls die Kreditkartennummer nicht verändert wurde, bleibt alles beim alten.    
            if currentNumber = newNumber then booking
            else 
                let event = Events.CreditCardNumberChanged newNumber
                h <& event, c <& event
