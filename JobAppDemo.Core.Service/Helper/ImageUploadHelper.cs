using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAppDemo.Core.Service.Helper
{
    public class ImageUploadHelper : IImageUploadHelper
    {
        public async Task<string> UploadProfile(IFormFile file)
        {
            var filename = file.FileName.Substring(0, file.FileName.LastIndexOf('.'));
            var ext = file.FileName.Substring(file.FileName.LastIndexOf('.'),
                file.FileName.Length - file.FileName.LastIndexOf('.'));
            filename += Guid.NewGuid().ToString();
            var filepath = $"wwwroot/Images/{filename + ext}";

            using (var fstream = System.IO.File.Create(filepath))
            {
                var stream = file.OpenReadStream();
                fstream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(fstream);
            }
            return filepath;
        }
    }
}
