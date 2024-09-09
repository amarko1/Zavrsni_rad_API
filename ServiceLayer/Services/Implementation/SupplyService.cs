using AutoMapper;
using DAL.Models;
using DAL.Repositories.Abstraction;
using DAL.Repositories.Implementation;
using ServiceLayer.Dto;
using ServiceLayer.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Implementation
{
    public class SupplyService : ISupplyService
    {
        private readonly ISupplyRepository _supplyRepository; // Pretpostavljam da si ovo već implementirao
        private readonly IMapper _mapper;

        public SupplyService(ISupplyRepository supplyRepository, IMapper mapper)
        {
            _supplyRepository = supplyRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SupplyDto>> GetAllSuppliesAsync()
        {
            var supplies = await _supplyRepository.GetAllSuppliesAsync();
            return _mapper.Map<IEnumerable<SupplyDto>>(supplies);
        }

        public async Task<SupplyDto> GetSupplyByIdAsync(int id)
        {
            var supply = await _supplyRepository.GetSupplyByIdAsync(id);
            return _mapper.Map<SupplyDto>(supply);
        }

        public async Task AddSupplyAsync(SupplyDto supplyDto)
        {
            var supply = _mapper.Map<Supply>(supplyDto);
            await _supplyRepository.AddSupplyAsync(supply);
        }

        public async Task UpdateSupplyAsync(SupplyDto supplyDto)
        {
            var supply = _mapper.Map<Supply>(supplyDto);
            await _supplyRepository.UpdateSupplyAsync(supply);
        }

        public async Task DeleteSupplyAsync(int id)
        {
            await _supplyRepository.DeleteSupplyAsync(id);
        }
    }

}
