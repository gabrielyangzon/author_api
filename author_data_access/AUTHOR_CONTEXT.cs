using author_data_types.Models;
using Microsoft.EntityFrameworkCore;

namespace author_data_access
{
    public class AuthorContext : DbContext
    {
        private bool forTest = false;
        public AuthorContext(bool forTest=false)
        {

            this.forTest = forTest;

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (forTest)
            {
                optionsBuilder.UseInMemoryDatabase("Bookstore");
            }
            else
            {
                optionsBuilder.UseSqlite(@"Data Source=Bookstore.db");
            }
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

    }



}