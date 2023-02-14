using JobAppDemo.Infra.Contract;
using JobAppDemo.Infra.Domain;
using JobAppDemo.Infra.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAppDemo.Infra.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MyDbContext _context;

        public EmployeeRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<User> UserLogin(string username,string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x=>x.Name.ToLower() == username.ToLower() && x.Password == password);
            return user;
        }

        public async Task<int> AddEmployee(EmployeeModel employee)
        {
            await _context.Employee.AddAsync(employee);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<EmployeeModel>> GetEmployees()
        {
            var employees = await _context.Employee.Include(x => x.Department).ToListAsync();
            return employees;
        }

        public async Task<EmployeeModel> GetEmployeeById(int employeeId)
        {
            var employee = await _context.Employee.Include(x => x.Department).SingleOrDefaultAsync(x=> x.Id == employeeId);
            return employee;
        }

        public async Task<int> UpdateEmployee(EmployeeModel employee)
        {
            _context.Employee.Update(employee);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteEmployee(int employeeId)
        {
            var delEmployee = await _context.Employee.FirstOrDefaultAsync(x => x.Id == employeeId);
            if (delEmployee == null)
            {
                throw new Exception("Employee not exiest");
            }
            _context.Employee.Remove(delEmployee);
            return await _context.SaveChangesAsync();
        }

    }
}