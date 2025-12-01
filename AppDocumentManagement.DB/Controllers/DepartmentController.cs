using AppDocumentManagement.DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppDocumentManagement.DB.Controllers
{
    public class DepartmentController
    {
        public bool AddDepartment(Department department)
        {
            bool result = false;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    context.Add(department);
                    context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в добавлении отдела / департамента");
            }
            return result;
        }

        public List<Department> GetAllDepartments()
        {
            List<Department> departments = new List<Department>();
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    departments = context.Departments.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в получении списка отделов / департаментов");
            }
            return departments;
        }

        public Department GetDepartmentByID(int id)
        {
            Department department = null;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    var asyncDepartment = context.Departments.SingleOrDefaultAsync(x => x.DepartmentID == id);
                    if (asyncDepartment.Result != null)
                    {
                        department = asyncDepartment.Result;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в получении отдела / департамента по ID");
            }
            return department;
        }

        public bool UpdateDepartment(Department inputDepartment)
        {
            bool result = false;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    Department currentDepartment = context.Departments.SingleOrDefault(x => x.DepartmentID == inputDepartment.DepartmentID);
                    if (currentDepartment == null)
                    {
                        result = AddDepartment(inputDepartment);
                        return result;
                    }
                    currentDepartment.DepartmentTitle = inputDepartment.DepartmentTitle;
                    currentDepartment.DepartmentShortTitle = inputDepartment.DepartmentShortTitle;
                    context.Departments.Update(currentDepartment);
                    context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в обновлении данных отдела / департамента");
            }
            return result;
        }

        public bool RemoveDepartment(int departmentID)
        {
            bool result = false;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    Department currentDepartment = context.Departments.FirstOrDefault(x => x.DepartmentID == departmentID);
                    if (currentDepartment == null)
                    {
                        return result;
                    }
                    context.Departments.Remove(currentDepartment);
                    context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в удалении департамента / отдела");
            }
            return result;
        }
    }
}
