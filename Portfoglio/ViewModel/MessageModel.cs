using System.Collections.Generic;
using Portfoglio.Models;

namespace Portfoglio.ViewModel
{
    public class MessageModel : IMessage
    {
        public IEnumerable<string> From { get; set; }
        public IEnumerable<string> To { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Name { get; set; }
    }
}