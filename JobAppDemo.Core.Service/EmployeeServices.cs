using AutoMapper;
using JobAppDemo.Core.Builder;
using JobAppDemo.Core.Contract;
using JobAppDemo.Core.Domain.RequestModel;
using JobAppDemo.Core.Domain.ResponseModel;
using JobAppDemo.Core.Service.Helper;
using JobAppDemo.Infra.Contract;
using JobAppDemo.Infra.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JobAppDemo.Core.Service
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly IEmployeeRepository _employee;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IImageUploadHelper _imageUploadHelper;

        public EmployeeServices(IEmployeeRepository employee,IMapper mapper, IConfiguration configuration, IImageUploadHelper imageUploadHelper)
        {
            _employee = employee;
            _mapper = mapper;
            _configuration = configuration;
            _imageUploadHelper = imageUploadHelper;
        }
        public async Task<string> UserLoginAsync(UserLoginRequestModel userLoginRequestModel)
        {
            var user = await _employee.UserLogin(userLoginRequestModel.Name, userLoginRequestModel.Password);
            var result = _mapper.Map<UserLoginRequestModel>(user);
            if (result != null) 
            {
                var token = GenerateToken(user);
                return token;
            }
            
            throw new Exception("User not found...!!!");
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Name",user.Name),
                new Claim("Password",user.Password)
            };
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public async Task<List<EmployeeResponseModel>> GetEmployeesAsync()
        {
            try
            {
                var employees = await _employee.GetEmployees();
                if (employees == null)
                { 
                    throw new NullReferenceException("Employee not found...!!!");
                }
                var result = _mapper.Map<List<EmployeeResponseModel>>(employees);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<EmployeeResponseModel> GetEmployeeById(int id)
        {
            var employee = await _employee.GetEmployeeById(id);
            var result = _mapper.Map<EmployeeResponseModel>(employee);
            if(employee==null)
            {
                throw new NullReferenceException("Employee not exiest.....!!!!");
            }
            return result;
        }

        public async Task AddEmployee(EmployeeRequestModel employee)
        {
            var profilePic = await _imageUploadHelper.UploadProfile(employee.ProfilePic);
            var employees = EmployeeBuilder.Build(employee, profilePic);
            var employeeCount = await _employee.AddEmployee(employees);

            if (employeeCount == 0)
            {
                throw new Exception("Employee not added...!!!");
            }
        }

        public async Task UpdateEmployee(EmployeeRequestModel employee, int id)
        {
            try
            {
                var updateEmployee = await _employee.GetEmployeeById(id);
                if(updateEmployee==null) 
                {
                    throw new Exception("Employee not exiest.....!!!!");
                }
                updateEmployee.Name = employee.Name;
                updateEmployee.Address = employee.Address;
                updateEmployee.DepartmentId = employee.DepartmentId;
                updateEmployee.ProfilePic = updateEmployee.ProfilePic;
                var count =await _employee.UpdateEmployee(updateEmployee);
                if(count == 0)
                {
                    throw new Exception("Employee not updated...!!!");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteEmployee(int id)
        {
            var employee = await _employee.GetEmployeeById(id);
            if (employee == null)
            { 
                throw new NullReferenceException("Employee not found...!!!");
            }
            await _employee.DeleteEmployee(id);
        }

    }
}
