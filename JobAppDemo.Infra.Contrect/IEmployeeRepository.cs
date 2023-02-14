using JobAppDemo.Infra.Domain;
using JobAppDemo.Infra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAppDemo.Infra.Contract
{
    public interface IEmployeeRepository
    {
        Task<int> AddEmployee(EmployeeModel employee);

        Task<List<EmployeeModel>> GetEmployees();

        Task<EmployeeModel> GetEmployeeById(int employeeId);

        Task<int> UpdateEmployee(EmployeeModel employee);

        Task<int> DeleteEmployee(int employeeId);

        Task<User> UserLogin(string username,string password);
    }
}
