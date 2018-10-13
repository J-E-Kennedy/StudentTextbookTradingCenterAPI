using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SttcBookTrade.Entities
{

#pragma warning disable CS1591
    public class BookTradeContext : DbContext
    {
        public BookTradeContext(DbContextOptions<BookTradeContext> options)
            : base(options)
        {
            Database.Migrate();

        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);

            foreach (var relationship in modelbuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelbuilder.Entity<BookTradeLink>().HasKey(c => new { c.BookId, c.TradeId });

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<School> Schools { get; set; }

        public DbSet<Trade> Trade { get; set; }

        public DbSet<BookTradeLink> BookTradeLink {get; set;}

    }

#pragma warning restore CS1591
}
