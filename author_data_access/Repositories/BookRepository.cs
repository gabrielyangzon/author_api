﻿using author_data_types.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace author_data_access.Repositories
{
    public interface IBookRepository
    {
        public Task<List<Book>> GetAllBooksByAuthorId(int authorId);

        public Task<Book> AddBook(Book book);

        public Task<Book> UpdateBook(Book book);

        public Task<bool> DeleteBook(Guid id);
    }


    public class BookRepository : IBookRepository
    {
        private AuthorContext _context;
        public BookRepository(AuthorContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetAllBooksByAuthorId(int authorId)
        {
            var allBooks = await _context.Books.Where(x => x.AuthorId == authorId).ToListAsync();

            return allBooks;
        }

        public async Task<Book> AddBook(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();

            return book;
        }

        public async Task<Book> UpdateBook(Book book)
        {

            _context.Books.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return book;
            }

            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<bool> DeleteBook(Guid id)
        {
            try
            {
                Book? bookToBeRemoved = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
                if (bookToBeRemoved == null) return false;
               
                _context.Books.Remove(bookToBeRemoved);
                await _context.SaveChangesAsync();
                return true;



            }
            catch (Exception ex)
            {
                return false;
            }


        }


    }
}
