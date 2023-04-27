using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions
{
    public interface IRepository<T>
    {
        T GetById(int id);

        IReadOnlyList<T> ListAll();

        IReadOnlyList<T> List(Func<T, bool> filter);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        T FirstOrDefault(Func<T, bool> filter);
    }
}
