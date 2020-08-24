using AutoMapper;
using EmployeeApi.DTO;
using EmployeeApi.Models;

namespace EmployeeApi.Mapping {

    public class UserProfile : Profile {
        public UserProfile() {
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<FullUserDTO, User>().ReverseMap();
        }
    }
}