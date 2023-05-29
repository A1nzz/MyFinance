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
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task AddAsync(Transaction item)
        {
            return _unitOfWork.TransactionRepository.AddAsync(item);
        }

        public Task DeleteAsync(Transaction item)
        {
            return _unitOfWork.TransactionRepository.DeleteAsync(item);
        }

        public Task SaveChangesAsync()
        {
            return _unitOfWork.SaveAllAsync();
        }

        public Task UpdateAsync(Transaction item)
        {
            return _unitOfWork.TransactionRepository.UpdateAsync(item);
        }

        Task<IReadOnlyList<Transaction>> IBaseService<Transaction>.GetAllAsync()
        {
            return _unitOfWork.TransactionRepository.ListAllAsync();
        }

        Task<Transaction> IBaseService<Transaction>.GetByIdAsync(int id)
        {
            return _unitOfWork.TransactionRepository.GetByIdAsync(id);
        }
    }

}
