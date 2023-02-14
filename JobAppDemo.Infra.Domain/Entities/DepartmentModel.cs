using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAppDemo.Infra.Domain
{
    public class DepartmentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public DepartmentModel(string name)
        {
            Name = name;
        }

        public DepartmentModel()
        {
        }
    }
}
