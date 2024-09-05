namespace UserService.Tests
{
    public class UserService_IsValidUser
    {
        private readonly Mock<IUserRepository> mockRepository;
        private readonly UserService userService;

        public UserService_IsValidUser() {
            mockRepository = new Mock<IUserRepository>();
            userService = new UserService(mockRepository.Object);
        }

        [Fact]
        public void GetUserById_WhenUserExists()
        {
            //Arrange            
            mockRepository.Setup(repo => repo.GetUserById(1)).Returns(new User { Id = 1, Name = "Test"});

            //SetupSequence
            //mockRepository.SetupSequence(repo => repo.GetUserById(It.IsAny<int>()))
            //.Returns(new User { Id = 1, Name = "John Doe" })
            //.Returns(new User { Id = 2, Name = "Jane Doe" });

            //var userService = new UserService(mockRepository.Object);

            //Act
            var result = userService.GetUserById(1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.Id, 1);
            Assert.Equal(result.Name, "Test");
        }

        [Fact]
        public void CreateUser_ShouldCallAddUserOnce()
        {
            //Arrange
            //var userService = new UserService(mockRepository.Object);
            var newUser = new User { Id = 2, Name = "Test2" };

            //Act
            userService.CreateUser(newUser);

            //Assert
            mockRepository.Verify(repo => repo.AddUser(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public void GetAllUsers_ShouldReturnListOfUsers()
        {
            //Arrange
            mockRepository.Setup(repo => repo.GetAllUsers()).Returns(new List<User>() { 
                new User {Id=3, Name = "Test3" },
                new User {Id=4, Name = "Test4" }
            });

            //Act
            var result = userService.GetAllUsers();

            //Assert
            Assert.Equal(2, result.Count());
        }
    }
}