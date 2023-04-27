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

        public void Add(Transaction item)
        {
            _unitOfWork.TransactionRepository.Add(item);
        }

        public void Delete(Transaction item)
        {
            _unitOfWork.TransactionRepository.Delete(item);
        }

        public IReadOnlyList<Transaction> GetAll()
        {
            return _unitOfWork.TransactionRepository.ListAll();
        }

        public Transaction GetById(int id)
        {
            return _unitOfWork.TransactionRepository.GetById(id);
        }

        public void Update(Transaction item)
        {
            _unitOfWork.TransactionRepository.Update(item);
        }
    }

}
