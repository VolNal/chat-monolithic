using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VolNal.Chat.API.HttpModels;

namespace VolNal.Chat.API.Controllers;

[ApiController]
[Route("/api/chat")]
public class ChatController : ControllerBase
{
    private readonly IMapper _mapper;

    public ChatController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<GetChatsViewModel>> Get()
    {
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult> Get(PostCreateChatViewModel model)
    {
        return Ok();
    }
}