using AutoMapper;
using JobAppDemo.Configuration;
using JobAppDemo.Core.Builder;
using JobAppDemo.Core.Contract;
using JobAppDemo.Core.Domain.RequestModel;
using JobAppDemo.Core.Service;
using JobAppDemo.Core.Service.Helper;
using JobAppDemo.Infra.Contract;
using JobAppDemo.Infra.Domain;
using JobAppDemo.Infra.Repository;
using Microsoft.AspNetCore.Http;
using Moq;

namespace ServicesTest;

public class EmployeeServicesTest
{
    private readonly Mock<IEmployeeRepository> _employeeRepository;
    private readonly MapperConfiguration configuration;
    private readonly IMapper mapper;
    private readonly Mock<IImageUploadHelper> _imageUploadHelper;
    private readonly EmployeeServices employeeServices;

    public EmployeeServicesTest()
    {
        _employeeRepository = new Mock<IEmployeeRepository>();
        configuration = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfile()));
        mapper = new Mapper(configuration);
        _imageUploadHelper = new Mock<IImageUploadHelper>();
        employeeServices = new EmployeeServices(_employeeRepository.Object, mapper, null, _imageUploadHelper.Object);
    }

    [Fact]
    public async Task GetEmployeeById_Test()
    {
        EmployeeModel employee = new EmployeeModel()
        {
            Id = 1,
            Name = "Amit",
            Address = "Amroli"
        };
        _employeeRepository.Setup(x => x.GetEmployeeById(1)).ReturnsAsync(employee);
        var _employeeServices = new EmployeeServices(_employeeRepository.Object, mapper, null, _imageUploadHelper.Object);
        var result = await _employeeServices.GetEmployeeById(1);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetEmployeeById_Exception_Test()
    {
        EmployeeModel employee = new EmployeeModel()
        {
            Id = 1,
            Name = "Amit",
            Address = "Amroli"
        };
        _employeeRepository.Setup(x => x.GetEmployeeById(2)).ReturnsAsync(null as EmployeeModel);
        var _employeeServices = new EmployeeServices(_employeeRepository.Object, mapper, null, _imageUploadHelper.Object);
        await Assert.ThrowsAsync<NullReferenceException>(async () => await _employeeServices.GetEmployeeById(2));
    }

    [Fact]
    public async void GetEmployeesAsync_Test()
    {
        List<EmployeeModel> employeeList = new List<EmployeeModel>()
        {
            new EmployeeModel()
            {
                Id= 1,
                Name="Amit",
                Address="Amroli"
            }
        };
        _employeeRepository.Setup(x => x.GetEmployees()).ReturnsAsync(employeeList);
        var _employeeServices = new EmployeeServices(_employeeRepository.Object, mapper, null, _imageUploadHelper.Object);
        var result = await _employeeServices.GetEmployeesAsync();
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetEmployees_Exception_Test()
    {
        List<EmployeeModel> employees = new List<EmployeeModel>()
        {
            new EmployeeModel()
            {
                Id= 1,
                Name= "Amit",
                Address= "Amroli"
            }
        };
        _employeeRepository.Setup(x => x.GetEmployees()).ReturnsAsync(null as List<EmployeeModel>);
        var _employeeServices = new EmployeeServices(_employeeRepository.Object, mapper, null, _imageUploadHelper.Object);
        await Assert.ThrowsAsync<NullReferenceException>(async () => await _employeeServices.GetEmployeesAsync());
    }

    [Fact]
    public async Task AddEmployeeAsync_Test()
    {
        EmployeeRequestModel employee = new EmployeeRequestModel()
        {
            Name = "Amit",
            Address = "amroli",
            DepartmentId = 1
        };
        _imageUploadHelper.Setup(x => x.UploadProfile(It.IsAny<IFormFile>())).ReturnsAsync("File key");
        _employeeRepository.Setup(x => x.AddEmployee(It.IsAny<EmployeeModel>())).ReturnsAsync(1);
        employeeServices.AddEmployee(employee);
    }

    [Fact]
    public async Task AddEmployeeAsync_Exception_Test()
    {
        EmployeeRequestModel employee = new EmployeeRequestModel()
        {
            Name = "Amit",
            Address = "amroli",
            DepartmentId = 1
        };
        _imageUploadHelper.Setup(x => x.UploadProfile(It.IsAny<IFormFile>())).ReturnsAsync("File key");
        _employeeRepository.Setup(x => x.AddEmployee(It.IsAny<EmployeeModel>())).ReturnsAsync(0);
        Assert.ThrowsAsync<Exception>(async () => await employeeServices.AddEmployee(employee));
    }

    [Fact]
    public async Task UpdateEmployee_Test()
    {
        EmployeeRequestModel employee = new EmployeeRequestModel()
        {
            Name = "Amit",
            Address = "Surat",
            DepartmentId = 1,
        };
        EmployeeModel empModel = new EmployeeModel()
        {
            Id = 1,
            Name = "Name",
            Address = "Amit",
            DepartmentId = 1
        };
        _employeeRepository.Setup(x => x.GetEmployeeById(It.IsAny<int>())).ReturnsAsync(empModel);
        _employeeRepository.Setup(x => x.UpdateEmployee(empModel)).ReturnsAsync(empModel.Id);
        EmployeeServices employeeServices = new EmployeeServices(_employeeRepository.Object, mapper, null, _imageUploadHelper.Object);
        await employeeServices.UpdateEmployee(employee, It.IsAny<int>());
    }
    
    [Fact]
    public async Task UpdateEmployee_Null_Test()
    {
        EmployeeRequestModel employee = new EmployeeRequestModel()
        {
            Name = "Amit",
            Address = "Surat",
            DepartmentId = 1
        };
        _employeeRepository.Setup(x => x.GetEmployeeById(It.IsAny<int>())).ReturnsAsync(null as EmployeeModel);
        _employeeRepository.Setup(x => x.UpdateEmployee(null as EmployeeModel)).ReturnsAsync(It.IsAny<int>());
        var employeeServices = new EmployeeServices(_employeeRepository.Object, mapper, null, _imageUploadHelper.Object);
        Assert.ThrowsAsync<Exception>(async () => await employeeServices.UpdateEmployee(employee, It.IsAny<int>()));
    }

    [Fact]
    public async Task UpdateEmployee_IsUpdated_Test()
    {
        EmployeeRequestModel employee = new EmployeeRequestModel()
        {
            Name = "Amit",
            Address = "Surat",
            DepartmentId = 1
        };
        EmployeeModel empModel = new EmployeeModel()
        {
            Id = 1,
            Name = "Amit kumar",
            Address = "Amroli",
            DepartmentId = 1
        };
        _employeeRepository.Setup(x => x.GetEmployeeById(It.IsAny<int>())).ReturnsAsync(empModel);
        _employeeRepository.Setup(x => x.UpdateEmployee(empModel)).ReturnsAsync(It.IsAny<int>());
        var employeeServices = new EmployeeServices(_employeeRepository.Object, mapper, null, _imageUploadHelper.Object);
        Assert.ThrowsAsync<Exception>(async () => await employeeServices.UpdateEmployee(employee, It.IsAny<int>()));
    }

    [Fact]
    public async Task DeleteEmployee_Test()
    {
        EmployeeModel employee = new EmployeeModel()
        {
            Id = 1,
            Name = "Amit",
            Address = "Amroli"
        };
        _employeeRepository.Setup(x => x.GetEmployeeById(It.IsAny<int>())).ReturnsAsync(employee);
        var employeeServices = new EmployeeServices(_employeeRepository.Object, mapper,null, _imageUploadHelper.Object);
        await employeeServices.DeleteEmployee(It.IsAny<int>());
    }

    public async Task DeleteEmployee_Exception_Test()
    {
        EmployeeModel employee = new EmployeeModel()
        {
            Id = 1,
            Name = "Amit",
            Address = "Amroli"
        };
        _employeeRepository.Setup(x => x.GetEmployeeById(It.IsAny<int>())).ReturnsAsync(null as EmployeeModel);
        _employeeRepository.Setup(x => x.DeleteEmployee(It.IsAny<int>()));
        var employeeServices = new EmployeeServices(_employeeRepository.Object, mapper, null, _imageUploadHelper.Object);
        await Assert.ThrowsAsync<NullReferenceException>(async () => await employeeServices.DeleteEmployee(It.IsAny<int>()));
    }
}