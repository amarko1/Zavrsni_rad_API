using ServiceLayer.Dto;
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
        Task AddSupplyAsync(SupplyDto supplyDto);
        Task UpdateSupplyAsync(SupplyDto supplyDto);
        Task DeleteSupplyAsync(int id);
    }

}
