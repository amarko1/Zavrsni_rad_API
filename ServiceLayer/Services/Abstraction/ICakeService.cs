using ServiceLayer.Dto;
using ServiceLayer.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Abstraction
{
    public interface ICakeService
    {
        Task<IEnumerable<CakeDto>> GetAllCakesAsync();
        Task<CakeDto?> GetCakeAsync(int id);
        Task CreateCakeAsync(CakeUpdateRequest newCake);
        Task UpdateCakeAsync(CakeUpdateRequest updatedCake);
        Task DeleteCakeAsync(int id);
        Task<string?> GetImageContentAsync(int id);
    }

}
