using System.Collections.Generic;

namespace Telegram4Net.SchemaTools.Models
{
    internal class Constructor
    {
        public int Id { get; set; }
        public string Predicate { get; set; }
        public List<Param> Params { get; set; }
        public string Type { get; set; }
    }
}