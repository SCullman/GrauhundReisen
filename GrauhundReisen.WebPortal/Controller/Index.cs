using System;
using Nancy;
using GrauhundReisen.Funktional;
using System.Threading.Tasks;

namespace GrauhundReisen.WebPortal
{
	public class Index : NancyModule
	{
		readonly CommandHandler.Booking _bookingService;

        public Index(ReadModel.BookingForm bookingForm, CommandHandler.Booking bookingService)
		{
			_bookingService = bookingService;
			Get [""] = _ => View ["index", new { bookingForm.CreditCardTypes, bookingForm.Destinations }];
			Post ["", runAsync: true] = async(parameters, cancel) => await ProceedBooking();
		}

        async Task <Object> ProceedBooking()
        {
            var id = Guid.NewGuid().ToString();
            var cmd = new Commands.OrderBooking(
                id,
                this.Request.Form["TravellerFirstName"].Value,
                this.Request.Form["TravellerLastName"].Value,
                this.Request.Form["PaymentCreditCardType"].Value,
                this.Request.Form["PaymentCreditCardNumber"].Value,
                this.Request.Form["Destination"].Value,
                this.Request.Form["TravellerEmail"].Value
            );
            await Task.Run (() =>_bookingService.Handle(cmd));
            return View["confirmation", new { BookingId = id }];
        }
	}
}