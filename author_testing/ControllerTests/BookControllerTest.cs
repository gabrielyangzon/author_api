using author_api.AutoMapperProfile;
using author_api.Controllers;
using author_data_access;
using author_data_types.Models;
using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace author_testing.ControllerTests
{
    public class BookControllerTest
    {

        private readonly BookController _bookController;
        private readonly IMapper _mapper;
        private readonly AuthorContext _authorContext;


        public BookControllerTest()
        {

            var mockMapper = new MapperConfiguration(config =>
            {
                config.AddProfile(new AutomapperProfile());

            });

            _mapper = mockMapper.CreateMapper();
            _authorContext = TestHelpers.GetDatabaseContext();
            _bookController = new BookController(_authorContext, _mapper);
        }

        [Fact]
        public async Task GetAll_BookByAuthorId_Test()
        {
            //Arrange
            ///Act
            var bookList = (OkObjectResult)await _bookController.GetAllBookByAuthorId(1);


            //Assert
            Assert.IsType<List<Book>>(bookList.Value);
            Assert.NotNull(bookList.Value);

        }

        [Fact]
        public async Task AddBook_Test()
        {
            ///Arrange   
            var bookToBeAddedBad = new BookCreateDto() { AuthorId = 0, Title = "Testbookbad", };
            var bookToBeAdded = new BookCreateDto() { AuthorId = 1, Title = "Testbook", };

            ///Act
            var resultBadRequest = (BadRequestResult)await _bookController.AddBook(bookToBeAddedBad);
            var resultOk = (OkObjectResult)await _bookController.AddBook(bookToBeAdded);

            ///Assert
            Assert.Equal(resultBadRequest.StatusCode, StatusCodes.Status400BadRequest);
            Assert.Equal(resultOk.StatusCode, 200);

        }

        [Fact]
        public async Task Update_Book_Test()
        {
            ///Arrange
            int authorId = 1;
            var allBook = ((OkObjectResult)await _bookController.GetAllBookByAuthorId(authorId)).Value as List<Book>;
            var bookToUpdate = allBook.First();
            bookToUpdate.Title = "TitleTest";
            ///Act
            var result = (OkObjectResult)await _bookController.UpdateBook(bookToUpdate, bookToUpdate.Id);
            var updatedBook = result.Value as Book;

            //Assert
            Assert.NotNull(updatedBook);
            Assert.Equal(bookToUpdate.Title, updatedBook.Title);
        }

    }
}
