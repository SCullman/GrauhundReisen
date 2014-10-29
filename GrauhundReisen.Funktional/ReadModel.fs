namespace GrauhundReisen.Funktional

open System.IO
open Newtonsoft.Json

module ReadModel = 
    [<AutoOpen>]
    module models = 
        type Booking = 
            { Id : string
              FirstName : string
              LastName : string
              CreditCardType : string
              CreditCardNumber : string
              Destination : string
              EMail : string }
        
        type Destination = 
            { Name : string }
        
        type CreditcardType = 
            { Name : string }
    
    module Repositories = 
        module bookings = 
            let readBookingFromFile bookingPath id = 
                let path = Path.Combine(bookingPath, id)
                let bookingAsString = File.ReadAllText(path)
                let booking = JsonConvert.DeserializeObject<Booking>(bookingAsString)
                booking
        
        module destinations = 
            let getAll = 
                [ { Name = "Leipzig" }
                  { Name = "Berlin" }
                  { Name = "Hamburg" }
                  { Name = "Frankfurt" }
                  { Name = "Köln" }
                  { Name = "München" } ]
                |> List.sortBy (fun d -> d.Name)
        
        module creditCardTypes = 
            let getAll = 
                [ { Name = "Visa" }
                  { Name = "MasterCard" }
                  { Name = "AmericanExpress" } ]
                |> List.sortBy (fun d -> d.Name)
        
    type BookingForm() = 
        member this.CreditCardTypes = Repositories.creditCardTypes.getAll
        member this.Destinations = Repositories.destinations.getAll
        
    type Bookings(connectionstring) = 
        member this.GetBookingBy id = Repositories.bookings.readBookingFromFile connectionstring id
