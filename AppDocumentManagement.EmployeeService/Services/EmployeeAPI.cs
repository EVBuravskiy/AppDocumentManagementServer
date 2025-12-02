using AppDocumentManagement.DB.Controllers;
using AppDocumentManagement.DB.Entities;
using AppDocumentManagement.EmployeeService;
using AppDocumentManagement.EmployeeService.Converters;
using Grpc.Core;

namespace AppDocumentManagement.EmployeeService.Services
{
    public class EmployeeAPI : employeeApi.employeeApiBase
    {
        private readonly ILogger<EmployeeAPI> _logger;
        public EmployeeAPI(ILogger<EmployeeAPI> logger)
        {
            _logger = logger;
        }

        //Employee
        public override Task<BoolReply> AddEmployee(MEmployee mEmployee, ServerCallContext context)
        {
            EmployeeController employeeController = new EmployeeController();
            Employee employee = MEmployeeConverter.ConvertToEmployee(mEmployee);
            bool result = employeeController.AddEmployee(employee);
            BoolReply boolReply = new BoolReply()
            {
                Result = result,
            };
            return Task.FromResult(boolReply);
        }

        public override Task<MEmployeesListReply> GetAllEmployees(EmptyRequest request, ServerCallContext context)
        {
            EmployeeController employeeController = new EmployeeController();
            List<Employee> employees = employeeController.GetAllEmployees();
            MEmployeesListReply employeesListReply = new MEmployeesListReply();
            foreach (Employee employee in employees)
            {
                MEmployee mEmployee = MEmployeeConverter.ConvertToMEmployee(employee);
                employeesListReply.Employees.Add(mEmployee);
            }
            return Task.FromResult(employeesListReply);
        }

        public override Task<MEmployeesListReply> GetAllAvailableEmployees(EmptyRequest request, ServerCallContext context)
        {
            EmployeeController employeeController = new EmployeeController();
            List<Employee> employees = employeeController.GetAllAvailableEmployees();
            MEmployeesListReply employeesListReply = new MEmployeesListReply();
            foreach (Employee employee in employees)
            {
                MEmployee mEmployee = MEmployeeConverter.ConvertToMEmployee(employee);
                employeesListReply.Employees.Add(mEmployee);
            }
            return Task.FromResult(employeesListReply);
        }

        public override Task<MEmployee> GetEmployeeByID(IDRequest request, ServerCallContext context)
        {
            EmployeeController employeeController = new EmployeeController();
            Employee employee = employeeController.GetEmployeeByID(request.ID);
            MEmployee mEmployee = MEmployeeConverter.ConvertToMEmployee(employee);
            return Task.FromResult(mEmployee);
        }

        public override Task<MEmployeesListReply> GetEmployeesByDepartmentID(IDRequest request, ServerCallContext context)
        {
            EmployeeController employeeController = new EmployeeController();
            List<Employee> employees = employeeController.GetEmployeesByDeparmentID(request.ID);
            MEmployeesListReply employeesListReply = new MEmployeesListReply();
            foreach (Employee employee in employees)
            {
                MEmployee mEmployee = MEmployeeConverter.ConvertToMEmployee(employee);
                employeesListReply.Employees.Add(mEmployee);
            }
            return Task.FromResult(employeesListReply);
        }

        public override Task<BoolReply> RemoveEmployee(IDRequest request, ServerCallContext context)
        {
            EmployeeController employeeController = new EmployeeController();
            bool result = employeeController.RemoveEmployee(request.ID);
            BoolReply boolReply = new BoolReply()
            {
                Result = result
            };
            return Task.FromResult(boolReply);
        }

        public override Task<BoolReply> UpdateEmployee(MEmployee mEmployee, ServerCallContext context)
        {
            Employee employee = MEmployeeConverter.ConvertToEmployee(mEmployee);
            EmployeeController employeeController = new EmployeeController();
            bool result = employeeController.UpdateEmployee(employee);
            BoolReply boolReply = new BoolReply()
            {
                Result = result
            };
            return Task.FromResult(boolReply);
        }

