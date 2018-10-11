using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace SttcBookTrade.Entities
{

#pragma warning disable CS1591
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public int UserId { get; set; }
        
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public string Edition { get; set; }

        //public bool IsOwned { get; set; }

        [Required]
        public string Author { get; set; }
        
        public string ISBN10 { get; set; }
        
        public string ISBN13 { get; set; }

        public int Price { get; set; }

        [MaxLength(50)]
        public string Condition { get; set; }

        public string Notes { get; set; }

}
#pragma warning restore CS1591
}
