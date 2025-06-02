using resortlibrary.Models;

namespace resortapi.Repositories
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<Booking?> GetByIdWithIncludesAsync(int id);
    }

}
