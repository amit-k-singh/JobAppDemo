using JobAppDemo.Core.Contract;
using JobAppDemo.Core.Service;
using JobAppDemo.Core.Service.Helper;
using JobAppDemo.Infra.Contract;
using JobAppDemo.Infra.Repository;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace JobAppDemo.Configuration
{
    public static class DependancyConfiguration
    {
        public static void AddDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDepartmentServices, DepartmentServices>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IEmployeeServices, EmployeeServices>();
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddTransient<IImageUploadHelper,ImageUploadHelper>();
        }
    }
}
