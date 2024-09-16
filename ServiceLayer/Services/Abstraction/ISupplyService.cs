using ServiceLayer.Dto;
using ServiceLayer.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Abstraction
{
    public interface ISupplyService
    {
        Task<IEnumerable<SupplyDto>> GetAllSuppliesAsync();
        Task<SupplyDto> GetSupplyByIdAsync(int id);
        Task AddSupplyAsync(SupplyCreateRequest supplyDto);
        Task UpdateSupplyAsync(SupplyCreateRequest supplyDto);
        Task DeleteSupplyAsync(int id);
    }

}
