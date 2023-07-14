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

namespace author_testing.ControllerTests
{

    public class AuthorControllerTest
    {

        AuthorsController _authorController;
        private readonly Mock<IMapper> _mapper;
        private readonly AuthorContext _authorContext;



        public AuthorControllerTest()
        {
            _mapper = new Mock<IMapper>();
            _authorContext = TestHelpers.GetDatabaseContext();

            _authorController = new AuthorsController(_mapper.Object, _authorContext);



        }

        [Fact]
        public async Task GetAll_Author_Test()
        {
            // Arrange
            ///Act
            var result = (OkObjectResult)await _authorController.GetAuthors();

            ///Assert

            Assert.IsType<List<Author>>(result.Value);
        }


        [Fact]
        public async Task Delete_Author_Test()
        {

            ///Arrange
            IActionResult resultBefore = await _authorController.GetAuthors();
            var beforeDeleteAuthors = (resultBefore as ObjectResult).Value as List<Author>;

            ///Act
            var result = (OkObjectResult)await _authorController.DeleteAuthor(5);


            IActionResult resulAfter = await _authorController.GetAuthors();
            var afterDeleteAuthors = (resulAfter as ObjectResult).Value as List<Author>;


            ///Assert 
            Assert.Equal(result.StatusCode, 200);
            Assert.NotEqual(beforeDeleteAuthors.Count(), afterDeleteAuthors.Count());
        }

    }
}
