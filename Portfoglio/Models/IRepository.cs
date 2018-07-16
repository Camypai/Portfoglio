using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfoglio.Models
{
    public interface IRepository<T>  where T : IBaseModel
    {
        IEnumerable<T> GetList();
        Task<T> GetItem(int id);
        void Create(T item);
        void Create(IEnumerable<T> items);
        void Update(T item);
        void Delete(int id);
        void Delete(T item);
        void Save();
        Task SaveAsync();
        
    }
}