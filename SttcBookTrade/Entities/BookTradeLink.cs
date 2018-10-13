using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SttcBookTrade.Entities
{
    public class BookTradeLink
    {
        [ForeignKey("BookId")]
        public int BookId { get; set; }
        public Book Book { get; set; }
        
        [ForeignKey("TradeId")]
        public int TradeId { get; set; }
        public Trade Trade { get; set; }
    }
}
