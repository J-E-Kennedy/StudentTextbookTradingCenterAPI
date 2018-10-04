using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SttcBookTrade.Entities
{
    public class BookTradeContext : DbContext
    {
        public BookTradeContext(DbContextOptions<BookTradeContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<School> Schools { get; set; }
        
    }
}
