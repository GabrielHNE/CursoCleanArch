using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interface;

namespace CleanArchMvc.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task Add(CategoryDTO category)
        {
            var catEntity = _mapper.Map<Category>(category);
            await _categoryRepository.CreateAsync(catEntity);
        }

        public async Task<CategoryDTO> GetById(int? id)
        {
            if(!id.HasValue)
                return null;
            
            var catEntity = await _categoryRepository.GetByIdAsync(id);
            return _mapper.Map<CategoryDTO>(catEntity);
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            var catEntity = await _categoryRepository.GetCategoriesAsync();

            return _mapper.Map<IEnumerable<CategoryDTO>>(catEntity);
        }

        public async Task Remove(int? id)
        {
            var categoryEntity = _categoryRepository.GetByIdAsync(id).Result;
            await _categoryRepository.RemoveAsync(categoryEntity);
        }

        public async Task Update(CategoryDTO category)
        {
            var catEntity = _mapper.Map<Category>(category);
            await _categoryRepository.UpdateAsync(catEntity);
        }
    }
}