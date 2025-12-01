using AppDocumentManagement.DB.Entities;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace AppDocumentManagement.DB.Controllers
{
    public class EmployeeController
    {
        public bool AddEmployee(Employee newEmployee)
        {
            bool result = false;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    context.Employees.Add(newEmployee);
                    context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в сохранении нового сотрудника");
            }
            return result;
        }

        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    employees = context.Employees.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в получении списка сотрудников");
            }
            return employees;
        }

        public List<Employee> GetAllAvailableEmployees()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    employees = context.Employees.Where(e => e.isDeleted == false).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в получении списка доступных сотрудников");
            }
            return employees;
        }

        public Employee GetEmployeeByID(int employeeID)
        {
            Employee employee = null;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    employee = context.Employees.Where(e => e.EmployeeID == employeeID).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в получении сотрудника по ID");
            }
            return employee;
        }

        public List<Employee> GetEmployeesByDeparmentID(int departmentID)
        {
            List<Employee> employeesByDepartment = new List<Employee>();
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    employeesByDepartment = context.Employees.Where(x => x.DepartmentID == departmentID).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в получении сотрудника по ID отдела");
            }
            return employeesByDepartment;
        }

        public bool RemoveEmployee(int employeeID)
        {
            bool result = false;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    Employee currentEmployee = context.Employees.SingleOrDefault(x => x.EmployeeID == employeeID);
                    if (currentEmployee != null)
                    {
                        currentEmployee.isDeleted = true;
                        context.Employees.Update(currentEmployee);
                        context.SaveChanges();
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в удалении сотрудника");
            }
            return result;
        }

        public bool UpdateEmployee(Employee inputEmployee)
        {
            bool result = false;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    context.Employees.Update(inputEmployee);
                    context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в обновлении данных сотрудника");
            }
            return result;
        }

        public Employee GetGeneralDirector()
        {
            Employee generalDirector = null;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    generalDirector = context.Employees.Where(e => e.EmployeeRole == EmployeeRole.GeneralDirector).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в получении данных генерального директора");
            }
            return generalDirector;
        }

        public bool CheckForAviableEmployee()
        {
            bool result = false;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    List<Employee> employees = GetAllEmployees();
                    if (employees.Count > 0) result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в получении списка сотрудников");
            }
            return result;
        }
    }
}
