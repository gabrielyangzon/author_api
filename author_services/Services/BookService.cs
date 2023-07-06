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
            return await _bookRepository.GetAllBooksByAuthorId(id);
        }


        public async Task<Book> AddBook(Book book)
        {
            return await _bookRepository.AddBook(book);
        }



    }
}
