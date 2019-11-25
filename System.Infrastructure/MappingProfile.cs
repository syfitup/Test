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
        }
    }
}
