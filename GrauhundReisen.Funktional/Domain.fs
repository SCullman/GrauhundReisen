namespace GrauhundReisen.Funktional

open Events
open Aggregate

module Domain = 
    module Booking = 
        type Booking = Aggregate
        
        let Id aggregate = 
            let getId = 
                function 
                | Event.Ordered order -> Some order.Id
                | _ -> None
            aggregate |> first getId
        
        let EMail aggregate = 
            let getEmail = 
                function 
                | Event.Ordered order -> Some order.Email
                | Event.EmailChanged email -> Some email
                | _ -> None
            aggregate |> latest getEmail
        
        let CreditCardNumber aggregate = 
            let getNumber = 
                function 
                | Event.Ordered order -> Some order.CreditCardNumber
                | Event.CreditCardNumberChanged number -> Some number
                | _ -> None
            aggregate |> latest getNumber
        
        let order id firstname lastname email cctype ccnumber destination : Booking = 
            let order = 
                { Id = id
                  FirstName = firstname
                  LastName = lastname
                  Email = email
                  CreditCardNumber = ccnumber
                  CreditCardType = cctype
                  Destination = destination }
            startWith (Event.Ordered order) 
        
        let changeEMail email aggregate : Booking = 
            let currentEmail = aggregate |> EMail
            //Zusätzliche Logik: Falls die Email nicht verändert wurde, bleibt alles beim alten.  
            if currentEmail = email then aggregate
            else 
                let event = Event.EmailChanged email
                aggregate |> append event
        
        let changeCreditCardNumber newNumber aggregate : Booking = 
            let currentNumber = aggregate |> CreditCardNumber
            //Zusätzliche Logik: Falls die Kreditkartennummer nicht verändert wurde, bleibt alles beim alten.    
            if currentNumber = newNumber then aggregate
            else 
                let event = Event.CreditCardNumberChanged newNumber
                aggregate |> append event
