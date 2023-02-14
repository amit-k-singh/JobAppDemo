using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAppDemo.Core.Service.Helper;

public interface IImageUploadHelper
{
    public Task<string> UploadProfile(IFormFile file);
}
