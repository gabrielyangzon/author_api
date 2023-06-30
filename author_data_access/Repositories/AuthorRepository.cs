﻿using author_data_types.Models;
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
        public Task<List<Author>> GetAuthors();
        public Task<List<Author>> GetAuthorsWithBooks();
        public Task<Author> GetAuthorById(int id);
        public Task<Author> AddAuthor(Author author);
        public Task<Author> EditAuthor(AuthorEditDto author);

        public Task<bool> DeleteAuthor(int id);
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


        public async Task<List<Author>> GetAuthors()
        {
            using (var context = new AuthorContext())
            {
                var authorList = await context.Authors.ToListAsync();
                return authorList;
            }

        }

        public async Task<List<Author>> GetAuthorsWithBooks()
        {
            using (var context = new AuthorContext())
            {
                var authorList = await context.Authors.Include(a => a.Books).ToListAsync();
                return authorList;
            }

        }


        public async Task<Author> GetAuthorById(int id)
        {
            using (var context = new AuthorContext())
            {
                var author = await context.Authors.FirstOrDefaultAsync(author => author.Id == id);

                return author;
            }
        }


        public async Task<Author> AddAuthor(Author author)
        {

            using (var context = new AuthorContext())
            {

                var newAuthor = context.Authors.Add(author);

                await context.SaveChangesAsync();

                return newAuthor.Entity;
            }

        }


        public async Task<Author> EditAuthor(AuthorEditDto authorModified)
        {
            using (var context = new AuthorContext())
            {

                Author author = new Author()
                {
                    Id = authorModified.Id,
                    FirstName = authorModified.FirstName,
                    LastName = authorModified.LastName
                };


                var result = context.Entry(author).State = EntityState.Modified;

                try
                {
                    await context.SaveChangesAsync();

                    return author;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public async Task<bool> DeleteAuthor(int id)
        {
            using (var context = new AuthorContext())
            {
                var author = context.Authors.FirstOrDefault(author => author.Id == id);

                if (author != null)
                {
                    context.Authors.Remove(author);
                    await context.SaveChangesAsync();
                    return true;
                }
            }

            return false;
        }
    }
}
