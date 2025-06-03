using resortapi.Converters;
using resortapi.Repositories;
using resortdtos;

namespace resortapi.Services
{
    public class PriceChangesService : IPriceChangesService
    {
        private readonly PriceChangesRepo _repo;
        private readonly PriceChangesConverter _converter;

        public PriceChangesService(PriceChangesRepo repo, PriceChangesConverter converter)
        {
            _repo = repo;
            _converter = converter;
        }

        public async Task<ICollection<PriceChangesDto>> GetAllAsync()
        {
            var priceChanges = await _repo.GetAllAsync();
            return _converter.FromObjecttoDTO_Collection(priceChanges);
        }

        public async Task<PriceChangesDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity != null ? _converter.FromObjecttoDTO(entity) : null;
        }

        public async Task<PriceChangesDto?> AddAsync(PriceChangesDto dto)
        {
            var entity = _converter.FromDTOtoObject(dto);
            if (entity == null)
            {
                return null;
            }

            var added = await _repo.AddAsync(entity);
            return _converter.FromObjecttoDTO(added);
        }

        public async Task<bool> UpdateAsync(int id, PriceChangesDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
            {
                return false;
            }

            existing.PriceChange = dto.PriceChange;
            existing.Type = dto.Type;

            await _repo.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
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
