using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ds.Tutorial.Model
{
    public class Book
    {
        public int ID { get; set; }
        [Required]
        [StringLength(100)]
        public string? Title { get; set; }
        [StringLength(100)]
        public string? Author { get; set; }
        [Range(1900, 2100)]
        public int PublishYear { get; set; }
    }
}
