using AutoMapper;
using JobAppDemo.Core.Domain.RequestModel;
using JobAppDemo.Core.Domain.ResponseModel;
using JobAppDemo.Infra.Domain;
using JobAppDemo.Infra.Domain.Entities;

namespace JobAppDemo.Configuration
{
    public class AutoMapperProfile :Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<DepartmentModel, DepartmentResponseModel>();

            CreateMap<EmployeeModel, EmployeeResponseModel>().ForMember(e => e.DepartmentName,ex=>ex.MapFrom(exa=>exa.Department.Name));

            CreateMap<User, UserLoginRequestModel>();
        }
    }
}
