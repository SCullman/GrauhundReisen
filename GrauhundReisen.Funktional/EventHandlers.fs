namespace GrauhundReisen.Funktional

open Events
open ReadModel.models
open System.IO
open Newtonsoft.Json

module EventHandlers = 
    let saveBookingAsFile path (b : Booking) = 
        let savePath = Path.Combine(path, b.Id)
        let bookingAsString = JsonConvert.SerializeObject(b)
        File.WriteAllText(savePath, bookingAsString)
    
    let readBookingFromFile path id = 
        let path = Path.Combine(path, id)
        let bookingAsString = File.ReadAllText path
        JsonConvert.DeserializeObject<Booking> bookingAsString
    
    let deleteBooking path id = 
        let path = Path.Combine(path, id)
        File.Delete path
    
    let mapEventToModel (order : Order) = 
        { Id = order.Id
          Destination = order.Destination
          FirstName = order.FirstName
          LastName = order.LastName
          EMail = order.Email
          CreditCardNumber = order.CreditCardNumber
          CreditCardType = order.CreditCardType }
    
    type Booking(path) = 
        let save = saveBookingAsFile path
        let read = readBookingFromFile path
        let store order = mapEventToModel order |> save
        let updateNumber id n = { read id with CreditCardNumber = n } |> save
        let updateEmail id e = { read id with EMail = e } |> save
        member this.Handle id (event : Event) = 
            match event with
            | Ordered order -> store order
            | CreditCardNumberChanged n -> updateNumber id n
            | EmailChanged e -> updateEmail id e
