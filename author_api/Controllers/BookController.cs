using author_data_access;
using author_data_types.Models;
using author_services.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace author_api.Controllers
{
    public class BookController : Controller
    {
        BookService _bookService;
        private readonly IMapper _mapper;

        public BookController(AuthorContext context, IMapper mapper)
        {
            _bookService = new BookService(context);
            _mapper = mapper;
        }


        [HttpGet]
        [Route("GetAllBookByAuthorId")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllBookByAuthorId(int id)
        {
            var bookList = await _bookService.GetAllBooksByAuthorId(id);


            if (bookList == null)
            {
                return NotFound("No books");
            }


            return Ok(bookList);
        }


        [HttpPost]
        [Route("AddBook")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AddBook([FromBody] BookCreateDto book)
        {
            if (book.AuthorId == 0)
            {
                return BadRequest();
            }

            var bookToAdd = _mapper.Map<Book>(book);

            await _bookService.AddBook(bookToAdd);

            return Ok(book);
        }

    }
}
