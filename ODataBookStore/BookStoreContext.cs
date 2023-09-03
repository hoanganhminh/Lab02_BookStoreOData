using Microsoft.EntityFrameworkCore;
using ODataBookStore.Models;

namespace ODataBookStore
{
    public class BookStoreContext : DbContext //step 6
    {
        public BookStoreContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Press> Presses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().OwnsOne(c => c.Location);
            modelBuilder.Entity<Book>().OwnsOne(c => c.Press);
        }
    }
}
