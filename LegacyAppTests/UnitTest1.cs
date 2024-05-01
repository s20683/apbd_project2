using System;
using LegacyApp;
using Xunit;

namespace LegacyAppTests
{
    public class UserServiceTests
    {
        [Fact]
        public void AddUser_ValidData_ReturnsTrue()
        {
            // Arrange
            var clientRepository = new MockClientRepository();
            var userCreditService = new MockUserCreditService();
            var userService = new UserService(clientRepository, userCreditService);

            // Act
            var result = userService.AddUser("John", "Doe", "john@example.com", new DateTime(1990, 1, 1), 1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AddUser_InvalidData_ReturnsFalse()
        {
            // Arrange
            var clientRepository = new MockClientRepository();
            var userCreditService = new MockUserCreditService();
            var userService = new UserService(clientRepository, userCreditService);

            // Act
            var result = userService.AddUser("", "", "invalid-email", new DateTime(2005, 1, 1), 1);

            // Assert
            Assert.False(result);
        }
    }

    public class MockClientRepository : IClientRepository
    {
        public Client GetById(int clientId)
        {
            return new Client(2, "Pala", "Warszawa, Koszykowa 12", "pala@gmail.pl",  "RegularClient");
        }
    }

    public class MockUserCreditService : IUserCreditService
    {
        public int GetCreditLimit(string lastName)
        {
            return 1000;
        }
    }
}