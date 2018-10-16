using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SttcBookTrade.Models
{
    public class UserForCreationDto
    {
        public string Username { get; set; }
        
        public string Password { get; set; }
        
        public string Firstname { get; set; }
        
        public string Lastname { get; set; }
        
        public string Email { get; set; }
        
        public int SchoolId { get; set; }

        public string Profile { get; set; }

        public string StudentIdentification { get; set; }
    }
}
