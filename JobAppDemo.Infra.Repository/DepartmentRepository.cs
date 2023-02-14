

using JobAppDemo.Infra.Contract;
using JobAppDemo.Infra.Domain;
using Microsoft.EntityFrameworkCore;

namespace JobAppDemo.Infra.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly MyDbContext _context;

        public DepartmentRepository(MyDbContext context) 
        {
            _context = context;
        }
        public async Task<List<DepartmentModel>> GetDepartments()
        { 
            var departments =await _context.Department.ToListAsync();
            return departments;
        }

        public async Task<DepartmentModel> GetDepartment(int id)
        { 
            var department =  await _context.Department.SingleOrDefaultAsync(x=> x.Id == id);
            return department;
        }

        public async Task<int> AddDepartment(DepartmentModel department)
        {
            await _context.Department.AddAsync(department);
            return await _context.SaveChangesAsync();
        }


        public async Task<int> UpdateDepartment(DepartmentModel department)
        {
            _context.Department.Update(department);
            return await _context.SaveChangesAsync();
        }
    }
}
