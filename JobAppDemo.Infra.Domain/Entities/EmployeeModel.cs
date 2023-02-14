
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAppDemo.Infra.Domain
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int DepartmentId { get; set; }
        public string? ProfilePic { get; set; }
        public DepartmentModel Department { get; set; }

        public EmployeeModel()
        {
        }

        public EmployeeModel(string name, string address, int departmentId,string profilePic)
        {
            Name = name;
            Address = address;
            DepartmentId = departmentId;
            ProfilePic = profilePic;
        }

    }
}
