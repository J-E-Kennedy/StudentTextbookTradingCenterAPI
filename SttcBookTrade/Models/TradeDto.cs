using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SttcBookTrade.Models
{
    public class TradeDto
    {
        public int TradeId { get; set; }

        public int SellerId { get; set; }

        public int ReceiverId { get; set; }
        
        public ICollection<BookDto> Books { get; set; }
        = new List<BookDto>();
    }
}
