using JobAppDemo.Core.Domain.RequestModel;
using JobAppDemo.Infra.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAppDemo.Core.Builder
{
    public class EmployeeBuilder
    {
        public static EmployeeModel Build(EmployeeRequestModel model,string profilePic)
        {
            return new EmployeeModel(model.Name, model.Address, model.DepartmentId, profilePic);
        }
    }
} 
