using AutoMapper;
using EmployeeApi.DTO;
using EmployeeApi.Models;

namespace EmployeeApi.Mapping {

    public class EmployeeProfile : Profile {
        public EmployeeProfile() {
            CreateMap<EmployeeDTO, Employee>().ReverseMap();
            CreateMap<AddressDTO, Address>().ReverseMap();
            CreateMap<FullEmployeeDTO, Employee>().ReverseMap();
        }
    }
}