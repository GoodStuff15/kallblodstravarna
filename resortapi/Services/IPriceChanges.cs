using resortdtos;

namespace resortapi.Services
{
    public interface IPriceChangesService
    {
        Task<ICollection<PriceChangesDto>> GetAllAsync();
        Task<PriceChangesDto?> GetByIdAsync(int id);
        Task<PriceChangesDto?> AddAsync(PriceChangesDto dto);
        Task<bool> UpdateAsync(int id, PriceChangesDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
