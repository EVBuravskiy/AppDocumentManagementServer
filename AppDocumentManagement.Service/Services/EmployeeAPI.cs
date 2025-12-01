using AppDocumentManagement.DB.Controllers;
using AppDocumentManagement.DB.Entities;
using AppDocumentManagement.Service.Converters;
using Grpc.Core;

namespace AppDocumentManagement.Service.Services
{
    public class EmployeeAPI : employeeApi.employeeApiBase
    {
        private readonly ILogger<EmployeeAPI> _logger;
        public EmployeeAPI(ILogger<EmployeeAPI> logger)
        {
            _logger = logger;
        }

        public override Task<EBoolReply> AddEmployee(MEmployee mEmployee, ServerCallContext context)
        {
            EmployeeController employeeController = new EmployeeController();
            Employee employee = MEmployeeConverter.ConvertToEmployee(mEmployee);
            bool result = employeeController.AddEmployee(employee);
            EBoolReply boolReply = new EBoolReply()
            {
                Result = result,
            };
            return Task.FromResult(boolReply);
        }

        public override Task<EmployeesListReply> GetAllEmployees(EmptyRequest request, ServerCallContext context)
        {
            EmployeeController employeeController = new EmployeeController();
            List<Employee> employees = employeeController.GetAllEmployees();
            EmployeesListReply employeesListReply = new EmployeesListReply();
            foreach (Employee employee in employees)
            {
                MEmployee mEmployee = MEmployeeConverter.ConvertToMEmployee(employee);
                employeesListReply.Employees.Add(mEmployee);
            }
            return Task.FromResult(employeesListReply);
        }

        public override Task<EmployeesListReply> GetAllAvailableEmployees(EmptyRequest request, ServerCallContext context)
        {
            EmployeeController employeeController = new EmployeeController();
            List<Employee> employees = employeeController.GetAllAvailableEmployees();
            EmployeesListReply employeesListReply = new EmployeesListReply();
            foreach (Employee employee in employees)
            {
                MEmployee mEmployee = MEmployeeConverter.ConvertToMEmployee(employee);
                employeesListReply.Employees.Add(mEmployee);
            }
            return Task.FromResult(employeesListReply);
        }

        public override Task<MEmployee> GetEmployeeByID(EIDRequest request, ServerCallContext context)
        {
            EmployeeController employeeController = new EmployeeController();
            Employee employee = employeeController.GetEmployeeByID(request.ID);
            MEmployee mEmployee = MEmployeeConverter.ConvertToMEmployee(employee);
            return Task.FromResult(mEmployee);
        }

        public override Task<EmployeesListReply> GetEmployeesByDepartmentID(EIDRequest request, ServerCallContext context)
        {
            EmployeeController employeeController = new EmployeeController();
            List<Employee> employees = employeeController.GetEmployeesByDeparmentID(request.ID);
            EmployeesListReply employeesListReply = new EmployeesListReply();
            foreach (Employee employee in employees)
            {
                MEmployee mEmployee = MEmployeeConverter.ConvertToMEmployee(employee);
                employeesListReply.Employees.Add(mEmployee);
            }
            return Task.FromResult(employeesListReply);
        }

        public override Task<EBoolReply> RemoveEmployee(EIDRequest request, ServerCallContext context) 
        {
            EmployeeController employeeController = new EmployeeController();
            bool result = employeeController.RemoveEmployee(request.ID);
            EBoolReply boolReply = new EBoolReply()
            {
                Result = result
            };
            return Task.FromResult(boolReply);
        }

        public override Task<EBoolReply> UpdateEmployee(MEmployee mEmployee, ServerCallContext context)
        {
            Employee employee = MEmployeeConverter.ConvertToEmployee(mEmployee);
            EmployeeController employeeController = new EmployeeController();
            bool result = employeeController.UpdateEmployee(employee);
            EBoolReply boolReply = new EBoolReply()
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

        public override Task<EBoolReply> CheckForAviableEmployee(EmptyRequest request, ServerCallContext context)
        {
            EmployeeController employeeController = new EmployeeController();
            bool result = employeeController.CheckForAviableEmployee();
            EBoolReply boolReply = new EBoolReply()
            {
                Result = result
            };
            return Task.FromResult(boolReply);
        }
    }
}
