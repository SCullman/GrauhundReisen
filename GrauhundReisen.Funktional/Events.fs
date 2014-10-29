namespace GrauhundReisen.Funktional

module Events = 
    type BookingId = string
    
    type Email = string
    
    type CreditCardNumber = string
    
    type Order = 
        { Id : BookingId
          FirstName : string
          LastName : string
          CreditCardType : string
          CreditCardNumber : CreditCardNumber
          Destination : string
          Email : Email }
    
    type Event = 
        | Ordered of Order
        | EmailChanged of Email
        | CreditCardNumberChanged of CreditCardNumber

    let DeserializeEvent s = Newtonsoft.Json.JsonConvert.DeserializeObject<Event>(s)
