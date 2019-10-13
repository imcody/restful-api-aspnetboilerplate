using AutoMapper;
using ResponsibleSystem.Authorization.Roles;
using ResponsibleSystem.Authorization.Users;
using ResponsibleSystem.Extensions;
using ResponsibleSystem.Shared.Dto;

namespace ResponsibleSystem.Backoffice.Users.Dto
{
    public class UserMapProfile : Profile
    {
        public UserMapProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(x => x.UserRole, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.UserRole = src.UserRole.GetDescriptionFromValue();
                });

            CreateMap<UserDto, User>()
                .ForMember(x => x.Roles, opt => opt.Ignore())
                .ForMember(x => x.UserRole, opt => opt.Ignore())
                .ForMember(x => x.UserName, opt => opt.MapFrom(u=> u.EmailAddress))
                .AfterMap((src, dest) =>
                {
                    dest.UserRole = EnumExtensions.GetValueFromDescription<AppUserRole>(src.UserRole) ?? default(AppUserRole);
                });


            CreateMap<User, CreateUserDto>()
                .ForMember(x => x.UserRole, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.UserRole = src.UserRole.GetDescriptionFromValue();
                });

            CreateMap<CreateUserDto, User>()
                .ForMember(x => x.Roles, opt => opt.Ignore())
                .ForMember(x => x.UserName, opt => opt.MapFrom(u=> u.EmailAddress))
                .ForMember(x => x.UserRole, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.UserRole = EnumExtensions.GetValueFromDescription<AppUserRole>(src.UserRole) ?? default(AppUserRole);
                }); ;
        }
    }
}
