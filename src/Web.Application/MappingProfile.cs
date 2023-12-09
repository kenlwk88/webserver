using AutoMapper;
using Core.DataAccess.SqlLite;
using Web.Domain.User;
using Web.Domain.User.Common;

namespace Web.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterUserApiRequest, UserDto>();
            CreateMap<UpdateUserApiRequest, UserDto>();
            CreateMap<UserDto, UserModel>();
        }
    }
}
