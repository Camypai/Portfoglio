using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfoglio.Models
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetList();
        T GetItem(int id);
        void Create(T item);
        void Update(T item);
        void Hide(int id);
        void Hide(T item);
        void Delete(int id);
        void Delete(T item);
        void Save();
        Task SaveAsync();
    }
}