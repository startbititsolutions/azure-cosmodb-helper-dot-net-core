using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmos_console.Model
{
    public class Conversation
    {
        public string id { get; set; }
        public string type { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
        public string userId { get; set; }
        public string title { get; set; }
    }
}
