using JobAppDemo.Infra.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAppDemo.Infra.Domain
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<DepartmentModel> Department { get; set; }
        public DbSet<EmployeeModel> Employee { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
