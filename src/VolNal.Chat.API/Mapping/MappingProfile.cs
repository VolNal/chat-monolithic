using AutoMapper;
using VolNal.Chat.API.Controllers;
using VolNal.Chat.Api.DAL.Models;
using VolNal.Chat.Api.Mapping.Interfaces;

namespace VolNal.Chat.Api.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //HttpModels => Commands
        CreateMap<AuthenticationUserViewModel, UserDto>();
        //CreateMap<IGetChatsViewModel, GetChatsCommand>().ReverseMap();
        //CreateMap<IChatViewModel, ChatDto>().ReverseMap();
        //CreateMap<IPostCreateChatViewModel, RequestCreateChatCommand>();

        //CreateMap<RequestCreateChatCommand, ResponseCreateChatCommand>().ReverseMap();

        //Commands => Dto
        //CreateMap<GetChatsCommand, UserDto>().ReverseMap();
    }
}