using resortlibrary.Models;

namespace resortapi.Repositories
{
    public interface IAccomodationRepo : IRepository<Accomodation>
    {
        public Task<ICollection<Accomodation>> GetAvailableByGuestNo(DateTime start, DateTime end, int guestNo);
        public Task<ICollection<Accomodation>> GetAvailableAsync(DateTime start, DateTime end);
        //public Task<ICollection<Accomodation>> GetAllAsync();
        public Task<Accomodation?> GetByIdAsync(int id);
        //public Task<ICollection<Accomodation>> GetAllWithIncludesAsync();
        public Task<Accomodation?> AddAsync(Accomodation accomodation);
        public Task<Accomodation?> UpdateAsync(Accomodation accomodation);
        //public Task DeleteAsync(Accomodation accomodation);


    }
}
