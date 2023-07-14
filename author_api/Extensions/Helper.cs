using author_data_access;
using author_data_types.Models;
using Microsoft.EntityFrameworkCore;

namespace author_api.Extensions
{
    public static class Helper
    {

        public static void CreateDbIfNotExists(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<AuthorContext>();
                    Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }

        }


        public static void Initialize(AuthorContext context)
        {
            if (context.Authors.Any())
            {
                return;
            }

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
                }
            };
            context.Authors.AddRange(authors);
            context.SaveChanges();

        }


    }
}
