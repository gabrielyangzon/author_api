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
    public class BookService
    {
        private IBookRepository _bookRepository;

        public BookService(AuthorContext context)
        {
            _bookRepository = new BookRepository(context);
        }


        public async Task<List<Book>> GetAllBooksByAuthorId(int id)
        {
            List<Book> booksByAuthor = await _bookRepository.GetAllBooksByAuthorId(id);
            return booksByAuthor;
        }


        public async Task<Book> AddBook(Book book)
        {
            Book addedBook = await _bookRepository.AddBook(book);
            return addedBook;
        }

        public async Task<Book> UpdateBook(Book book)
        {
            Book updatedBook = await _bookRepository.UpdateBook(book);
            return updatedBook;
        }

        public async Task<bool> DeleteBook(Guid id)
        {
            bool isDeleted = await _bookRepository.DeleteBook(id);
            return isDeleted;
        }



    }
}
