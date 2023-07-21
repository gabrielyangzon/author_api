using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using author_data_access;
using author_data_types.Models;

using author_services.Services;
using AutoMapper;

namespace author_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorContext _context;
        private readonly IMapper _mapper;
        readonly AuthorService _authorService;



        public AuthorsController(IMapper mapper, AuthorContext context)
        {
            _authorService = new AuthorService(context);
            _mapper = mapper;
        }


        // GET: api/Authors
        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AuthorOnlyResponse>))]
        public async Task<ActionResult> GetAuthors()
        {
            var authors = await _authorService.GetAllAuthors();

            if (authors == null)
            {
                return NotFound();
            }


            var authorResponse = _mapper.Map<List<AuthorOnlyResponse>>(authors);



            return Ok(authorResponse);
        }


        // GET: api/AuthorsWithBooks
        [HttpGet]
        [Route("GetAllAuthorsWithBooks")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Author))]
        public async Task<ActionResult> GetAuthorsWithBooks()
        {
            var authors = await _authorService.GetAllAuthorsWithBooks();

            if (authors == null)
            {
                return NotFound();
            }

            return Ok(authors);
        }


        // GET: api/Authors/5
        [HttpGet("{id}")]

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Author))]
        public async Task<ActionResult> GetAuthorById(int id)
        {
            var author = await _authorService.GetAuthorById(id);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }



        [HttpPut]
        [Route("EditAuthor")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PutAuthor([FromQuery] int id, [FromBody] AuthorEditDto author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            var authorToBeModified = _mapper.Map<Author>(author);

            var modifiedAuthor = await _authorService.EditAuthor(authorToBeModified);

            if (modifiedAuthor == null)
            {
                return BadRequest();
            }



            return Ok(modifiedAuthor);
        }



        [HttpPost]
        [Route("AddAuthor")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Author))]
        public async Task<ActionResult> PostAuthor([FromBody] AuthorCreateDto author)
        {

            var authorToBeAdded = _mapper.Map<Author>(author);

            var newAuthor = await _authorService.AddAuthor(authorToBeAdded);
            return Ok(newAuthor);
        }


        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var authorToDelete = _authorService.GetAuthorById(id);

            if (authorToDelete == null)
            {
                return NotFound("Author not found");
            }

            bool isDeleted = await _authorService.DeleteAuthor(id);

            if (isDeleted)
            {
                return Ok("Author deleted");
            }

            return BadRequest();
        }
    }
}
