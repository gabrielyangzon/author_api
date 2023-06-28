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

namespace author_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorContext _context;
        readonly IAuthorRepository _authorRepository;

        public AuthorsController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        // GET: api/Authors
        [HttpGet]
        [Route("Authors")]
        public async Task<ActionResult<List<Author>>> GetAuthors()
        {
            var authors = _authorRepository.GetAuthors();

            if (authors == null)
            {
                return NotFound();
            }


            return Ok(authors);
        }

        // GET: api/AuthorsWithBooks
        [HttpGet]
        [Route("AuthorsWithBooks")]
        public async Task<ActionResult<List<Author>>> GetAuthorsWithBooks()
        {
            var authors = _authorRepository.GetAuthorsWithBooks();

            if (authors == null)
            {
                return NotFound();
            }


            return Ok(authors);
        }




        // GET: api/Authors/5
        //[HttpGet("{id}")]
        //[Route("GetAuthorById")]
        //public async Task<ActionResult<Author>> GetAuthorById(int id)
        //{

        //    var author = _authorRepository.GetAuthorById(id);

        //    if (author == null)
        //    {
        //        return NotFound();
        //    }

        //    return author;
        //}

        // PUT: api/Authors/5
        [HttpPut("{id}")]
        [Route("EditAuthor")]
        public async Task<IActionResult> PutAuthor(int id, Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Authors

        [HttpPost]
        [Route("AddAuthor")]
        public async Task<ActionResult<Author>> PostAuthor(AuthorCreateDto author)
        {

            var newAuthor = _authorRepository.AddAuthor(author);

            return Ok(newAuthor);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        [Route("DeleteAuthor")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            if (_context.Authors == null)
            {
                return NotFound();
            }
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
