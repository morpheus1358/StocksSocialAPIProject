using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScriptureExplorer.Models
{
    public class Verse
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Text { get; set; } = string.Empty;
        public int ChapterId { get; set; }
        public Chapter Chapter { get; set; } = null!;
    }
}