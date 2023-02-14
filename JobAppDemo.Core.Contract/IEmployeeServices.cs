using JobAppDemo.Core.Domain.RequestModel;
using JobAppDemo.Core.Domain.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAppDemo.Core.Contract
{
    public interface IEmployeeServices
    {
        Task AddEmployee(EmployeeRequestModel employee);

        Task<List<EmployeeResponseModel>> GetEmployeesAsync();

        Task<EmployeeResponseModel> GetEmployeeById(int id);

        Task UpdateEmployee(EmployeeRequestModel employee,int id);

        Task DeleteEmployee(int id);

        Task<string> UserLoginAsync(UserLoginRequestModel userLoginRequestModel);
    }
}
