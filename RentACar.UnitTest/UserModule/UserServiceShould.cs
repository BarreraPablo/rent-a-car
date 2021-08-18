using Telerik.JustMock;
using RentACar.Core.DTOs.UserDTOs;
using RentACar.Core.Entities;
using RentACar.Core.Exceptions;
using RentACar.Core.Interfaces;
using RentACar.Core.Services;
using Xunit;
using System.Threading.Tasks;

namespace RentACar.UnitTest
{
    public class UserServiceShould
    {
        [Fact]
        public async void RegisterUser_UserIsNull_ThrowException()
        {
            var unitOfWorkMock = Mock.Create<IUnitOfWork>();
            var userService = new UserService(unitOfWorkMock);

            await Assert.ThrowsAsync<ArgumentNotDefinedException>(() => userService.RegisterUser(null));
        }

        [Fact]
        public async void RegisterUser_UsernameIsTaken_ThrowException() {
            var unitOfWorkMock = Mock.Create<IUnitOfWork>();
            var userService = new UserService(unitOfWorkMock);

            Mock.Arrange(() => unitOfWorkMock.UserRepository.GetByUsername(Arg.AnyString)).TaskResult(new User() { });
            //unitOfWorkMock.Setup(u => u.UserRepository.GetByUsername(It.IsAny<string>())).ReturnsAsync(new User() { });

            await Assert.ThrowsAsync<BussinessException>(() => userService.RegisterUser(new User() { }));
        }

        [Fact]
        public async void RegisterUser_EmailIsAlreadyRegistered_ThrowException()
        {
            var unitOfWorkMock = Mock.Create<IUnitOfWork>();
            var userService = new UserService(unitOfWorkMock);

            Mock.Arrange(() => unitOfWorkMock.UserRepository.GetByEmail(Arg.AnyString)).TaskResult(new User());
            //unitOfWorkMock.Setup(u => u.UserRepository.GetByEmail(It.IsAny<string>())).ReturnsAsync(new User() { });

            await Assert.ThrowsAsync<BussinessException>(() => userService.RegisterUser(new User() { EmailAddress = "emailaddresstest@gmail.com" }));
        }

        [Fact]
        public async void RegisterUser_EmailIsNotRegisteredShouldCallTheAddMethodOnce()
        {
            var unitOfWorkMock = Mock.Create<IUnitOfWork>();
            var userService = new UserService(unitOfWorkMock);

            Mock.Arrange(() => unitOfWorkMock.UserRepository.GetByUsername(Arg.AnyString)).TaskResult(null);
            Mock.Arrange(() => unitOfWorkMock.UserRepository.GetByEmail(Arg.AnyString)).TaskResult(null);

            await userService.RegisterUser(new User() { Id = 1, Username = "Test", Role = 0, Password = "123", EmailAddress = "test" });

            Mock.Assert(() => unitOfWorkMock.UserRepository.Add(Arg.IsAny<User>()), Occurs.Once());
            //unitOfWorkMock.Verify(u => u.UserRepository.Add(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async void GetByUsername_UsernameIsOk_CallGetByUsernameMethodOnce()
        {
            var unitOfWorkMock = Mock.Create<IUnitOfWork>();
            var userService = new UserService(unitOfWorkMock);
            Mock.Arrange(() => unitOfWorkMock.UserRepository.GetByUsername(Arg.AnyString)).TaskResult(new User());
            //unitOfWorkMock.Setup(u => u.UserRepository.GetByUsername(It.IsAny<string>())).ReturnsAsync(new User());

            await userService.GetByUsername(new UserLoginReqDto() { });

            Mock.Assert(() => unitOfWorkMock.UserRepository.GetByUsername(Arg.AnyString), Occurs.Once());
            //unitOfWorkMock.Verify(u => u.UserRepository.GetByUsername(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async void GetByUsername_UserLoginIsNull_ThrowException()
        {
            var unitOfWorkMock = Mock.Create<IUnitOfWork>();
            var userService = new UserService(unitOfWorkMock);

            await Assert.ThrowsAsync<ArgumentNotDefinedException>(() => userService.GetByUsername(null));
        }
    }
}
