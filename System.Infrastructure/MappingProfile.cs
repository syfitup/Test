using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using SYF.Infrastructure.Entities;
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
             .ForMember(a => a.BusinessUnitName, op => op.MapFrom(src => src.SubDepartment.Name));

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
            CreateMap<UserModel, User>();
        }
    }
}
