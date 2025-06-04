using resortlibrary.Models;

namespace resortapi.Repositories
{
    public interface IAccomodationRepo : IRepository<Accomodation>
    {
        public Task<ICollection<Accomodation>> GetAvailableByGuestNo(DateTime start, DateTime end, int guestNo);
    }
}
