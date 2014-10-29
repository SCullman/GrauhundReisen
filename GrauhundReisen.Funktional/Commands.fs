namespace GrauhundReisen.Funktional

module Commands = 
    type OrderBooking = 
        { Id : string
          FirstName : string
          LastName : string
          CreditCardType : string
          CreditCardNumber : string
          Destination : string
          Email : string }
    
    type ChangeBooking = 
        { Id : string
          CreditCardNumber : string
          Email : string }
    
    type Command = 
        | ChangeBooking
        | OrderBooking
