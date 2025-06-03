using resortapi.Converters;
using resortapi.Repositories;
using resortdtos;
using resortlibrary.Models;

namespace resortapi.Services
{
    public class AdditionalOptionService : IAdditionalOptionService
    {
        private readonly IRepository<AdditionalOption> _repo;
        private readonly AdditionalOptionConverter _converter;

        public AdditionalOptionService(IRepository<AdditionalOption> repo, AdditionalOptionConverter converter)
        {
            _repo = repo;
            _converter = converter;
        }

        public async Task<ICollection<AdditionalOptionDto>> GetAllAsync()
        {
            var options = await _repo.GetAllWithIncludesAsync();
            return _converter.FromModelCollectionToDto(options);
        }

        public async Task<AdditionalOptionDto?> GetByIdAsync(int id)
        {
            var option = await _repo.GetAsync(id);
            return option == null ? null : _converter.FromModelToDto(option);
        }

        public async Task<AdditionalOptionDto> AddAsync(AdditionalOptionDto dto)
        {
            var model = _converter.FromDtoToModel(dto);
            await _repo.CreateAsync(model);
            return _converter.FromModelToDto(model);
        }


        public async Task<bool> UpdateAsync(int id, AdditionalOptionDto dto)
        {
            var existing = await _repo.GetAsync(id);
            if (existing == null)
                return false;

            existing.Name = dto.Name;
            existing.Description = dto.Description;
            existing.Price = dto.Price;
            existing.PerGuest = dto.PerGuest;
            existing.PerNight = dto.PerNight;

            await _repo.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repo.GetAsync(id);
            if (existing == null)
                return false;

            await _repo.DeleteAsync(existing);
            return true;
        }
    }
}
