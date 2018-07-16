using System.Threading.Tasks;

namespace Portfoglio.Models
{
    public interface IRepositoryAuth //: IRepository<IUser>
    {
        Task<User> GetUser(User user);
    }
}