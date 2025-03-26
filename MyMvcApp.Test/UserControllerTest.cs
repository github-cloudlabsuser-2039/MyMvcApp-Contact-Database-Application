// filepath: c:\hackaton\MyMvcApp-Contact-Database-Application\Tests\Controllers\UserControllerTest.cs
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Controllers;
using MyMvcApp.Models;
using Xunit;

namespace MyMvcApp.Tests.Controllers
{
    public class UserControllerTest
    {
        [Fact]
        public void Index_ReturnsViewWithUserList()
        {
            // Arrange
            var controller = new UserController();
            UserController.userlist = new List<User>
            {
                new User { Id = 1, Name = "John Doe", Email = "john@example.com" },
                new User { Id = 2, Name = "Jane Smith", Email = "jane@example.com" }
            };

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<User>>(result.Model);
            Assert.Equal(2, ((List<User>)result.Model).Count);
        }

        [Fact]
        public void Details_ValidId_ReturnsViewWithUser()
        {
            // Arrange
            var controller = new UserController();
            UserController.userlist = new List<User>
            {
                new User { Id = 1, Name = "John Doe", Email = "john@example.com" }
            };

            // Act
            var result = controller.Details(1) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result.Model);
            Assert.Equal("John Doe", ((User)result.Model).Name);
        }

        [Fact]
        public void Details_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var controller = new UserController();
            UserController.userlist = new List<User>();

            // Act
            var result = controller.Details(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Create_PostValidUser_RedirectsToIndex()
        {
            // Arrange
            var controller = new UserController();
            var newUser = new User { Id = 1, Name = "John Doe", Email = "john@example.com" };

            // Act
            var result = controller.Create(newUser) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Contains(newUser, UserController.userlist);
        }

        [Fact]
        public void Edit_ValidId_ReturnsViewWithUser()
        {
            // Arrange
            var controller = new UserController();
            UserController.userlist = new List<User>
            {
                new User { Id = 1, Name = "John Doe", Email = "john@example.com" }
            };

            // Act
            var result = controller.Edit(1) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result.Model);
            Assert.Equal("John Doe", ((User)result.Model).Name);
        }

        [Fact]
        public void Edit_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var controller = new UserController();
            UserController.userlist = new List<User>();

            // Act
            var result = controller.Edit(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_ValidId_RedirectsToIndex()
        {
            // Arrange
            var controller = new UserController();
            var user = new User { Id = 1, Name = "John Doe", Email = "john@example.com" };
            UserController.userlist = new List<User> { user };

            // Act
            var result = controller.Delete(1, null) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.DoesNotContain(user, UserController.userlist);
        }

        [Fact]
        public void Delete_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var controller = new UserController();
            UserController.userlist = new List<User>();

            // Act
            var result = controller.Delete(1, null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}