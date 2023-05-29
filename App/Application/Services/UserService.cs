using Application.Abstractions;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task AddAsync(User item)
        {
            return _unitOfWork.UserRepository.AddAsync(item);
        }

        public Task DeleteAsync(User item)
        {
            return _unitOfWork.UserRepository.DeleteAsync(item);
        }

        public Task SaveChangesAsync()
        {
            return _unitOfWork.SaveAllAsync();
        }

        public Task UpdateAsync(User item)
        {
            return _unitOfWork.UserRepository.UpdateAsync(item);
        }

        Task<IReadOnlyList<User>> IBaseService<User>.GetAllAsync()
        {
            return _unitOfWork.UserRepository.ListAllAsync();
        }

        Task<User> IBaseService<User>.GetByIdAsync(int id)
        {
            return _unitOfWork.UserRepository.GetByIdAsync(id);
        }

        public Task<User> FirstOrDefaultAsync(Expression<Func<User, bool>> filter, CancellationToken cancellationToken = default)
        {
            return _unitOfWork.UserRepository.FirstOrDefaultAsync(filter, cancellationToken);
        }

    }
}
