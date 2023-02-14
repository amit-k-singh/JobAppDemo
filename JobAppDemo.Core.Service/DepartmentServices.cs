using AutoMapper;
using JobAppDemo.Core.Builder;
using JobAppDemo.Core.Contract;
using JobAppDemo.Core.Domain.RequestModel;
using JobAppDemo.Core.Domain.ResponseModel;
using JobAppDemo.Infra.Contract;
using JobAppDemo.Infra.Domain;

namespace JobAppDemo.Core.Service
{
    public class DepartmentServices : IDepartmentServices
    {
        private readonly IDepartmentRepository _department;
        private readonly IMapper _mapper;

        public DepartmentServices(IDepartmentRepository department,IMapper mapper)
        {
            _department = department;
            _mapper = mapper;
        }

        public async Task<List<DepartmentResponseModel>> GetDepartmentsAsync()
        {
            try
            {
                var departments = await _department.GetDepartments();
                var result = _mapper.Map<List<DepartmentResponseModel>>(departments);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DepartmentResponseModel> GetDepartmentByIdAsync(int Id)
        {
            try
            {
                var department = await _department.GetDepartment(Id);
                var result = _mapper.Map<DepartmentResponseModel>(department);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task AddDepartmentAsync(DepartmentRequestModel department)
        {
            try
            {
                var departments = DepartmentBuilder.Build(department);
                var departmentCount = await _department.AddDepartment(departments);
                if(departmentCount == 0)
                {
                    throw new Exception("Department not added successfully...!!!");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task UpdateDepartmentAsync(DepartmentRequestModel department,int id)
        {
            try
            {
                //var departments = DepartmentBuilder.Build(department);
                var departments = await _department.GetDepartment(id);
                if (departments == null)
                {
                    throw new Exception("Department not fount...!!!");
                }
                departments.Name = department.Name;
                var updateDepartment = await _department.UpdateDepartment(departments);

            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
