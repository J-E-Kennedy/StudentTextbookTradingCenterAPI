using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SttcBookTrade.Entities
{
    public class Trade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TradeId { get; set; }

        [ForeignKey("OffererUserId")]
        [InverseProperty("OffererUser")]
        public int OffererUserId { get; set; }
        public virtual User OffererUser { get; set; }

        [ForeignKey("ReceiverUserId")]
        [InverseProperty("ReceiverUser")]
        public int ReceiverUserId { get; set; }
        public virtual User ReceiverUser { get; set; }

        public ICollection<Book> Books { get; set; }
            = new List<Book>();


    }
}
