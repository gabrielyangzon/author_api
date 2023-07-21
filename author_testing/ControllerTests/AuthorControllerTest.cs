using author_api.Controllers;
using author_data_types.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using AutoMapper;
using author_api.Extensions;
using author_data_access;
using System.Reflection.Metadata;
using author_data_access.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq.EntityFrameworkCore;
using System.Collections.Generic;
using NuGet.Protocol;
using Newtonsoft.Json.Linq;
using author_api.AutoMapperProfile;

namespace author_testing.ControllerTests
{

    public class AuthorControllerTest
    {

        AuthorsController _authorController;
        private readonly IMapper _mapper;
        private readonly AuthorContext _authorContext;



        public AuthorControllerTest()
        {

            var mockMapper = new MapperConfiguration(config =>
            {
                config.AddProfile(new AutomapperProfile());

            });

            _mapper = mockMapper.CreateMapper();

            _authorContext = TestHelpers.GetDatabaseContext();
            _authorController = new AuthorsController(_mapper, _authorContext);
        }

        [Fact]
        public async Task GetAll_Author_Test()
        {
            // Arrange
            ///Act
            var result = (OkObjectResult)await _authorController.GetAuthors();

            ///Assert
            Assert.Equal(result.StatusCode, 200);
            Assert.IsType<List<AuthorOnlyResponse>>(result.Value);
        }


        [Fact]
        public async Task GetAll_Author_WithBooks_Test()
        {
            // Arrange
            ///Act
            var result = (OkObjectResult)await _authorController.GetAuthorsWithBooks();

            ///Assert

            Assert.IsType<List<Author>>(result.Value);
        }


        [Fact]
        public async Task Get_Author_By_Id_Test()
        {
            // Arrange
            ///Act
            var result = (OkObjectResult)await _authorController.GetAuthorById(2);

            ///Assert

            Assert.IsType<Author>(result.Value);
            Assert.NotNull(result.Value);
        }

        [Fact]
        public async Task Edit_Author_Test()
        {
            ///Arrange
            var authorId = 1;
            var author = (OkObjectResult)await _authorController.GetAuthorById(authorId);
            var editAuthor = author.Value as AuthorEditDto;

            if (editAuthor != null)
            {
                editAuthor.FirstName = "testfirst";
                editAuthor.LastName = "testLastName";


                //Act
                var result = (OkObjectResult)await _authorController.PutAuthor(authorId, editAuthor);

                ///Assert
                Assert.Equal(result.StatusCode, 200);
                Assert.IsType<Author>(result.Value);
                Assert.NotNull(result.Value);
                Assert.Equal(authorId, (result.Value as Author).Id);
                Assert.Equal(editAuthor.FirstName, (result.Value as Author).FirstName);
                Assert.Equal(editAuthor.LastName, (result.Value as Author).LastName);
            }

        }

        [Fact]
        public async Task Add_Author_Test()
        {

            ///Arrange
            var newAuthor = new AuthorCreateDto { FirstName = "Gabriel", LastName = "Yangzon" };

            ///Act
            var result = (OkObjectResult)await _authorController.PostAuthor(newAuthor);


            ///Assert
            Assert.Equal(result.StatusCode, 200);
            Assert.IsType<Author>(result.Value);
            Assert.NotNull(result.Value);
            Assert.Equal(newAuthor.FirstName, (result.Value as Author).FirstName);
            Assert.Equal(newAuthor.LastName, (result.Value as Author).LastName);
        }

        [Fact]
        public async Task Delete_Author_Test()
        {

            ///Arrange
            var resultBefore = await _authorController.GetAuthors();
            var beforeDeleteAuthors = (resultBefore as ObjectResult).Value as List<AuthorOnlyResponse>;

            ///Act
            var result = (OkObjectResult)await _authorController.DeleteAuthor(1);


            var resulAfter = await _authorController.GetAuthors();
            var afterDeleteAuthors = (resulAfter as ObjectResult).Value as List<AuthorOnlyResponse>;


            ///Assert 
            Assert.Equal(result.StatusCode, 200);
            Assert.NotEqual(beforeDeleteAuthors.Count(), afterDeleteAuthors.Count());
        }



    }
}
