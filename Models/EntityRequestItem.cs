using System.Collections.Generic;

namespace Karmin.Models
{
    public class EntityRequestItem
    {
        public List<DocumentItem> Documents { get; set; }
    }

    public class DocumentItem
    {
        public string Language { get; set; }
        public string Id { get; set; }
        public string Text { get; set; }
    }
}