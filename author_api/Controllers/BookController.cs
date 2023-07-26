using author_data_access;
using author_data_types.Models;
using author_services.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(List<Book>))]
        public async Task<IActionResult> GetAllBookByAuthorId([Required]int id)
        {
            if(id == 0) { return BadRequest(); }
            var bookList = await _bookService.GetAllBooksByAuthorId(id);


            if (bookList == null)
            {
                return NotFound("No books");
            }


            return Ok(bookList);
        }


        [HttpPost]
        [Route("AddBook")]
        [ProducesResponseType(200, Type = typeof(Book))]
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

        [HttpPut]
        [Route("UpdateBook")]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateBook([FromBody] Book book,Guid id)
        {


            Guid output;
            bool isValid = Guid.TryParse(book.Id.ToString(),out output);

            if (!isValid || book.Id != id) { return BadRequest(); }

            var updatedBook = await _bookService.UpdateBook(book);

            return Ok(book);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200,Type=typeof(bool))]
        public async Task<IActionResult> DeleteBook(Guid id)
        {

           bool isDeleted = await _bookService.DeleteBook(id);

            if(!isDeleted)
            {
                return BadRequest(false);
            }


            return Ok(true);
        }        

    }
}
