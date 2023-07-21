using author_data_access;
using author_data_types.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace author_testing
{
    public static class TestHelpers
    {
        public static AuthorContext GetDatabaseContext()
        {
            var databaseContext = new AuthorContext();

            databaseContext.Database.EnsureDeleted();
            databaseContext.Database.EnsureCreated();

            var authors = new List<Author>
                    {
                        new Author
                        {

                            FirstName ="Joydip",
                            LastName ="Kanjilal",
                               Books = new List<Book>()
                            {
                                new Book { Id = Guid.NewGuid(), AuthorId = 1, Title = "Mastering C# 8.0"},
                                new Book { Id = Guid.NewGuid(), AuthorId = 1,Title = "Entity Framework Tutorial"},
                                new Book { Id = Guid.NewGuid(), AuthorId = 1,Title = "ASP.NET 4.0 Programming"}
                            }
                        },
                        new Author
                        {

                            FirstName ="Yashavanth",
                            LastName ="Kanetkar",
                            Books = new List<Book>()
                            {
                                new Book { Id = Guid.NewGuid(), AuthorId = 2, Title = "Let us C"},
                                new Book { Id = Guid.NewGuid(), AuthorId = 2,Title = "Let us C++"},
                                new Book { Id = Guid.NewGuid(), AuthorId = 2,Title = "Let us C#"}
                            }
                        },
                        new Author
                        {

                            FirstName ="Test1",
                            LastName ="Test1",
                            Books = new List<Book>()
                            {
                                new Book { Id = Guid.NewGuid(), AuthorId = 2, Title = "Test book1"},
                                new Book { Id = Guid.NewGuid(), AuthorId = 2,Title = "Test book2"},
                                new Book { Id = Guid.NewGuid(), AuthorId = 2,Title = "Test book3"}
                            }
                        }
                    };

            databaseContext.Authors.AddRange(authors);
            databaseContext.SaveChanges();

            return databaseContext;
        }
    }
}
