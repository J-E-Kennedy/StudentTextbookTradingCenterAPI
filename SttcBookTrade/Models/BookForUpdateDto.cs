using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SttcBookTrade.Models
{
#pragma warning disable CS1591
    public class BookForUpdateDto
    {
        [Required(ErrorMessage = "Requries a name.")]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Requries an edition.")]
        public string Edition { get; set; }

        [Required(ErrorMessage = "Requries an author.")]
        public string Author { get; set; }

        public string ISBN10 { get; set; }

        public string ISBN13 { get; set; }

        public int Price { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Condition cannot exceed 50 characters.")]
        public string Condition { get; set; }

        public string Notes { get; set; }
    }
#pragma warning restore CS1591
}
