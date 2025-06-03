using resortdtos;

namespace resortapi.Services
{
    public interface IAccessibilityService
    {
        Task<ICollection<AccessibilityDto>> GetAllAccessibilitiesAsync();
        Task<AccessibilityDto?> GetAccessibilityByIdAsync(int id);
        Task<AccessibilityDto?> AddNewAccessibilityAsync(AccessibilityDto dto);
        Task<bool> UpdateAccessibilityAsync(int id, AccessibilityDto dto);
        Task<bool> DeleteAccessibilityAsync(int id);
    }
}
