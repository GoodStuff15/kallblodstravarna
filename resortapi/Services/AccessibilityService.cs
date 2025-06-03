using resortapi.Converters;
using resortapi.Repositories;
using resortdtos;

namespace resortapi.Services
{
    public class AccessibilityService : IAccessibilityService
    {
        private readonly AccessibilityRepo _repo;
        private readonly AccessibilityConverter _converter;

        public AccessibilityService(AccessibilityRepo accessibilityRepo, AccessibilityConverter accessibilityConverter)
        {
            _repo = accessibilityRepo;
            _converter = accessibilityConverter;
        }

        public async Task<ICollection<AccessibilityDto>> GetAllAccessibilitiesAsync()
        {
            var accessibilities = await _repo.GetAllAsync();
            return _converter.FromObjecttoDTO_Collection(accessibilities);
        }

        public async Task<AccessibilityDto?> GetAccessibilityByIdAsync(int id)
        {
            var accessibility = await _repo.GetByIdAsync(id);
            return accessibility != null ? _converter.FromObjecttoDTO(accessibility) : null;
        }

        public async Task<AccessibilityDto?> AddNewAccessibilityAsync(AccessibilityDto dto)
        {
            var accessibility = _converter.FromDTOtoObject(dto);
            if (accessibility == null)
            {
                return null;
            }

            var added = await _repo.AddAsync(accessibility);
            return _converter.FromObjecttoDTO(added);
        }

        public async Task<bool> UpdateAccessibilityAsync(int id, AccessibilityDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
            {
                return false;
            }

            existing.Name = dto.Name;
            existing.Description = dto.Description;

            await _repo.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAccessibilityAsync(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
            {
                return false;
            }

            await _repo.DeleteAsync(existing);
            return true;
        }
    }
}
