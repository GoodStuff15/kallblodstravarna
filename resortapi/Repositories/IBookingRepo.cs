using resortlibrary.Models;

namespace resortapi.Repositories
{
    public interface IBookingRepository : IRepository<Booking>
    {
        public Task<ICollection<Booking>> GetByCustomerIdWithIncludesAsync(int id);

        Task<ICollection<Booking>> GetAllWithCustomerAsync();

        Task<ICollection<Booking>> GetByCustomerIdAndEmailAsync(int customerId, string email);

    }

}
