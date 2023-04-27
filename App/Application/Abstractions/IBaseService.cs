using Domain.Entities;

namespace Application.Abstractions
{
    public interface IBaseService<T> where T : Entity
    {
        IReadOnlyList<T> GetAll();
        T GetById(int id);
        void Add(T item);
        void Update(T item);
        void Delete(T item);
    }
}
