using JobAppDemo.Core.Domain.RequestModel;
using JobAppDemo.Infra.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAppDemo.Core.Builder
{
    public class DepartmentBuilder
    {
        public static DepartmentModel Build(DepartmentRequestModel model)
        {
            return new DepartmentModel(model.Name);
        }
    }
}
