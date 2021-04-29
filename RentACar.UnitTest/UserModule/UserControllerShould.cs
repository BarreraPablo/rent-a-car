using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using RentACar.Api.Controllers;
using Moq;
using RentACar.Core.Interfaces;
using RentACar.Infrastructure.Interfaces;
using RentACar.Core.DTOs.UserDTOs;
using RentACar.Core.Enumerations;
using RentACar.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using RentACar.Infrastructure.Mappings;

namespace RentACar.UnitTest
{
    public class UserControllerShould 
    {
        [Fact]
        public async void Register_UserCreateDtoIsOk_HashPasswordAndRegisterUser()
        {
            var userService = new Mock<IUserService>();
            var passwordService = new Mock<IPasswordService>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutomapperProfile());
            });
            var mapper = config.CreateMapper();

            var userController = new UserController(mapper, userService.Object, passwordService.Object);

            var userCreateDto = new UserCreateDto
            {
                EmailAddress = "pablo@gmail.com",
                Password = "Password123.",
                Role = RoleType.Administrator,
                Username = "Pablo"
            };

            var result = await userController.Register(userCreateDto);

            passwordService.Verify(p => p.Hash(It.IsAny<string>()), Times.Once);
            userService.Verify(u => u.RegisterUser(It.IsAny<User>()), Times.Once);

            Assert.IsType<NoContentResult>(result);
        }



    }
}
