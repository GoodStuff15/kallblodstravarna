using resortlibrary.Models;

namespace resortlibrary.Factories.IFactories
{
    public interface IBookingBuilder
    {
        public BookingBuilder AddCheckIn(DateTime checkIn);
        public BookingBuilder AddCheckOut(DateTime checkOut);
        public BookingBuilder AddTimeOfBooking(DateTime timeOfBooking);
        public BookingBuilder AddActive(bool active);
        public BookingBuilder AddCost(decimal cost);
        public BookingBuilder AddAmountPaid(decimal amountPaid);
        public BookingBuilder AddCustomerId(int customerId);
        public BookingBuilder AddCustomer(Customer customer);
        public BookingBuilder AddAccomodationId(int accomodationId);
        public BookingBuilder AddAccomodation(Accomodation accomodation);
        public BookingBuilder AddGuestList(ICollection<Guest> guests);
        public BookingBuilder AddPriceChanges(ICollection<PriceChanges> priceChanges);
        public BookingBuilder AdditionalOptions(ICollection<AdditionalOption> additionalOptions);
        public Booking Build();
    }
}
