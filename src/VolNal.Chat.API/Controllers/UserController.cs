using System.Text.RegularExpressions;
using AutoMapper;
using JwtIdentity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VolNal.Chat.API.Controllers.Base;
using VolNal.Chat.Api.DAL.Models;
using VolNal.Chat.Api.DAL.Repositories.Implementation;
using VolNal.Chat.Api.DAL.Repositories.Interfaces;
using VolNal.Chat.API.HttpModels;

namespace VolNal.Chat.API.Controllers;

[AllowAnonymous]
[ApiController]
[Route("/api/user")]
public class UserController : ControllerBaseCastom
{
    private readonly IMapper _mapper;
    private readonly PasswordManager _passwordManager;
    private readonly JwtTokenService _jwtTokenService;

    public UserController(IMapper mapper,
        IUserRepository userRepository,
        PasswordManager passwordManager,
        JwtTokenService jwtTokenService) : base(userRepository)
    {
        _mapper = mapper;

        _passwordManager = passwordManager;
        _jwtTokenService = jwtTokenService;
    }

    /// <summary>
    /// Create user.
    /// </summary>
    /// <param name="model"></param>
    /// <returns>Status code 200.</returns>
    /// <response code="200">User and meals is created.</response>
    /// <response code="409">User with this email already exists.</response>
    [HttpPost("/api/users/Authorize")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorViewModel<AuthorizeUserViewModel>))]
    public async Task<IActionResult> Authorize(AuthorizeUserViewModel model)
    {
        if (!IsValidEmail(model, out var errorEmail))
            return Error(errorEmail);

        if (!IsValidPassword(model, out var errorPassword))
            return Error(errorPassword);

        var mapUser = _mapper.Map<UserDto>(model);

        var user = await _userRepository.GetAsync(mapUser);
        if (user != null)
            return Error("User with this email already exists.", model);

        var hash = _passwordManager.HashPassword(mapUser.Password);
        mapUser.Password = hash;

        await _userRepository.CreateAsync(mapUser);
        return Ok();
    }

    /// <summary>
    /// Get authentication token.
    /// </summary>
    /// <param name="model"></param>
    /// <returns>Response json.</returns>
    /// <response code="200">Authentication successful.</response>
    /// <response code="400">User entered incorrect data.</response>
    [HttpPost("/api/users/Authentication")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthenticationUserViewModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorViewModel<AuthenticationUserViewModel>))]
    public async Task<IResult> Authentication([FromBody] AuthenticationUserViewModel model)
    {
        if (!IsValidEmail(model, out var errorEmail))
            return ErrorResult(errorEmail);

        if (!IsValidPassword(model, out var errorPassword))
            return ErrorResult(errorPassword);

        var user = _mapper.Map<UserDto>(model);
        var response = await _userRepository.GetAsync(user);

        if (response == null)
        {
            return ErrorResult("Incorrectly entered data.", model);
        }

        if (_passwordManager.VerifyPassword(response.Password, user.Password) == PasswordVerificationResult.Failed)
        {
            return ErrorResult("Incorrectly entered data.", model);
        }

        var token = _jwtTokenService.GetToken(response);
        return Results.Json(token, statusCode: StatusCodes.Status200OK);
    }

    //TODO: вынести? в класс валидации модели контроллера.
    private bool IsValidEmail(UserViewModelBase model, out ErrorViewModel<UserViewModelBase>? error)
    {
        var pattern = "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}";
        var isMatch = Regex.Match(model.Email, pattern, RegexOptions.IgnoreCase);

        if (!isMatch.Success)
        {
            error = new ErrorViewModel<UserViewModelBase>()
            {
                Detail = "Wrong email.",
                ViewModel = model
            };

            return false;
        }

        error = null;
        return true;
    }

    //TODO: вынести? в класс валидации модели контроллера.
    private bool IsValidPassword(UserViewModelBase model, out ErrorViewModel<UserViewModelBase>? error)
    {
        var pattern = @"(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z !#$%&'()*+,-.\/:;<=>?@\[\\\]^_`{|}~]{8,255}";
        var isMatch = Regex.Match(model.Password, pattern, RegexOptions.None);

        if (!isMatch.Success)
        {
            error = new ErrorViewModel<UserViewModelBase>()
            {
                Detail = "Password must have an uppercase letter, a number, and be longer than 7.",
                ViewModel = model
            };

            return false;
        }

        error = null;
        return true;
    }
}