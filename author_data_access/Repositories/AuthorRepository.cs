using author_data_types.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace author_data_access.Repositories
{

    public interface IAuthorRepository
    {
        public List<Author> GetAuthors();
        public List<Author> GetAuthorsWithBooks();

        public Author AddAuthor(AuthorCreateDto author);

        public Author GetAuthorById(int id);
    }

    public class AuthorRepository : IAuthorRepository
    {

        public AuthorRepository()
        {
            using (var context = new AuthorContext())
            {
                var authors = new List<Author>
                {
                new Author
                {
                    Id = 1,
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
                     Id = 2,
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


        public List<Author> GetAuthors()
        {
            using (var context = new AuthorContext())
            {
                var authorList = context.Authors.ToList();
                return authorList;
            }

        }

        public List<Author> GetAuthorsWithBooks()
        {
            using (var context = new AuthorContext())
            {
                var authorList = context.Authors.Include(a => a.Books).ToList();
                return authorList;
            }

        }


        public Author GetAuthorById(int id)
        {
            using (var context = new AuthorContext())
            {
                var author = context.Authors.FirstOrDefault(author => author.Id == id);

                return author;
            }
        }


        public Author AddAuthor(AuthorCreateDto author)
        {

            using (var context = new AuthorContext())
            {
                Author addAuthor = new Author() { FirstName = author.FirstName, LastName = author.LastName };
                var newAuthor = context.Authors.Add(addAuthor);

                context.SaveChanges();

                return newAuthor.Entity;
            }





        }



    }
}
