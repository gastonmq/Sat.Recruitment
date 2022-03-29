using System;
using System.Dynamic;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Controllers;
using Services;
using Xunit;
using Moq;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UserServiceTest
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<IFileService> _mockFileService;
        private readonly Mock<IMoneyService> _mockMoneyService;
        private readonly Mock<ILogger<UsersController>> _mockLogger;
        private readonly UserService _userService;
        private readonly FileService _fileService;

        public UserServiceTest()
        {
            _mockUserService = new Mock<IUserService>();
            _mockFileService = new Mock<IFileService>();
            _mockMoneyService = new Mock<IMoneyService>();
            _mockLogger = new Mock<ILogger<UsersController>>();
            _usersController = new UsersController(_mockUserService.Object,_mockLogger.Object);
            _userService = new UserService(_mockFileService.Object, _mockMoneyService.Object);
            _fileService = new FileService();
        }

        [Fact]
        public void CreateUserSuccess()
        {


            User user = new User()
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = 124
            };

            var returnReadUsersFromFile = _fileService.ReadUsersFromFile();
            _mockFileService.Setup(service => service.ReadUsersFromFile()).Returns(returnReadUsersFromFile);
            _mockFileService.Setup(service => service.AppendUserToTxt(user)).Returns("User Created.");
            var result = _userService.CreateUser(user);

            Assert.True(result.IsSuccess);
            Assert.Equal("User Created.", result.Message);

        }

        [Fact]
        public void CreateUserDuplicated()
        {
            User user = new User()
            {
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = 124
            };
            var returnReadUsersFromFile = _fileService.ReadUsersFromFile();
            _mockFileService.Setup(service => service.ReadUsersFromFile()).Returns(returnReadUsersFromFile);
            var result = _userService.CreateUser(user);

            Assert.False(result.IsSuccess);
            Assert.Equal("The user is duplicated.", result.Message);
        }
    }
}
