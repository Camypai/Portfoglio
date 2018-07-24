using System.Collections.Generic;

namespace Portfoglio.Models
{
    public interface IMessage
    {
        IEnumerable<string> From { get; set; }
        IEnumerable<string> To { get; set; }
        string Title { get; set; }
        string Text { get; set; }
        string Name { get; set; }
    }
}