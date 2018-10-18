using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SttcBookTrade.Models
{
#pragma warning disable CS1591
    public class UserDto
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        //public string Lastname { get; set; }
        //public string Email { get; set; }
        ////public int SchoolId { get; set; }
        public string Profile { get; set; }
        //public int StudentIdentification { get; set; }

        public int NumberOfBooksForSale { get
            {
                return Books.Count;
            }
        }

        public ICollection<BookDto> Books { get; set; }
        = new List<BookDto>();
    }
#pragma warning restore CS1591
}
