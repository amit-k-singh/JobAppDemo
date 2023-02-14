
using JobAppDemo.Core.Domain.RequestModel;
using JobAppDemo.Core.Domain.ResponseModel;

namespace JobAppDemo.Core.Contract
{
    public interface IDepartmentServices
    {
        //Department Service Interface
        Task <List<DepartmentResponseModel>> GetDepartmentsAsync();
        Task<DepartmentResponseModel> GetDepartmentByIdAsync(int id);

        Task AddDepartmentAsync(DepartmentRequestModel department);

        Task UpdateDepartmentAsync(DepartmentRequestModel department,int id);
    }
}
