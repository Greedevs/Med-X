using MedX.Domain.Configurations;
using MedX.Service.DTOs.ServiceItems;
using MedX.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedX.Service.Services;

public class AffairService : IAffairItemService
{
    public Task<ServiceItemResultDto> AddAsync(ServiceItemCreationDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ServiceItemResultDto>> GetAllAsync(PaginationParams @params, string search = null)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceItemResultDto> GetAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceItemResultDto> UpdateAsync(ServiceItemUpdateDto dto)
    {
        throw new NotImplementedException();
    }
}
