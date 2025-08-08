/*
* Author: Steve Bang
* History:
* - [2025-08-03] - Created by mrsteve.bang@gmail.com
*/
using AutoMapper;
using Steve.ManagerHero.UserService.Application.DTOs;

namespace Steve.ManagerHero.Application.Features.Mapping;

public class SessionMappingProfile : Profile
{
    public SessionMappingProfile()
    {
        CreateMap<Session, SessionDto>();
    }
}