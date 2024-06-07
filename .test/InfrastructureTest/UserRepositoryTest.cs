using Domain.Entities;

namespace InfrastructureTest;

public class UserRepositoryTest
{
    private TestHelper testHelper = new TestHelper();
    
    [Fact]
    public async void GetAllUsers_ReturnsAllUsers()
    {
        var userRepository = testHelper.UnitOfWork.UserRepository;
        
        // Arrange
        var users = new List<User>
        {
            new(){ Id = Guid.NewGuid(), Name = "User1" },
            new() { Id = Guid.NewGuid(), Name = "User2" }
        };

        foreach (var user in users)
        {
            await userRepository.Add(user);
        }

        await testHelper.UnitOfWork.SaveChanges();

        // Act
        var result = await userRepository.GetAll();

        // Assert
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async void GetUserById_ReturnsCorrectUser()
    {
        var userRepository = testHelper.UnitOfWork.UserRepository;
        
        // Arrange
        var users = new List<User>
        {
            new() { Id = Guid.NewGuid(), Name = "User1" },
            new() { Id = Guid.NewGuid(), Name = "User2" }
        };

        foreach (var user in users)
        {
            await userRepository.Add(user);
        }

        await testHelper.UnitOfWork.SaveChanges();
        
        // Act
        var result = await userRepository.GetById(users.First().Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("User1", result.Name);
    }

    [Fact]
    public async void GetUserById_ReturnsNullForNonExistingUser()
    {
        var userRepository = testHelper.UnitOfWork.UserRepository;
        
        // Arrange
        var users = new List<User>
        {
            new() { Id = Guid.NewGuid(), Name = "User1" },
            new() { Id = Guid.NewGuid(), Name = "User2" }
        };

        foreach (var user in users)
        {
            await userRepository.Add(user);
        }

        await testHelper.UnitOfWork.SaveChanges();
        
        // Act
        var result = await userRepository.GetById(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }
}