        public override Task<MEmployee> GetGeneralDirector(EmptyRequest request, ServerCallContext context)
        {
            EmployeeController employeeController = new EmployeeController();
            Employee employee = employeeController.GetGeneralDirector();
            MEmployee mEmployee = MEmployeeConverter.ConvertToMEmployee(employee);
            return Task.FromResult(mEmployee);
        }

        public override Task<BoolReply> CheckForAviableEmployee(EmptyRequest request, ServerCallContext context)
        {
            EmployeeController employeeController = new EmployeeController();
            bool result = employeeController.CheckForAviableEmployee();
            BoolReply boolReply = new BoolReply()
            {
                Result = result
            };
            return Task.FromResult(boolReply);
        }

        //EmployeePhoto
        public override Task<BoolReply> AddEmployeePhoto(MEmployeePhoto mEmployeePhoto, ServerCallContext context)
        {
            EmployeePhotoController employeePhotoController = new EmployeePhotoController();
            EmployeePhoto employeePhoto = MEmployeePhotoConverter.ConvertToEmployeePhoto(mEmployeePhoto);
            bool result = employeePhotoController.AddEmployeePhoto(employeePhoto);
            BoolReply boolReply = new BoolReply()
            {
                Result = result,
            };
            return Task.FromResult(boolReply);
        }
        public override Task<MEmployeePhotoList> GetEmployeePhotos(EmptyRequest emptyRequest, ServerCallContext context)
        {
            EmployeePhotoController employeePhotoController = new EmployeePhotoController();
            List<EmployeePhoto> employeePhotos = employeePhotoController.GetEmployeePhotos();
            MEmployeePhotoList mEmployeePhotoList = new MEmployeePhotoList();
            foreach (EmployeePhoto photo in employeePhotos)
            {
                MEmployeePhoto mEmployeePhoto = MEmployeePhotoConverter.ConvertToMEmployeePhoto(photo);
                mEmployeePhotoList.MEmployeePhotos.Add(mEmployeePhoto);
            }
            return Task.FromResult(mEmployeePhotoList);
        }
        public override Task<MEmployeePhoto> GetEmployeePhotoByEmployeeID(IDRequest request, ServerCallContext context)
        {
            EmployeePhotoController employeePhotoController = new EmployeePhotoController();
            EmployeePhoto employeePhoto = employeePhotoController.GetEmployeePhotoByEmployeeID(request.ID);
            MEmployeePhoto mEmployeePhoto = MEmployeePhotoConverter.ConvertToMEmployeePhoto(employeePhoto);
            return Task.FromResult(mEmployeePhoto);
        }

        //Department
        public override Task<BoolReply> AddDepartment(MDepartment mDepartment, ServerCallContext context)
        {
            DepartmentController departmentController = new DepartmentController();
            Department department = MDepartmentConverter.ConvertToDepartment(mDepartment);
            bool result = departmentController.AddDepartment(department);
            BoolReply boolReply = new BoolReply() { Result = result };
            return Task.FromResult(boolReply);
        }

        public override Task<MDepartmentListReply> GetAllDepartments(EmptyRequest request, ServerCallContext context)
        {
            DepartmentController departmentController = new DepartmentController();
            List<Department> departments = departmentController.GetAllDepartments();
            MDepartmentListReply departmentListReply = new MDepartmentListReply();
            foreach (Department department in departments)
            {
                MDepartment mDepartment = MDepartmentConverter.ConvertToMDepartment(department);
                departmentListReply.MDepartments.Add(mDepartment);
            }
            return Task.FromResult(departmentListReply);
        }

        public override Task<MDepartment> GetDepartmentByID(IDRequest request, ServerCallContext context)
        {
            DepartmentController departmentController = new DepartmentController();
            Department department = departmentController.GetDepartmentByID(request.ID);
            MDepartment mDepartment = MDepartmentConverter.ConvertToMDepartment(department);
            return Task.FromResult(mDepartment);
        }

