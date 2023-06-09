using AutoMapper;
using JwtIdentity.Models.Interfaces;
using VolNal.Chat.API.Controllers;
using VolNal.Chat.Api.DAL.Models;
using VolNal.Chat.Api.Mapping.Interfaces;

namespace VolNal.Chat.Api.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //HttpModels => Commands
        CreateMap<AuthorizeUserViewModel, UserDto>();
        CreateMap<AuthenticationUserViewModel, UserDto>();
        CreateMap<UserDto, IIdentityUser>();
        CreateMap<IPostCreateChatViewModel, ChatDto>();
        //CreateMap<IPostCreateChatViewModel, RequestCreateChatCommand>();

        //CreateMap<RequestCreateChatCommand, ResponseCreateChatCommand>().ReverseMap();

        //Commands => Dto
        //CreateMap<GetChatsCommand, UserDto>().ReverseMap();
    }
}