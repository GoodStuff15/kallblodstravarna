using resortapi.Repositories;
using resortdtos;
using resortlibrary.Models;

namespace resortapi.Services
{
    public class CalculatorService : ICalculatorService
    {
        private readonly IAccomodationRepo _accomodationRepo;
        private readonly IRepository<AccomodationType> _typeRepo;
        private readonly IRepository<AdditionalOption> _additionalOptionsRepo;
        
        public CalculatorService(IAccomodationRepo accomodationRepo, IRepository<AccomodationType> typeRepo, IRepository<AdditionalOption> additionalOptionsRepo)
        {
            _accomodationRepo = accomodationRepo;
            _typeRepo = typeRepo;
            _additionalOptionsRepo = additionalOptionsRepo;
        }
        public PriceRequestDto PriceRequestValidation(PriceRequestDto priceRequest)
        {
            //if (numberOfNights <= 0)
            //{
            //    throw new ArgumentException("CheckOut måste vara senare än CheckIn");
            //}
            throw new NotImplementedException("Price request validation not implemented");
        }

        public async Task<decimal> GetAccomodationPrice(int accomodationId)
        {
            var accomodation = await _accomodationRepo.GetAsync(accomodationId);

            var accomodationType = await _typeRepo.GetAsync(accomodation.AccomodationTypeId);

            return accomodationType.BasePrice;
        }

        public async Task<decimal> GetOptionPrices(ICollection<int> options, int guestNo, int nightNo)
        {
            var additionalOptions = new List<AdditionalOption>();

            foreach (var option in options)
            {
                additionalOptions.Add(await _additionalOptionsRepo.GetAsync(option));
            }

            return CalculateOptionPrices(additionalOptions, guestNo, nightNo);
     
        }

        public decimal CalculateOptionPrices(ICollection<AdditionalOption> options, int guestNo, int nightNo)
        {
            decimal optionPrices = 0;
            foreach (var option in options)
            {
                // If option is per guest and per night
                if (option.PerGuest && option.PerNight)
                {
                    optionPrices += option.Price * guestNo * nightNo;
                }
                // If option is per guest
                else if (option.PerGuest)
                {
                    optionPrices += option.Price * guestNo;
                }
                // If option is per night
                else if (option.PerNight)
                {
                    optionPrices += option.Price * nightNo;
                }
                // If option is per booking
                else
                {
                    optionPrices += option.Price;
                }
            }

            return optionPrices;

        }

        public async Task<decimal> CalculateCurrentPrice(PriceRequestDto dto)
        {
            decimal totalPrice = 0;

            int numberOfGuests = dto.GuestCount;
            int numberOfNights = dto.Duration;

            // Accomodation base price per night added to total price
            totalPrice += await GetAccomodationPrice(dto.AccomodationId) * dto.GuestCount;

            // Adding additional options prices
            totalPrice += await GetOptionPrices(dto.AdditonalOptionIds, dto.GuestCount, dto.Duration);

            return totalPrice;
        }

    }
}
