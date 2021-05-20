using Moq;
using RentACar.Core.DTOs.UserDTOs;
using RentACar.Core.Entities;
using RentACar.Core.Exceptions;
using RentACar.Core.Interfaces;
using RentACar.Core.Services;
using Xunit;

namespace RentACar.UnitTest
{
    public class UserServiceShould
    {
        [Fact]
        public async void RegisterUser_UserIsNull_ThrowException()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var userService = new UserService(unitOfWorkMock.Object);

            await Assert.ThrowsAsync<ArgumentNotDefinedException>(() => userService.RegisterUser(null));
        }

        [Fact]
        public async void RegisterUser_UsernameIsTaken_ThrowException() {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var userService = new UserService(unitOfWorkMock.Object);

            unitOfWorkMock.Setup(u => u.UserRepository.GetByUsername(It.IsAny<string>())).ReturnsAsync(new User() { });

            await Assert.ThrowsAsync<BussinessException>(() => userService.RegisterUser(new User() { }));
        }

        [Fact]
        public async void RegisterUser_EmailIsAlreadyRegistered_ThrowException()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var userService = new UserService(unitOfWorkMock.Object);

            unitOfWorkMock.Setup(u => u.UserRepository.GetByEmail(It.IsAny<string>())).ReturnsAsync(new User() { });

            await Assert.ThrowsAsync<BussinessException>(() => userService.RegisterUser(new User() { }));
        }

        [Fact]
        public async void RegisterUser_EmailIsNotRegisteredShouldCallTheAddMethodOnce()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var userService = new UserService(unitOfWorkMock.Object);

            unitOfWorkMock.Setup(u => u.UserRepository.GetByUsername(It.IsAny<string>())).ReturnsAsync((User)null);
            unitOfWorkMock.Setup(u => u.UserRepository.GetByEmail(It.IsAny<string>())).ReturnsAsync((User)null);

            await userService.RegisterUser(new User() { Id = 1, Username = "Test", Role = 0, Password = "123" });

            unitOfWorkMock.Verify(u => u.UserRepository.Add(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async void GetByUsername_UsernameIsOk_CallGetByUsernameMethodOnce()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var userService = new UserService(unitOfWorkMock.Object);
            unitOfWorkMock.Setup(u => u.UserRepository.GetByUsername(It.IsAny<string>())).ReturnsAsync(new User());

            await userService.GetByUsername(new UserLoginDto() { });

            unitOfWorkMock.Verify(u => u.UserRepository.GetByUsername(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async void GetByUsername_UserLoginIsNull_ThrowException()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var userService = new UserService(unitOfWorkMock.Object);

            await Assert.ThrowsAsync<ArgumentNotDefinedException>(() => userService.GetByUsername(null));
        }
    }
}
