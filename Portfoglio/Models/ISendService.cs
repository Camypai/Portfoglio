using System.Threading.Tasks;

namespace Portfoglio.Models
{
    public interface ISendService
    {
        Task SendAsync(IMessage message);
    }
}