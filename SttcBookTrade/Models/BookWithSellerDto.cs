using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SttcBookTrade.Models
{
#pragma warning disable CS1591
    public class BookWithSellerDto
    {
        public int BookId { get; set; }

        public string Name { get; set; }

        public string Edition { get; set; }

        public string Author { get; set; }

        public string ISBN10 { get; set; }

        public string ISBN13 { get; set; }

        public float Price { get; set; }

        public string Condition { get; set; }

        public string Notes { get; set; }

        public UserWithoutBooksDto User { get; set; }
    }
#pragma warning restore CS1591
}
