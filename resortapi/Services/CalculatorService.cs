using resortdtos;
using resortlibrary.Models;

namespace resortapi.Services
{
    public class CalculatorService
    {
        public CalculatorService() { }

        public decimal CalculatePrice_Booking(Booking booking)
        {
            decimal totalPrice = 0;

            int numberOfGuests = booking.Guests.Count;
            int numberOfNights = (booking.CheckOut - booking.CheckIn).Days;

            if (numberOfNights <= 0)
                throw new ArgumentException("CheckOut måste vara senare än CheckIn");

            // Boendets grundpris per natt
            totalPrice += booking.Accomodation.AccomodationType.BasePrice * numberOfNights;

            // Tillval
            foreach (var option in booking.AdditionalOptions)
            {
                // Om tillvalet är per gäst och per natt (som frukost)
                if (option.PerGuest && option.PerNight)
                {
                    totalPrice += option.Price * numberOfGuests * numberOfNights;
                }
                // Om tillvalet endast är per gäst
                else if (option.PerGuest)
                {
                    totalPrice += option.Price * numberOfGuests;
                }
                // Om tillvalet endast är per natt
                else if (option.PerNight)
                {
                    totalPrice += option.Price * numberOfNights;
                }
                // Om tillvalet endast är per bokning
                else
                {
                    totalPrice += option.Price;
                }
            }

            return totalPrice;
        }

    }
}
