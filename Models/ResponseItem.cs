using System.Collections.Generic;

namespace Karmin.Models
{
    public class ResponseItem
    {
        public string OriginalMessage { get; set; }
        public int Temperature { get; set; }
        public List<EntityItem> Entities { get; set; }
    }
}