using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BookOpinions.Data.Models;
using BookOpinions.Services;
using BookOpinions.Services.Models.Book;
using BookOpinions.Web.Controllers;
using BookOpinions.Web.Models.Home;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BookOpinions.UnitTests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Index_ShouldPass()
        {
            //var controller = new HomeController();
            //controller.WithCallTo(c => c.Index()).ShouldRenderDefaultView()
            //    .WithModel<IEnumerable<SimpleBookViewModel>>();
        }

        [TestMethod]
        public async Task Index_ReturnsAViewResult_WithAListOfBrainstormSessions()
        {
            // Arrange
            var mockService = new Mock<IHomeService>();

            RoleManager<IdentityRole> mockRoleManager = new Mock<RoleManager<IdentityRole>>().Object;
            UserManager<User> mockUserManager = new Mock<UserManager<User>>().Object;

            //mockService.Setup(homeService => homeService.GetPopularBooks(It.IsAny<int>())).Returns(Task.FromResult(GetBookWellsCollection()));
            //var controller = new HomeController(mockService.Object);

            //// Act
            //var result = await controller.Index();

            //// Assert
            //var viewResult = Assert.IsType<ViewResult>(result);
            //var model = Assert.IsAssignableFrom<IEnumerable<StormSessionViewModel>>(
            //    viewResult.ViewData.Model);
            //Assert.Equal(2, model.Count());
        }

        private List<BookWellsCollectionServiceModel> GetBookWellsCollection()
        {
            return null;
        }
    }
}
