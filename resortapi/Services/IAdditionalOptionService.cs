using resortdtos;

namespace resortapi.Services
{
    public interface IAdditionalOptionService
    {
        Task<ICollection<AdditionalOptionDto>> GetAllAsync();
        Task<AdditionalOptionDto?> GetByIdAsync(int id);
        Task<AdditionalOptionDto> AddAsync(AdditionalOptionDto dto);
        Task<bool> UpdateAsync(int id, AdditionalOptionDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
