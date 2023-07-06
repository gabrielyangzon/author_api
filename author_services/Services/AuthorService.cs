using author_data_access;
using author_data_access.Repositories;
using author_data_types.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace author_services.Services
{
    public class AuthorService
    {
        readonly IAuthorRepository _authorRepository;

        public AuthorService()
        {

            _authorRepository = new AuthorRepository();

        }


        public async Task<List<Author>> GetAllAuthors()
        {
            return await _authorRepository.GetAuthors();
        }


        public async Task<List<Author>> GetAllAuthorsWithBooks()
        {
            return await _authorRepository.GetAuthorsWithBooks();
        }

        public async Task<Author> GetAuthorById(int id)
        {
            return await _authorRepository.GetAuthorById(id);
        }

        public async Task<Author> AddAuthor(Author author)
        {
            return await _authorRepository.AddAuthor(author);
        }

        public async Task<Author> EditAuthor(Author author)
        {
            return await _authorRepository.EditAuthor(author);
        }


        public async Task<bool> DeleteAuthor(int id)
        {
            return await _authorRepository.DeleteAuthor(id);
        }
    }
}
