using System.Security.Claims;
using Application.DTO;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.ExceptionFilters;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [UserExceptionFilter]
    public class UserController(UserService service) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser(LoginDTO loginDto)
        {
            return Ok(await service.LoginUser(loginDto));
        }
        
        [HttpPost("Create")]    
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateUser(CreateUserDTO userDto)
        {
            await service.CreateUser(userDto, User.Claims.First(p => p.Type == ClaimTypes.Name).Value);
            
            return Ok();
        }

        [HttpPut("Data")]
        [Authorize]
        public async Task<IActionResult> UpdateUserData(UpdateUserDTO userDto)
        {
            var login = User.Claims.First(p => p.Type == ClaimTypes.Name).Value;

            var role = User.Claims.First(p => p.Type == ClaimTypes.Role).Value;
            
            if (login != userDto.Login || role != "admin")
                return Forbid();

            await service.UpdateData(userDto, User.Claims.First(p => p.Type == ClaimTypes.Name).Value);
                    
            return Ok();
        }

        [HttpPut("Password")]
        [Authorize]
        public async Task<IActionResult> UpdateUserPassword(ChangePasswordDTO passwordDto)
        {
            var login = User.Claims.First(p => p.Type == ClaimTypes.Name).Value;

            var role = User.Claims.First(p => p.Type == ClaimTypes.Role).Value;

            if (login != passwordDto.Login || role != "admin")
                return Forbid();

            await service.ChangePassword(passwordDto, login);

            return Ok();
        }

        [HttpPut("Login")]
        [Authorize]
        public async Task<IActionResult> ChangeUserLogin(string currentLogin, string newLogin)
        {
            var login = User.Claims.First(p => p.Type == ClaimTypes.Name).Value;

            var role = User.Claims.First(p => p.Type == ClaimTypes.Role).Value;
            
            if (login != currentLogin && role != "admin")
                return Forbid();

            await service.ChangeLogin(currentLogin, newLogin, login);

            return Ok();
        }

        [HttpGet("All")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllActiveUsers()
        {
            return Ok(await service.GetAllActiveUsers());
        }

        [HttpGet("Login")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetUserByLogin(string login)
        {
            return Ok(await service.GetUserByLogin(login));
        }

        [HttpGet("Self")]
        [Authorize]
        public async Task<IActionResult> GetSelfData(LoginDTO loginDto)
        {
            if (loginDto.Login != User.Claims.First(p => p.Type == ClaimTypes.Name).Value)
                return Forbid();
            
            return Ok(await service.GetSelfData(loginDto.Login, loginDto.Password));
        }

        [HttpGet("Age")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetUsersByAge(int minAge)
        {
            return Ok(await service.GetUsersByAge(minAge));
        }

        [HttpDelete]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUser(string login, bool isSoft)
        {
            await service.DeleteUser(login, 
                isSoft, 
                User.Claims.First(p => p.Type == ClaimTypes.Name).Value);

            return Ok();
        }

        [HttpPut("Restore")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> RestoreUser(string login)
        {
            await service.RestoreUser(login);

            return Ok();
        }
    }
}
