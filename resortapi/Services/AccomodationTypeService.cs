using resortapi.Converters;
using resortapi.Repositories;
using resortdtos;
using resortlibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace resortapi.Services
{
    public class AccomodationTypeService : IAccomodationTypeService
    {
        private readonly AccomodationTypeRepo _repo;
        private readonly AccomodationTypeConverter _converter;

        public AccomodationTypeService(AccomodationTypeRepo repo, AccomodationTypeConverter converter)
        {
            _repo = repo;
            _converter = converter;
        }

        public async Task<ICollection<AccomodationTypeDto>> GetAllAsync()
        {
            var accomodationTypes = await _repo.GetAllAsync();
            return _converter.FromObjectCollection_ToOverviewCollection(accomodationTypes);
        }

        public async Task<AccomodationTypeDto?> GetByIdAsync(int id)
        {
            var accomodationType = await _repo.GetByIdAsync(id);
            if (accomodationType == null)
                return null;

            return _converter.FromObjectToDto(accomodationType);
        }

        public async Task<AccomodationTypeDto> AddAsync(AccomodationTypeDto newAccomodationType)
        {
            var model = _converter.FromDtoToObject(newAccomodationType);
            var added = await _repo.AddAsync(model);
            return _converter.FromObjectToDto(added);
        }

        public async Task<bool> UpdateAsync(int id, AccomodationTypeDto updatedAccomodationType)
        {
            if (id != updatedAccomodationType.Id)
                return false;

            var existingAccoType = await _repo.GetByIdAsync(id);
            if (existingAccoType == null)
                return false;

            existingAccoType.Name = updatedAccomodationType.Name;
            existingAccoType.Description = updatedAccomodationType.Description;
            existingAccoType.BasePrice = updatedAccomodationType.BasePrice;

            await _repo.UpdateAsync(existingAccoType);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingAccoType = await _repo.GetByIdAsync(id);
            if (existingAccoType == null)
                return false;

            await _repo.DeleteAsync(existingAccoType);
            return true;
        }
    }
}
