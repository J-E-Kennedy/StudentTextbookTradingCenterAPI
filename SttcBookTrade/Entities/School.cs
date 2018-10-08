using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SttcBookTrade.Entities
{

#pragma warning disable CS1591
    public class School
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SchoolId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string Website { get; set; }
        public string Info { get; set; }

        public ICollection<User> Users { get; set; }
        = new List<User>();
    }

#pragma warning restore CS1591
}
