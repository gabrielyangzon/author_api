using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using author_data_access;
using author_data_types.Models;
using author_data_access.Repositories;
using author_services.Services;

namespace author_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorContext _context;
        readonly AuthorService _authorService;


        public AuthorsController(IAuthorRepository authorRepository)
        {
            _authorService = new AuthorService();

        }


        // GET: api/Authors
        [HttpGet]
        [Route("Authors")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Author>>> GetAuthors()
        {
            var authors = await _authorService.GetAllAuthors();

            if (authors == null)
            {
                return NotFound();
            }


            return Ok(authors);
        }


        // GET: api/AuthorsWithBooks
        [HttpGet]
        [Route("AuthorsWithBooks")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Author>>> GetAuthorsWithBooks()
        {
            var authors = await _authorService.GetAllAuthorsWithBooks();

            if (authors == null)
            {
                return NotFound();
            }

            return Ok(authors);
        }


        // GET: api/Authors/5
        [HttpGet]
        [Route("GetAuthorById")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Author>> GetAuthorById([FromQuery] int id)
        {
            var author = await _authorService.GetAuthorById(id);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }


        // PUT: api/Authors/5
        //[HttpPut]
        //[Route("EditAuthor")]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public async Task<IActionResult> PutAuthor([FromQuery] int id, [FromBody] AuthorEditDto author)
        //{
        //    if (id != author.Id)
        //    {
        //        return BadRequest();
        //    }


        //    var modifiedAuthor = await _authorRepository.EditAuthor(author);

        //    if (modifiedAuthor == null)
        //    {
        //        return BadRequest();
        //    }



        //    return Ok(modifiedAuthor);
        //}


        // POST: api/Authors
        [HttpPost]
        [Route("AddAuthor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Author>> PostAuthor([FromBody] AuthorCreateDto author)
        {
            Author authorToBeAdded = new Author() { FirstName = author.FirstName, LastName = author.LastName };


            var newAuthor = _authorService.AddAuthor(authorToBeAdded);
            return Ok(newAuthor);
        }


        // DELETE: api/Authors/5
        //[HttpDelete]
        //[Route("DeleteAuthor")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> DeleteAuthor([FromQuery] int id)
        //{
        //    var authorToDelete = _authorRepository.GetAuthorById(id);

        //    if (authorToDelete == null)
        //    {
        //        return NotFound("Author not found");
        //    }

        //    bool isDeleted = await _authorRepository.DeleteAuthor(id);

        //    if (isDeleted)
        //    {
        //        return Ok("Author deleted");
        //    }

        //    return BadRequest();
        //}
    }
}
