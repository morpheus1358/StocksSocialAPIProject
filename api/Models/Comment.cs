
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Comment
    {

        public int Id { get; set; }
        public int? StockId { get; set; }
        // Navigation

        public string Title { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string Content { get; set; } = string.Empty;

        public Stock? Stock { get; set; }
    }
}