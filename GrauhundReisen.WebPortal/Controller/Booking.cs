using System.Web.UI.WebControls;

using Nancy;
using GrauhundReisen.Funktional;
using System.Threading.Tasks;

namespace GrauhundReisen.WebPortal
{
    public class Booking : NancyModule
    {
        readonly ReadModel.Bookings _bookings;
        readonly CommandHandler.Booking _bookingService;

        public Booking(ReadModel.Bookings bookings, CommandHandler.Booking bookingService)
        {
            _bookings = bookings;
            _bookingService = bookingService;

            Get["change-booking/{id}"] = _ => GetBookingFormFor((string)_.id);
            Post["change-booking", true] = async (parameters, cancel) => await UpdateBooking();
        }

        object GetBookingFormFor(string bookingId)
        {
            var booking = _bookings.GetBookingBy(bookingId);

            return View["change-booking", booking];
        }

        async Task<object> UpdateBooking()
        {
            var cmd = new Commands.ChangeBooking(
                this.Request.Form["BookingId"].Value,
                this.Request.Form["PaymentCreditCardNumber"].Value,
                this.Request.Form["TravellerEMail"].Value
                );

            await Task.Run(()=>_bookingService.Handle(cmd));

            return View["change-confirmation"];
        }
    }
}
