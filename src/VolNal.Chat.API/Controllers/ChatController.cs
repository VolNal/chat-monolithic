using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VolNal.Chat.API.Controllers.Base;
using VolNal.Chat.Api.DAL.Models;
using VolNal.Chat.Api.DAL.Repositories.Implementation;
using VolNal.Chat.Api.DAL.Repositories.Interfaces;
using VolNal.Chat.API.HttpModels;

namespace VolNal.Chat.API.Controllers;

[Authorize]
[ApiController]
[Route("/api/chat")]
public class ChatController : ControllerBaseCastom
{
    private readonly IMapper _mapper;
    private readonly IChatRepository _chatRepository;

    public ChatController(IMapper mapper, IUserRepository userRepository, IChatRepository chatRepository)
        : base(userRepository)
    {
        _mapper = mapper;

        _chatRepository = chatRepository;
    }

    [HttpGet]
    public async Task<ActionResult<GetChatsViewModel>> Get()
    {
        var user = await GetUserAsync();

        var chats= await _chatRepository.GetAsync(user);
        return Ok(chats);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody]PostCreateChatViewModel model)
    {
        var user = await GetUserAsync();
        var chat = _mapper.Map<ChatDto>(model);
        chat.CreatorId = user.Id;

        await _chatRepository.CreateAsync(chat);
        return Ok();
    }
}