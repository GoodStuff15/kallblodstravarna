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

            foreach(var option in booking.AdditionalOptions)
            {
                totalPrice += option.Price;
            }

            totalPrice += booking.Accomodation.AccomodationType.BasePrice;

            return totalPrice;
        }




    }
}