        public override Task<BoolReply> UpdateDepartment(MDepartment mDepartment, ServerCallContext context)
        {
            Department department = MDepartmentConverter.ConvertToDepartment(mDepartment);
            DepartmentController departmentController = new DepartmentController();
            bool result = departmentController.UpdateDepartment(department);
            BoolReply dBoolReply = new BoolReply()
            {
                Result = result
            };
            return Task.FromResult(dBoolReply);
        }

        public override Task<BoolReply> RemoveDepartment(IDRequest request, ServerCallContext context)
        {
            DepartmentController departmentController = new DepartmentController();
            bool result = departmentController.RemoveDepartment(request.ID);
            BoolReply dBoolReply = new BoolReply()
            {
                Result = result
            };
            return Task.FromResult(dBoolReply);
        }

        //RegistredUser
        public override Task<BoolReply> AddRegistratedUser(MRegistredUser mRegistredUser, ServerCallContext context)
        {
            RegistredUser registredUser = MRegistredUserConverter.ConvertToRegistredUser(mRegistredUser);
            RegisterUserController registerUserController = new RegisterUserController();
            bool result = registerUserController.AddRegistratedUser(registredUser);
            BoolReply boolReply = new BoolReply()
            {
                Result = result
            };
            return Task.FromResult(boolReply);
        }
        public override Task<MRegistredUserList> GetAllRegistredUsers(EmptyRequest emptyRequest, ServerCallContext context)
        {
            RegisterUserController registredUserController = new RegisterUserController();
            List<RegistredUser> registredUsers = registredUserController.GetAllRegistredUsers();
            MRegistredUserList mRegistredUserList = new MRegistredUserList();
            foreach (RegistredUser user in registredUsers)
            {
                MRegistredUser mRegistredUser = MRegistredUserConverter.ConvertToMRegistredUser(user);
                mRegistredUserList.MRegistredUsers.Add(mRegistredUser);
            }
            return Task.FromResult(mRegistredUserList);
        }
        public override Task<MRegistredUser> GetRegistredUserByEmployeeID(IDRequest request, ServerCallContext context)
        {
            RegisterUserController registerUserController = new RegisterUserController();
            RegistredUser registredUser = registerUserController.GetRegistredUserByEmployeeID(request.ID);
            MRegistredUser mRegistredUser = MRegistredUserConverter.ConvertToMRegistredUser(registredUser);
            return Task.FromResult(mRegistredUser);
        }
        public override Task<BoolReply> UpdateRegistratedUser(MRegistredUser mRegistredUser, ServerCallContext context)
        {
            RegistredUser registredUser = MRegistredUserConverter.ConvertToRegistredUser(mRegistredUser);
            RegisterUserController registerUserController = new RegisterUserController();
            bool result = registerUserController.UpdateRegistratedUser(registredUser);
            BoolReply boolReply = new BoolReply()
            {
                Result = result,
            };
            return Task.FromResult(boolReply);
        }
        public override Task<BoolReply> RemoveRegistratedUser(IDRequest request, ServerCallContext context)
        {
            RegisterUserController registerUserController = new RegisterUserController();
            bool result = registerUserController.RemoveRegistratedUser(request.ID);
            BoolReply boolReply = new BoolReply()
            {
                Result = result,
            };
            return Task.FromResult(boolReply);
        }
        public override Task<MRegistredUser> GetRegistratedUser(StringRequest request, ServerCallContext context)
        {
            RegisterUserController registerUserController = new RegisterUserController();
            RegistredUser registredUser = registerUserController.GetRegistratedUser(request.Login, request.Password);
            MRegistredUser mRegistredUser = MRegistredUserConverter.ConvertToMRegistredUser(registredUser);
            return Task.FromResult(mRegistredUser);
        }
        public override Task<BoolReply> CheckAviableAdministrator(EmptyRequest request, ServerCallContext context)
        {
            RegisterUserController registerUserController = new RegisterUserController();
            bool result = registerUserController.CheckAviableAdministrator();
            BoolReply boolReply = new BoolReply()
            {
                Result = result,
            };
            return Task.FromResult(boolReply);
        }
    }
}
