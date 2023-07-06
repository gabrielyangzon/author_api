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
        public Task<List<Author>> GetAuthors();
        public Task<List<Author>> GetAuthorsWithBooks();
        public Task<Author> GetAuthorById(int id);
        public Task<Author> AddAuthor(Author author);
        public Task<Author> EditAuthor(Author author);

        public Task<bool> DeleteAuthor(int id);
    }

    public class AuthorRepository : IAuthorRepository
    {

        public AuthorRepository()
        {

        }


        public async Task<List<Author>> GetAuthors()
        {
            using (var context = new AuthorContext())
            {
                var authorList = await context.Authors.AsNoTracking().ToListAsync();
                return authorList;
            }

        }

        public async Task<List<Author>> GetAuthorsWithBooks()
        {
            using (var context = new AuthorContext())
            {
                var authorList = await context.Authors.AsNoTracking().Include(a => a.Books).ToListAsync();
                return authorList;
            }

        }


        public async Task<Author> GetAuthorById(int id)
        {
            using (var context = new AuthorContext())
            {
                var author = await context.Authors.AsNoTracking().Include(a => a.Books).FirstOrDefaultAsync(author => author.Id == id);

                return author;
            }
        }


        public async Task<Author> AddAuthor(Author author)
        {

            using (var context = new AuthorContext())
            {

                await context.Authors.AddAsync(author);

                await context.SaveChangesAsync();

                return author;
            }

        }


        public async Task<Author> EditAuthor(Author author)
        {
            using (var context = new AuthorContext())
            {
                var findAuthor = GetAuthorById(author.Id).Result;

                if (findAuthor != null)
                {
                    author.Books = findAuthor.Books;

                }

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
