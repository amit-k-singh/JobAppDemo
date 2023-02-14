using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAppDemo.Core.Domain.RequestModel;

public class UserLoginRequestModel
{
    public string Name { get; set; }
    public string Password { get; set; }
}
