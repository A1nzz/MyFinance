using Application.Abstractions;
using Domain.Abstractions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {

        IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unit)
        {
            _unitOfWork = unit;
        }
        public Task AddAsync(Category item)
        {
            return _unitOfWork.CategoryRepository.AddAsync(item);
        }

        public Task DeleteAsync(Category item)
        {
            return _unitOfWork.CategoryRepository.DeleteAsync(item);
        }

        public Task SaveChangesAsync()
        {
            return _unitOfWork.SaveAllAsync();
        }

        public Task UpdateAsync(Category item)
        {
            return _unitOfWork.CategoryRepository.UpdateAsync(item);
        }

        Task<IReadOnlyList<Category>> IBaseService<Category>.GetAllAsync()
        {
            return _unitOfWork.CategoryRepository.ListAllAsync();
        }

        Task<Category> IBaseService<Category>.GetByIdAsync(int id)
        {
            return _unitOfWork.CategoryRepository.GetByIdAsync(id);
        }
    }
}
