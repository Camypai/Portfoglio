using System.Collections.Generic;

namespace Portfoglio.Models
{
    public class Message : IMessage
    {
        public IEnumerable<string> From { get; set; }
        public IEnumerable<string> To { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Name { get; set; }
    }
}