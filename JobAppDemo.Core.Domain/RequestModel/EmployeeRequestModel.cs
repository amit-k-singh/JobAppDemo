using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAppDemo.Core.Domain.RequestModel
{
    public class EmployeeRequestModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int DepartmentId { get; set; }
        public IFormFile ProfilePic { get; set; }
    }  
}
