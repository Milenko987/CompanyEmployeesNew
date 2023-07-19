using AutoMapper;
using Entities;
using Entities.Models;
using Shared.DataTransferObjects;

namespace CompanyEmployeesNew
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDTO>()
                .ForMember(a => a.FullAddress, opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));

            CreateMap<Employee, EmployeeDTO>();
            CreateMap<CompanyForCreattionDTO, Company>();
            CreateMap<EmployeeForCreationDTO, Employee>();
            CreateMap<EmployeeForUpdateDTO, Employee>().ReverseMap();
            CreateMap<CompanyForUpdateDTO, Company>();
            CreateMap<UserForRegistrationDTO, User>();
        }
    }
}
