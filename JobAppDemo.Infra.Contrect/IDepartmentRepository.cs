using JobAppDemo.Infra.Domain;

namespace JobAppDemo.Infra.Contract
{
    public interface IDepartmentRepository
    {
        public Task<List<DepartmentModel>> GetDepartments();

        public Task<DepartmentModel> GetDepartment(int id);

        Task<int> AddDepartment(DepartmentModel department);

        Task<int> UpdateDepartment(DepartmentModel department);
    }
}