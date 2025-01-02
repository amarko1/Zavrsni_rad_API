using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Abstraction
{
    public interface ICakeRepository
    {
        Task CreateCakeAsync(Cake newCake);
        Task<Cake?> GetCakeAsync(int id);
        Task<IEnumerable<Cake>> GetAllCakesAsync();
        Task UpdateCakeAsync(Cake updatedCake);
        Task DeleteCakeAsync(int id);
        Task SaveAsync();
        bool CheckIfCakeNameExists(string name, int? currentId = null);
        Task<IEnumerable<Cake>> SearchCakesAsync(string query);
        Task<List<Cake>> GetFilteredCakesAsync(CakeFilterParams filterParams);
    }
}
