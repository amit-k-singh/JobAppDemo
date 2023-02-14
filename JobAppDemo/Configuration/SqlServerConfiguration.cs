using JobAppDemo.Infra.Domain;
using Microsoft.EntityFrameworkCore;

namespace JobAppDemo.Configuration
{
    public static class SqlServerConfiguration
    {
        public static void AddSqlServer(this IServiceCollection services,IConfiguration configuration) 
        {
            var connectionString = configuration["ConnectionStrings:Default"];

            services.AddDbContext<MyDbContext>(option => 
            {
                option.UseSqlServer(connectionString, x =>
                {
                    x.MigrationsAssembly("JobAppDemo.Infra.Domain");
                });
            }, ServiceLifetime.Singleton);
        }
    }
}
