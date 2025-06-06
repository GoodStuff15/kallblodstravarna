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
        public BookingBuilder AddCheckIn(DateTime checkIn)//Set checkIn. Cant be in the past.
        {
            if (checkIn < DateTime.Now.Date)
            {
                throw new ArgumentException("CheckIn cant be in the past.");
            }

            _booking.CheckIn = checkIn;
            return this;
        }
        public BookingBuilder AddCheckOut(DateTime checkOut)//Set checkOut. Must be after checkIn.
        {
            if(_booking.CheckIn == default)
            {
                throw new InvalidOperationException("CheckIn must be entered befor checkOut.");
            }
            if(checkOut <= _booking.CheckIn)
            {
                throw new ArgumentException("CheckOut must be after checkIn.");
            }
            _booking.CheckOut = checkOut;
            return this;
        }

        public BookingBuilder AddTimeOfBooking(DateTime timeOfBooking)//Set time of booking
        {
            _booking.TimeOfBooking = timeOfBooking;
            return this;
        }

        public BookingBuilder AddActive(bool active)//Set if booking is active or not
        {
            _booking.Active = active;
            return this;
        }

        public BookingBuilder AddCancelled(bool active)//Set wheter the booking is cancelled
        {
            _booking.Cancelled = false;
            return this;
        }

        public BookingBuilder AddCost(decimal cost)//Set cost of booking
        {
            _booking.Cost = cost;
            return this;
        }

        public BookingBuilder AddAmountPaid(decimal amountPaid)//Set amount paid by customer
        {
            _booking.AmountPaid = amountPaid;
            return this;
        }

        public BookingBuilder AddCustomerId(int customerId)//Set customer id
        {
            _booking.CustomerId = customerId;
            return this;
        }

        public BookingBuilder AddCustomer(Customer customer)//Set customer object. Cant be null
        {
            if (customer == null)
            {
                throw new ArgumentException("Customer must be entered.");
            }

            _booking.Customer = customer;
            return this;
        }
        
        public BookingBuilder AddAccomodationId(int accomodationId)//Set accomodation id
        {
            _booking.AccomodationId = accomodationId;
            return this;
        }

        public BookingBuilder AddAccomodation(Accomodation accomodation)//Set accomodation object. Cant be null
        {
            if (accomodation == null)
            {
                throw new ArgumentException("Accomodation must be entered.");
            }

            _booking.Accomodation = accomodation;
            return this;
        }

        public BookingBuilder AddGuestList(ICollection<Guest> guests)//Set list of guests. Must contain at least one guest
        {
            if(guests == null || !guests.Any())
            {
                throw new ArgumentException("Min one guest must be entered.");
            }
            _booking.Guests = guests;
            return this;
        }

        public BookingBuilder AddPriceChanges(ICollection<PriceChanges> priceChanges)//Set list of price changes
        {
            _booking.PriceChanges = priceChanges;
            return this;
        }

        public BookingBuilder AdditionalOptions(ICollection<AdditionalOption> additionalOptions) //Set list of additional options
        {
            _booking.AdditionalOptions = additionalOptions;
            return this;
        }

        public Booking Build()//Build booking-object
        {
            return _booking;
        }
    }
}

