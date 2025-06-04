using resortdtos;
using resortlibrary.Models;

namespace resortapi.Services
{
    public interface ICalculatorService
    {
        public Task<decimal> CalculateCurrentPrice(PriceRequestDto dto);

        public decimal CalculateOptionPrices(ICollection<AdditionalOption> options, int guestNo, int nightNo);

        public Task<decimal> GetOptionPrices(ICollection<int> options, int guestNo, int nightNo);

        public Task<decimal> GetAccomodationPrice(int accomodationId);

        public PriceRequestDto PriceRequestValidation(PriceRequestDto priceRequest);
    }
}
