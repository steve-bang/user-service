/*
* Author: Steve Bang
* History:
* - [2025-04-24] - Created by mrsteve.bang@gmail.com
*/
using AutoMapper;

namespace Steve.ManagerHero.Application.Features.Mapping;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.EmailAddress, otp => otp.MapFrom(src => src.EmailAddress.Value))
            .ForMember(dest => dest.SecondaryEmailAddress, otp => otp.MapFrom(src => src.SecondaryEmailAddress != null ? src.SecondaryEmailAddress.Value : null))
            .ForMember(dest => dest.PhoneNumber, otp => otp.MapFrom(src => src.PhoneNumber != null ? src.PhoneNumber.Value : null))
            .ForMember(dest => dest.LastLogin, otp => otp.MapFrom(src => src.LastLoginDate));
    }
}