using System.Linq;
using IntecWebShop.Models;

namespace IntecWebShop.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> Collection();
        void Commit();
        bool Delete(string Id);
        T FindById(string Id);
        void Insert(T classe);
        void Update(T classe);
    }
}