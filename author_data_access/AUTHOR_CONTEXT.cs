using author_data_types.Models;
using Microsoft.EntityFrameworkCore;

namespace author_data_access
{
    public class AuthorContext : DbContext
    {




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlite(@"Data Source=Bookstore.db");
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

    }



}