using System.Collections.Generic;

namespace ShoppingCart.Interfaces
{

    public interface IRepository<T>
    {
        void Add(T item);
        T Get(int id);
        List<T> GetAll();
        List<T> GetAllById(List<int> id);
    }
}