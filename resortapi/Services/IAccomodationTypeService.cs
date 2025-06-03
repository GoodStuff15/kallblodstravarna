using resortdtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace resortapi.Services
{
    public interface IAccomodationTypeService
    {
        Task<ICollection<AccomodationTypeDto>> GetAllAsync();
        Task<AccomodationTypeDto?> GetByIdAsync(int id);
        Task<AccomodationTypeDto> AddAsync(AccomodationTypeDto newAccomodationType);
        Task<bool> UpdateAsync(int id, AccomodationTypeDto updatedAccomodationType);
        Task<bool> DeleteAsync(int id);
    }
}
