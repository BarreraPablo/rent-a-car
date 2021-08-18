using AutoMapper;
using Telerik.JustMock;
using Xunit;
using RentACar.Api.Controllers;
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
            var userService = Mock.Create<IUserService>();
            var passwordService = Mock.Create<IPasswordService>();
            var cryptographyService = Mock.Create<ICryptographyService>();
            var emailService = Mock.Create<IEmailService>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutomapperProfile());
            });
            var mapper = config.CreateMapper();

            var userController = new UserController(mapper, userService, passwordService, cryptographyService, emailService);

            var userCreateDto = new UserCreateDto
            {
                EmailAddress = "pablo@gmail.com",
                Password = "Password123.",
                Role = RoleType.Administrator,
                Username = "Pablo"
            };

            var result = await userController.Register(userCreateDto);

            Mock.Assert(() => passwordService.Hash(Arg.AnyString), Occurs.Once());
            Mock.Assert(() => userService.RegisterUser(Arg.IsAny<User>()), Occurs.Once());

            Assert.IsType<NoContentResult>(result);
        }



    }
}
