using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SttcBookTrade.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        [Required]
        [MaxLength(50)]
        public string Firstname { get; set; }

        [Required]
        [MaxLength(50)]
        public string Lastname { get; set; }

        [Required]
        [MaxLength(254)]
        public string Email { get; set; }

        [ForeignKey("SchoolId")]
        public School School { get; set; }

        public int SchoolId { get; set; }
        
        public string Profile { get; set; }

        public string StudentIdentification { get; set; }

        public ICollection<Book> Books { get; set; }
            = new List<Book>();
        
    }
}
