using AppDocumentManagement.DB.Entities;

namespace AppDocumentManagement.DB.Controllers
{
    public class EmployeePhotoController
    {
        public bool AddEmployeePhoto(EmployeePhoto employeePhoto)
        {
            bool result = false;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    context.EmployeePhotos.Add(employeePhoto);
                    context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в сохранении фотографии работника");
            }
            return result;
        }

        public List<EmployeePhoto> GetEmployeePhotos()
        {
            List<EmployeePhoto> employeePhotos = new List<EmployeePhoto>();
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    employeePhotos = context.EmployeePhotos.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в получении фотографий сотрудников");
            }
            return employeePhotos;
        }

        public EmployeePhoto GetEmployeePhotoByEmployeeID(int employeeID)
        {
            EmployeePhoto employeePhoto = new EmployeePhoto();
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    employeePhoto = context.EmployeePhotos.SingleOrDefault(e => e.EmployeeID == employeeID);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в получении фотографии по ID сотрудника");
            }
            return employeePhoto;
        }
    }
}
