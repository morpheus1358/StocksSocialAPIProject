using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScriptureExplorer.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Testament { get; set; } = string.Empty;
        public int TotalChapters { get; set; }
        public List<Chapter> Chapters { get; set; } = new List<Chapter>();
    }
}