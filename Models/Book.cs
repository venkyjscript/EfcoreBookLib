using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrary.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long BookId { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public DateTime DOR { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
    }

    public class Book2
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long BookId { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public DateTime DOR { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
    }
}
