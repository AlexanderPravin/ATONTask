﻿
using Application.Validators;

namespace Application.Services;

public class UserService(UnitOfWork unitOfWork, JwtTokenHelper tokenHelper, UserValidator userValidator)
{
    public async Task<string> LoginUser(LoginDTO loginDto)
    {
        var user = await unitOfWork.UserRepository.GetBy(p =>
            p.Login == loginDto.Login &&
            p.Password == loginDto.Password) 
                   ?? throw new NullEntityException("Can`t find user with given login and password");
        
        var role = user.IsAdmin ? "admin" : "user";

        return tokenHelper.CreateToken(role, loginDto.Login);
    }

    public async Task CreateUser(CreateUserDTO userDto, string createdBy)
    {
        var user = await unitOfWork.UserRepository.GetBy(p => p.Login == userDto.Login);

        if (user is not null)
            throw new DuplicateException($"User with login: {userDto.Login} already exist");

        user = userDto.Adapt<User>();

        user.CreatedBy = createdBy;
        
        user.CreatedOn = DateTime.UtcNow;

        var validationResult = await userValidator.ValidateAsync(user);

        if (!validationResult.IsValid)
            throw new ArgumentException(String.Join(", ", validationResult.Errors.Select(p => p.ErrorMessage)));
        
        await unitOfWork.UserRepository.Add(user);

        await unitOfWork.SaveChanges();
    }

    public async Task UpdateData(UpdateUserDTO userDto, string modifiedBy)
    {
        var userToChange = await CheckRights(userDto.Login, modifiedBy);
        
        userDto.Adapt(userToChange);

        userToChange.ModifiedBy = modifiedBy;
        
        userToChange.ModifiedOn = DateTime.UtcNow;
        
        var validationResult = await userValidator.ValidateAsync(userToChange);

        if (!validationResult.IsValid)
            throw new ArgumentException(String.Join(", ", validationResult.Errors.Select(p => p.ErrorMessage)));
        
        unitOfWork.UserRepository.Update(userToChange);
        
        await unitOfWork.SaveChanges();
    }

    public async Task ChangePassword(ChangePasswordDTO passwordDto, string modifiedBy)
    {
        var userToChange = await CheckRights(passwordDto.Login, modifiedBy);

        userToChange.Password = passwordDto.Password;

        userToChange.ModifiedBy = modifiedBy;
        
        userToChange.ModifiedOn = DateTime.UtcNow;

        var validationResult = await userValidator.ValidateAsync(userToChange);

        if (!validationResult.IsValid)
            throw new ArgumentException(String.Join(", ", validationResult.Errors.Select(p => p.ErrorMessage)));
        
        unitOfWork.UserRepository.Update(userToChange);
        
        await unitOfWork.SaveChanges();
    }

    public async Task ChangeLogin(string currentLogin, string newLogin, string modifiedBy)
    {
        var userToChange = await CheckRights(currentLogin, modifiedBy);

        var userToCheck = await unitOfWork.UserRepository.GetBy(p => p.Login == newLogin);

        if (userToCheck is not null)
            throw new DuplicateException($"User with given login {newLogin} already exists");

        userToChange.Login = newLogin;
        
        var validationResult = await userValidator.ValidateAsync(userToChange);

        if (!validationResult.IsValid)
            throw new ArgumentException(String.Join(", ", validationResult.Errors.Select(p => p.ErrorMessage)));
        
        unitOfWork.UserRepository.Update(userToChange);

        await unitOfWork.SaveChanges();
    }

    public async Task<IEnumerable<ResponseUserDTO>> GetAllActiveUsers()
    {
        var activeUsers = await unitOfWork.UserRepository.GetCollectionBy(p => p.RevokedOn == null);

        return activeUsers.OrderBy(p => p.CreatedOn).Adapt<IEnumerable<ResponseUserDTO>>();
    }

    public async Task<ResponseUserDTO> GetUserByLogin(string login)
    {
        var user = await unitOfWork.UserRepository.GetBy(p => p.Login == login)
                   ?? throw new NullEntityException($"Can`t find user with login: {login}");

        return user.Adapt<ResponseUserDTO>();
    }

    public async Task<SelfUserDTO> GetSelfData(string login, string password)
    {
        var user = await unitOfWork.UserRepository.GetBy(p =>
                       p.Login == login &&
                       p.Password == password) 
                   ?? throw new NullEntityException("Can`t find user with given login and password");

        var dto = user.Adapt<SelfUserDTO>();

        dto.IsRevoked = user.RevokedOn is null;

        return dto;
    }

    public async Task<IEnumerable<ResponseUserDTO>> GetUsersByAge(int minAge)
    {
        var users = await unitOfWork.UserRepository.GetCollectionBy(p =>
            p.Birthday != null && 
            DateTime.UtcNow.Year - p.Birthday.Value.Year > minAge);

        return users.Adapt<IEnumerable<ResponseUserDTO>>();
    }

    public async Task DeleteUser(string login, bool isSoft, string revokedBy)
    {
        var user = await unitOfWork.UserRepository.GetBy(p => p.Login == login)
                   ?? throw new NullEntityException($"Can`t find user by given login: {login}");

        if (isSoft)
        {
            user.RevokedOn = DateTime.UtcNow;
            user.RevokedBy = revokedBy;
            
            unitOfWork.UserRepository.Update(user);
        }
        else
            unitOfWork.UserRepository.Delete(user);

        await unitOfWork.SaveChanges();
    }

    public async Task RestoreUser(string login)
    {
        var user = await unitOfWork.UserRepository.GetBy(p => p.Login == login)
                   ?? throw new NullEntityException($"Can`t find user with given login: {login}");

        user.RevokedBy = null;
        user.RevokedOn = null;

        unitOfWork.UserRepository.Update(user);
        
        await unitOfWork.SaveChanges();
    }
    
    private async Task<User> CheckRights(string login, string modifiedBy)
    {
        var userToChange = await unitOfWork.UserRepository.GetBy(p => p.Login == login)
                           ?? throw new NullEntityException($"Can`t find user with given login: {login}");

        if (login == modifiedBy && userToChange.RevokedOn is not null)
            throw new AccessException("Access denied");

        return userToChange;
    }
}