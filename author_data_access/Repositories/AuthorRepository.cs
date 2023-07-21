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
        private readonly AuthorContext _context;

        public AuthorRepository(AuthorContext context)
        {
            _context = context;
        }


        public async Task<List<Author>> GetAuthors()
        {

            var authorList = await _context.Authors.AsNoTracking().ToListAsync();


            return authorList;


        }

        public async Task<List<Author>> GetAuthorsWithBooks()
        {

            var authorList = await _context.Authors.AsNoTracking().Include(a => a.Books).ToListAsync();
            return authorList;


        }


        public async Task<Author> GetAuthorById(int id)
        {

            var author = await _context.Authors.AsNoTracking().Include(a => a.Books).FirstOrDefaultAsync(author => author.Id == id);

            return author;

        }


        public async Task<Author> AddAuthor(Author author)
        {



            await _context.Authors.AddAsync(author);

            await _context.SaveChangesAsync();

            return author;


        }


        public async Task<Author> EditAuthor(Author author)
        {

            var findAuthor = GetAuthorById(author.Id).Result;

            if (findAuthor != null)
            {
                author.Books = findAuthor.Books;

            }

            var result = _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return author;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<bool> DeleteAuthor(int id)
        {

            var author = _context.Authors.FirstOrDefault(author => author.Id == id);

            if (author != null)
            {
                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
                return true;
            }


            return false;
        }
    }
}
