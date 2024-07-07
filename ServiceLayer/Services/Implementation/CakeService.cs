using AutoMapper;
using DAL.Models;
using DAL.Repositories.Abstraction;
using ServiceLayer.Dto;
using ServiceLayer.Services.Abstraction;

public class CakeService : ICakeService
{
    private readonly ICakeRepository _cakeRepository;
    private readonly IMapper _mapper;

    public CakeService(ICakeRepository cakeRepository, IMapper mapper)
    {
        _cakeRepository = cakeRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CakeDto>> GetAllCakesAsync()
    {
        var cakes = await _cakeRepository.GetAllCakesAsync();
        return _mapper.Map<IEnumerable<CakeDto>>(cakes);
    }

    public async Task<CakeDto?> GetCakeAsync(int id)
    {
        var cake = await _cakeRepository.GetCakeAsync(id);
        return _mapper.Map<CakeDto>(cake);
    }

    public async Task<CakeDto> CreateCakeAsync(CakeDto newCakeDto)
    {
        var newCake = _mapper.Map<Cake>(newCakeDto);

        if (!string.IsNullOrEmpty(newCakeDto.ImageContent))
        {
            newCake.ImageContent = newCakeDto.ImageContent;
        }

        await _cakeRepository.CreateCakeAsync(newCake);
        return _mapper.Map<CakeDto>(newCake);
    }

    public async Task UpdateCakeAsync(CakeDto updatedCakeDto)
    {
        var updatedCake = _mapper.Map<Cake>(updatedCakeDto);

        if (!string.IsNullOrEmpty(updatedCakeDto.ImageContent))
        {
            updatedCake.ImageContent = updatedCakeDto.ImageContent;
        }

        await _cakeRepository.UpdateCakeAsync(updatedCake);
    }

    public async Task DeleteCakeAsync(int id)
    {
        await _cakeRepository.DeleteCakeAsync(id);
    }

    public async Task<string?> GetImageContentAsync(int id)
    {
        var cake = await _cakeRepository.GetCakeAsync(id);
        return cake?.ImageContent;
    }
}
