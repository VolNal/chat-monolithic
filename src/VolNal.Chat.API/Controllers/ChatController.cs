using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VolNal.Chat.API.Controllers.Base;
using VolNal.Chat.Api.DAL.Models;
using VolNal.Chat.Api.DAL.Repositories.Interfaces;
using VolNal.Chat.API.HttpModels;
using VolNal.Chat.Api.HttpModels.Message;
using VolNal.Chat.Api.Services;

namespace VolNal.Chat.API.Controllers;

[ApiController]
[Route("/api/chat")]
public class ChatController : ControllerBaseCastom
{
    private readonly IMapper _mapper;
    private readonly ChatHub _hub;
    private readonly IChatRepository _chatRepository;
    private readonly IMessageRepository _messageRepository;

    public ChatController(IMapper mapper, ChatHub hub, IChatRepository chatRepository,
        IMessageRepository messageRepository)
    {
        _mapper = mapper;
        _hub = hub;

        _chatRepository = chatRepository;
        _messageRepository = messageRepository;
    }

    /// <summary>
    /// Получение списка всех чатов пользователя.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<GetChatsViewModel>> GetChats()
    {
        var user = await GetUserAsync();

        var chats = await _chatRepository.GetAllAsync(user);
        return Ok(chats);
    }

    /// <summary>
    /// Создание чата.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post(PostCreateChatViewModel model)
    {
        var chat = _mapper.Map<ChatDto>(model);

        await _chatRepository.CreateAsync(chat);
        return Ok();
    }

    /// <summary>
    /// Получение сообщений.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet("/api/chat/message")]
    public async Task<ActionResult<List<MessageDto>>> GetMessages(GetMessagesViewModel model)
    {
        var chat = _mapper.Map<ChatDto>(model);

        var messages = await _messageRepository.GetAsync(chat);
        return Ok(messages);
    }

    /// <summary>
    /// Отправка сообщения.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("/api/chat/message")]
    public async Task<IActionResult> PostMessages(PostMessageViewModel model)
    {
        var user = await GetUserAsync();
        if (model.Email != user.Email)
            return Error("Пользователь не совпадает", model);

        var message = _mapper.Map<MessageDto>(model);
        await _messageRepository.CreateAsync(message);

        //TODO: Передать дополнительно модель пользователя. 
        await _hub.SendMessageToChatAsync(message.Id.ToString(), message.Content);
        return Ok();
    }
}