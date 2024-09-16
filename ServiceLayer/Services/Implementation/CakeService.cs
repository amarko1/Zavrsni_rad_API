﻿using AutoMapper;
using DAL.Models;
using DAL.Repositories.Abstraction;
using ServiceLayer.Dto;
using ServiceLayer.ImageUtils;
using ServiceLayer.ServiceModels;
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

    public async Task CreateCakeAsync(CakeUpdateRequest newCakeDto)
    {
        var newCake = _mapper.Map<Cake>(newCakeDto);

        var imageAsStream = ImageUtils.GetFileAsMemoryStream(newCakeDto.ImageContent);
        var imageContent = Convert.ToBase64String(imageAsStream!);

        await _cakeRepository.CreateCakeAsync(new Cake()
        {
            Allergens = newCake.Allergens,
            ImageContent = imageContent,
            CategoryId = newCake.CategoryId,
            Name = newCake.Name,
            Price = newCake.Price,
            Size = newCake.Size,
            CustomMessage = newCake.CustomMessage,
            Description = newCake.Description
        });
    }

    public async Task UpdateCakeAsync(CakeUpdateRequest updatedCakeDto)
    {
        var updatedCake = _mapper.Map<Cake>(updatedCakeDto);

        var imageAsStream = ImageUtils.GetFileAsMemoryStream(updatedCakeDto.ImageContent);
        var imageContent = Convert.ToBase64String(imageAsStream!);

        await _cakeRepository.UpdateCakeAsync(new Cake()
        {
            Id = updatedCake.Id,
            Allergens = updatedCake.Allergens,
            ImageContent = imageContent,
            CategoryId = updatedCake.CategoryId,
            Name = updatedCake.Name,
            Price = updatedCake.Price,
            Size = updatedCake.Size,
            CustomMessage = updatedCake.CustomMessage,
            Description = updatedCake.Description
        });
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
