using System.Collections.Generic;

namespace Telegram4Net.SchemaTools.Models
{
    internal class Schema
    {
        public List<Constructor> Constructors { get; set; }
        public List<Method> Methods { get; set; }
    }
}