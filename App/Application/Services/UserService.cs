using Application.Abstractions;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Add(User user)
        {
            _unitOfWork.UserRepository.Add(user);
        }

        public void Delete(User user)
        {
            _unitOfWork.UserRepository.Delete(user);
        }

        public IReadOnlyList<User> GetAll()
        {
            return _unitOfWork.UserRepository.ListAll();
        }

        public User GetById(int id)
        {
            return _unitOfWork.UserRepository.GetById(id);
        }

        public void Update(User user)
        {
            _unitOfWork.UserRepository.Update(user);
        }

    }
}
