using System.Collections.Generic;

namespace Karmin.Models
{
    public class EntityResponseItem
    {
        public List<EntityDocument> Documents { get; set; }
        public List<object> Errors { get; set; }
    }

    public class EntityDocument
    {
        public int Id { get; set; }
        public List<EntityEntry> Entities { get; set; }
    }

    public class EntityEntry
    {
        public string Name { get; set; }
        public List<Match> Matches { get; set; }
        public string Type { get; set; }
        public string SubType { get; set; }
        public string WikipediaLanguage { get; set; }
        public string WikipediaId { get; set; }
        public string WikipediaUrl { get; set; }
        public string BingId { get; set; }
    }

    public class Match
    {
        public string Text { get; set; }
        public int Offset { get; set; }
        public int Length { get; set; }
    }
}