using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using SYF.Infrastructure.Criteria;
using SYF.Infrastructure.Entities;
using SYF.Infrastructure.Enums;
using SYF.Infrastructure.Models;

namespace SYF.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonModel>()
             .ForMember(a => a.PersonPositionName, op => op.MapFrom(src => src.PersonPosition.Name))
             .ForMember(a => a.DepartmentName, op => op.MapFrom(src => src.Department.Name))
             .ForMember(a => a.BusinessUnitName, op => op.MapFrom(src => src.SubDepartment.Name))
             .AfterMap<NameMeJohnAction>(); ;

            CreateMap<PersonModel, Person>();

            CreateMap<User, UserModel>()
           .ForMember(a => a.EmailAddress, op => op.MapFrom(src => src.Person.EmailAddress.Trim()));

            CreateMap<UserModel, User>()
                .ForMember(a => a.Person, b => b.MapFrom(c => c));

            CreateMap<UserModel, Person>()
                .ForMember(a => a.Id, b => b.Ignore())
                .ForMember(a => a.Roles, b => b.Ignore())
                .ForMember(a => a.Access, b => b.Ignore());

            CreateMap<Person, UserModel>();
            CreateMap<UserSearchRequest, UserCriteria>();

            CreateMap<User, UserSummary>()
            .ForMember(a => a.Name, op => op.MapFrom(src => src.Person.Name))
            .ForMember(a => a.EmailAddress, op => op.MapFrom(src => src.Person.EmailAddress))
            .ForMember(a => a.DepartmentName, op => op.MapFrom(src => src.Person.Department.Deleted ? "" : src.Person.Department.Name))
            .ForMember(a => a.SubDepartmentName, op => op.MapFrom(src => src.Person.SubDepartment.Deleted || src.Person.SubDepartment.Deleted ? "" : src.Person.SubDepartment.Name))
            .ForMember(a => a.PersonPositionName, op => op.MapFrom(src => src.Person.PersonPosition.Name))
            .ForMember(a => a.LockedOut, op => op.MapFrom(src => src.Flags.HasFlag(UserFlags.Locked)))
            .ForMember(a => a.Disabled, op => op.MapFrom(src => src.Flags.HasFlag(UserFlags.Disabled)));

            CreateMap<PersonAccess, PersonAccessModel>();
        }
    }

    public class NameMeJohnAction : IMappingAction<Person, PersonModel>
    {
        public void Process(Person source, PersonModel destination, ResolutionContext context)
        {
            destination = context.Mapper.Map<PersonModel>(source);
            destination.EmailAddress = "test@gmail.com";
        }
    }
}
