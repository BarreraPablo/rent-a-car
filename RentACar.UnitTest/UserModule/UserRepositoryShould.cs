using Microsoft.EntityFrameworkCore;
using Moq;
using RentACar.Core.Entities;
using RentACar.Core.Enumerations;
using RentACar.Core.Exceptions;
using RentACar.Infrastructure.Data;
using RentACar.Infrastructure.Repositories;
using System;
using System.Linq;
using Xunit;

namespace RentACar.UnitTest
{
    [Collection("Repository tests")]
    public class UserRepositoryShould
    {
        protected DbContextOptions<RentACarContext> ContextOptions { get; }
        public UserRepositoryShould()
        {
            ContextOptions = new DbContextOptionsBuilder<RentACarContext>()
            .UseInMemoryDatabase("RentACar")
            .Options;

            Seed();
        }

        private void Seed()
        {
            var user1 = new User() { Username = "Juan", Role = RoleType.Consumer, Password = "test", EmailAddress = "juan@gmail.com", CreatedAt = DateTime.Parse("2021-04-27") };
            var user2 = new User() { Username = "Cristian", Role = RoleType.Consumer, Password = "test", EmailAddress = "cristian@gmail.com", CreatedAt = DateTime.Parse("2021-04-29") };
            var user3 = new User() { Username = "Gustavo", Role = RoleType.Consumer, Password = "test", EmailAddress = "gustavo@gmail.com", CreatedAt = DateTime.Parse("2021-04-30") };

            using (var context = new RentACarContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var unitOfWork = new UnitOfWork(context);

                unitOfWork.UserRepository.Add(user1);
                unitOfWork.UserRepository.Add(user2);
                unitOfWork.UserRepository.Add(user3);

                unitOfWork.SaveChanges();
            }
        }

        [Fact]
        public async void Add_UserObjectIsOk_UserIsRegistered()
        {
            using (var context = new RentACarContext(ContextOptions))
            {
                var unitOfWork = new UnitOfWork(context);

                User user = new User()
                {
                    Username = "Test1",
                    EmailAddress = "contact@gmail.com",
                    Role = 0,
                    Password = "secret"
                };

                await unitOfWork.UserRepository.Add(user);
                unitOfWork.SaveChanges();


                Assert.Equal(4, user.Id); // After the insert the entity must have id > 0
            }

            using (var context = new RentACarContext(ContextOptions))
            {
                var user = context.Set<User>().FirstOrDefault(u => u.Username == "Test1");

                Assert.Equal(4, user.Id);
                Assert.Equal("Test1", user.Username);
                Assert.Equal("contact@gmail.com", user.EmailAddress);
                Assert.Equal(RoleType.Administrator, user.Role);
                Assert.Equal("secret", user.Password);
                Assert.Equal(DateTime.Today.Date, user.CreatedAt.Date);
            }
        }

        [Fact]
        public async void Add_UserObjectIsNull_ThrowException()
        {
            using (var context = new RentACarContext(ContextOptions))
            {
                var unitOfWork = new UnitOfWork(context);

                await Assert.ThrowsAsync<ArgumentNotDefinedException>(() => unitOfWork.UserRepository.Add(null));
            }
        }

        [Fact]
        public async void GetByUsername_UserNameIsNull_ThrowException()
        {
            using (var context = new RentACarContext(ContextOptions))
            {
                var userRepository = new UserRepository(context);

                await Assert.ThrowsAsync<ArgumentNotDefinedException>(() => userRepository.GetByUsername(null));
                await Assert.ThrowsAsync<ArgumentNotDefinedException>(() => userRepository.GetByUsername(""));
                await Assert.ThrowsAsync<ArgumentNotDefinedException>(() => userRepository.GetByUsername("   "));
            }
        }

        [Fact]
        public async void GetByUsername_UsernameIsOk_ReturnUser()
        {
            using (var context = new RentACarContext(ContextOptions))
            {
                var unitOfWork = new UnitOfWork(context);

                var user = await unitOfWork.UserRepository.GetByUsername("Juan");

                Assert.Equal("Juan", user.Username);
                Assert.Equal("test", user.Password);
                Assert.Equal("juan@gmail.com", user.EmailAddress);
            }
        }

        [Fact]
        public async void GetByUsername_UsernameNotExists_ReturnNull()
        {
            using (var context = new RentACarContext(ContextOptions))
            {
                var unitOfWork = new UnitOfWork(context);

                var user = await unitOfWork.UserRepository.GetByUsername("Esteban");

                Assert.Null(user);
            }
        }

        [Fact]
        public async void GetByEmail_EmailIsNull_ThrowException()
        {
            using (var context = new RentACarContext(ContextOptions))
            {
                var unitOfWork = new UnitOfWork(context);

                await Assert.ThrowsAsync<ArgumentNotDefinedException>(() => unitOfWork.UserRepository.GetByUsername(null));
                await Assert.ThrowsAsync<ArgumentNotDefinedException>(() => unitOfWork.UserRepository.GetByUsername(""));
                await Assert.ThrowsAsync<ArgumentNotDefinedException>(() => unitOfWork.UserRepository.GetByUsername("   "));
            }
        }

        [Fact]
        public async void GetByEmail_EmailIsOk_ReturnUser()
        {
            using (var context = new RentACarContext(ContextOptions))
            {
                var unitOfWork = new UnitOfWork(context);

                var user = await unitOfWork.UserRepository.GetByEmail("juan@gmail.com");

                Assert.True(user != null);
                Assert.Equal("Juan", user.Username);
            }
        }

        [Fact]
        public async void GetByEmail_EmailNotExists_ReturnNull()
        {
            using (var context = new RentACarContext(ContextOptions))
            {
                var unitOfWork = new UnitOfWork(context);

                var user = await unitOfWork.UserRepository.GetByEmail("test@hotmail.com");

                Assert.Null(user);
            }
        }

        [Fact]
        public async void Update_UserIsOk_UserIsUpdated()
        {
            using (var context = new RentACarContext(ContextOptions))
            {
                var unitOfWork = new UnitOfWork(context);

                var user = await unitOfWork.UserRepository.GetByUsername("Juan");


                user.Id = 1;
                user.Username = "Martin";
                user.EmailAddress = "contact@gmail.com";
                user.Role = 0;
                user.Password = "secret";

                await unitOfWork.UserRepository.GetAndUpdate(user);
                unitOfWork.SaveChanges();
            }

            using (var context = new RentACarContext(ContextOptions))
            {
                var user = context.Set<User>().FirstOrDefault(u => u.Id == 1);

                Assert.Equal(1, user.Id);
                Assert.Equal("Martin", user.Username);
                Assert.Equal("contact@gmail.com", user.EmailAddress);
                Assert.Equal(RoleType.Administrator, user.Role);
                Assert.Equal("secret", user.Password);
                Assert.Equal(DateTime.Today.Date, user.ModifiedAt?.Date);
            }
        }
    }
}
