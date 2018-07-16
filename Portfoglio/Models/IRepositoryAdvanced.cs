namespace Portfoglio.Models
{
    public interface IRepositoryAdvanced<T> : IRepository<T> where T : IBaseModel
    {
        void Hide(int id);
        void Hide(T item);
        void Show(int id);
        void Show(T item);
    }
}