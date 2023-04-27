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
        public void Add(Category item)
        {
            _unitOfWork.CategoryRepository.Add(item);
        }

        public void Delete(Category item)
        {
            _unitOfWork.CategoryRepository.Delete(item);
        }

        public IReadOnlyList<Category> GetAll()
        {
            return _unitOfWork.CategoryRepository.ListAll();
        }

        public Category GetById(int id)
        {
            return _unitOfWork.CategoryRepository.GetById(id);
        }

        public void Update(Category item)
        {
            _unitOfWork.CategoryRepository.Update(item);
        }
    }
}
