using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialAccounting.Application.Commands;
using FinancialAccounting.Application.DTOs;
using FinancialAccounting.Domain.Entities;
using FinancialAccounting.Domain.Factories;
using FinancialAccounting.Domain.Repositories;

namespace FinancialAccounting.Application.Services
{
    /// <summary>
    /// Сервис для управления категориями.
    /// </summary>
    public class CategoryManagementService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryManagementService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryDTO> CreateCategoryAsync(CreateCategoryCommand command)
        {
            var category = CategoryFactory.Create(command.Name);
            await _categoryRepository.AddAsync(category);
            return new CategoryDTO { Id = category.Id, Name = category.Name };
        }

        public async Task<List<CategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = (await _categoryRepository.GetAllAsync()).ToList();
            return categories.Select(c => new CategoryDTO { Id = c.Id, Name = c.Name }).ToList();
        }

        public async Task<CategoryDTO> UpdateCategoryAsync(UpdateCategoryCommand command)
        {
            var category = await _categoryRepository.GetByIdAsync(command.CategoryId);
            if (category == null)
                throw new Exception("Категория не найдена.");

            category.UpdateName(command.NewName);
            await _categoryRepository.UpdateAsync(category);
            return new CategoryDTO { Id = category.Id, Name = category.Name };
        }

        public async Task DeleteCategoryAsync(DeleteCategoryCommand command)
        {
            await _categoryRepository.DeleteAsync(command.CategoryId);
        }
    }
}