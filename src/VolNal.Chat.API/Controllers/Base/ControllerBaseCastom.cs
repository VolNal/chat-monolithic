using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using VolNal.Chat.Api.DAL.Models;
using VolNal.Chat.Api.DAL.Repositories.Interfaces;

namespace VolNal.Chat.API.Controllers.Base;

public class ControllerBaseCastom : ControllerBase
{
    public readonly IUserRepository _userRepository;

    public ControllerBaseCastom(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    protected IActionResult Error<T>(string detail, T model)
    {
        var error = new ErrorViewModel<T>()
        {
            Detail = detail,
            ViewModel = model
        };

        return BadRequest(error);
    }
    
    protected IActionResult Error<T>(ErrorViewModel<T> error)
    {
        return BadRequest(error);
    }
    
    protected IResult ErrorResult<T>(string detail, T model)
    {
        var error = new ErrorViewModel<T>()
        {
            Detail = detail,
            ViewModel = model
        };

        return Results.BadRequest(error);
    }
    
    protected IResult ErrorResult<T>(ErrorViewModel<T> error)
    {
        return Results.BadRequest(error);
    }
    
    /// <summary>
    /// Возвращает текущего пользователя, читая email из токена. 
    /// </summary>
    /// <returns></returns>
    protected async Task<UserDto> GetUserAsync()
    {
        var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(token);

        var tokenUserEmail = jwtToken.Claims.First(c => c.Type == ClaimTypes.Email).Value;

        var currentUser = new UserDto
        {
            Email = tokenUserEmail
        };

        var user = await _userRepository.GetAsync(currentUser);
        return user;
    }
}