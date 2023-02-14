using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAppDemo.Core.Domain.ResponseModel
{
    public record DepartmentResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
