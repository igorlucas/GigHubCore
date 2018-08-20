using AutoMapper;
using GigHubCore.Core.Dtos;
using GigHubCore.Core.Models;

namespace GigHubCore.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<Gig, GigDto>();
            CreateMap<Notification, NotificationDto>();
        }
    }
}
