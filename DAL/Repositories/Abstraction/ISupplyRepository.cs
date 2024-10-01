using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Abstraction
{
    public interface ISupplyRepository
    {
        Task<IEnumerable<Supply>> GetAllSuppliesAsync();
        Task<Supply> GetSupplyByIdAsync(int id);
        Task AddSupplyAsync(Supply supply);
        Task UpdateSupplyAsync(Supply supply);
        Task DeleteSupplyAsync(int id);
        bool CheckIfSupplyNameExists(string name, int? currentId = null);
    }
}
