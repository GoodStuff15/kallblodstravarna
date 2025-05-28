using resortlibrary.Models;

namespace resortlibrary.Builders
{
    public class BookingBuilder
    {
        private Booking _booking;

        public BookingBuilder() 
        {
            _booking = new Booking();
        }
        public BookingBuilder AddCheckIn(DateTime checkIn)
        {
            if (checkIn < DateTime.Now.Date)
            {
                throw new ArgumentException("Incheckning kan inte vara i då-tid.");
            }

            _booking.CheckIn = checkIn;
            return this;
        }
        public BookingBuilder AddCheckOut(DateTime checkOut)
        {
            _booking.CheckOut = checkOut;
            return this;
        }

        public BookingBuilder AddTimeOfBooking(DateTime timeOfBooking)
        {
            _booking.TimeOfBooking = timeOfBooking;
            return this;
        }

        public BookingBuilder AddActive(bool active)
        {
            _booking.Active = active;
            return this;
        }

        public BookingBuilder AddCancelled(bool active)
        {
            _booking.Cancelled = false;
            return this;
        }

        public BookingBuilder AddCost(decimal cost)
        {
            _booking.Cost = cost;
            return this;
        }

        public BookingBuilder AddAmountPaid(decimal amountPaid)
        {
            _booking.AmountPaid = amountPaid;
            return this;
        }

        public BookingBuilder AddCustomerId(int customerId)
        {
            _booking.CustomerId = customerId;
            return this;
        }

        public BookingBuilder AddCustomer(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentException("Kund måste anges.");
            }

            _booking.Customer = customer;
            return this;
        }
        
        public BookingBuilder AddAccomodationId(int accomodationId)
        {
            _booking.AccomodationId = accomodationId;
            return this;
        }

        public BookingBuilder AddAccomodation(Accomodation accomodation)
        {
            if (accomodation == null)
            {
                throw new ArgumentException("Boende måste anges.");
            }

            _booking.Accomodation = accomodation;
            return this;
        }

        public BookingBuilder AddGuestList(ICollection<Guest> guests)
        {
            _booking.Guests = guests;
            return this;
        }

        public BookingBuilder AddPriceChanges(ICollection<PriceChanges> priceChanges)
        {
            _booking.PriceChanges = priceChanges;
            return this;
        }

        public BookingBuilder AdditionalOptions(ICollection<AdditionalOption> additionalOptions)
        {
            _booking.AdditionalOptions = additionalOptions;
            return this;
        }

        public Booking Build()
        {
            if (_booking.CheckOut <= _booking.CheckIn)
            {
                throw new ArgumentException("Utcheckning måste vara efter incheckning.");
            }

            return _booking;
        }
    }
}

