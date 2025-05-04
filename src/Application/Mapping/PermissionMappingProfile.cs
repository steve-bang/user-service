/*
* Author: Steve Bang
* History:
* - [2025-05-01] - Created by mrsteve.bang@gmail.com
*/
using AutoMapper;

namespace Steve.ManagerHero.Application.Features.Mapping;

public class PermissionMappingProfile : Profile
{
    public PermissionMappingProfile()
    {
        CreateMap<Permission, PermissionDto>();
    }
}