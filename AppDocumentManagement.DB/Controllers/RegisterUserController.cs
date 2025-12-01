using AppDocumentManagement.DB.Entities;
using AppDocumentManagement.DB.Utilities;

namespace AppDocumentManagement.DB.Controllers
{
    public class RegisterUserController
    {
        public bool AddRegistratedUser(RegistredUser inputUser)
        {
            bool result = false;
            if (inputUser == null) return result;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    context.RegistredUsers.Add(inputUser);
                    context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в сохранении зарегистрированного пользователя в базу данных");
            }
            return result;
        }

        public List<RegistredUser> GetAllRegistredUsers()
        {
            List<RegistredUser> registredUsers = new List<RegistredUser>();
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    registredUsers = context.RegistredUsers.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в получении списка пользователей");
            }
            return registredUsers;
        }

        public RegistredUser GetRegistredUserByEmployeeID(int employeeID)
        {
            if (employeeID == 0) return null;
            RegistredUser registredUser = null;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    registredUser = context.RegistredUsers.SingleOrDefault(x => x.EmployeeID == employeeID);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в получении пользователя по ID сотрудника");
            }
            return registredUser;
        }

        public bool UpdateRegistratedUser(RegistredUser inputUser)
        {
            bool result = false;
            if (inputUser == null) return result;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    RegistredUser currentUser = context.RegistredUsers.SingleOrDefault(x => x.RegistredUserID == inputUser.RegistredUserID);
                    if (currentUser == null)
                    {
                        result = AddRegistratedUser(inputUser);
                        return result;
                    }
                    currentUser.RegistredUserLogin = inputUser.RegistredUserLogin;
                    currentUser.RegistredUserPassword = inputUser.RegistredUserPassword;
                    currentUser.UserRole = inputUser.UserRole;
                    currentUser.Employee = inputUser.Employee;
                    currentUser.IsRegistered = true;
                    currentUser.RegistredUserTime = inputUser.RegistredUserTime;
                    context.RegistredUsers.Update(currentUser);
                    context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в обновлении данных зарегистрированного пользователя");
            }
            return result;
        }

        public bool RemoveRegistratedUser(RegistredUser inputUser)
        {
            bool result = false;
            if (inputUser == null) return result;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    RegistredUser currentUser = context.RegistredUsers.SingleOrDefault(x => x.RegistredUserID == inputUser.RegistredUserID);
                    if (currentUser == null) return result;
                    currentUser.IsRegistered = false;
                    context.RegistredUsers.Update(currentUser);
                    context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в отмене регистрации пользователя");
            }
            return result;
        }

        public RegistredUser GetRegistratedUser(string login, string password)
        {
            return UserAutentication(login, password);
        }

        private RegistredUser UserAutentication(string login, string password)
        {
            RegistredUser checkedRegistredUser = new RegistredUser();
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password)) return checkedRegistredUser;
            using (ApplicationContext context = new ApplicationContext())
            {
                RegistredUser registredUser = context.RegistredUsers.SingleOrDefault(x => x.RegistredUserLogin == login);
                if (registredUser == null)
                {
                    checkedRegistredUser.UserRole = UserRole.Performer;
                    return checkedRegistredUser;
                }
                else
                {
                    checkedRegistredUser.RegistredUserLogin = login;
                    string forGettingHash = $"{login}-{password}";
                    string passHash = PassHasher.CalculateMD5Hash(forGettingHash);
                    if (String.Equals(passHash, registredUser.RegistredUserPassword))
                    {
                        checkedRegistredUser.UserRole = registredUser.UserRole;
                        checkedRegistredUser.RegistredUserTime = registredUser.RegistredUserTime;
                        checkedRegistredUser.Employee = registredUser.Employee;
                        checkedRegistredUser.EmployeeID = registredUser.EmployeeID;
                        checkedRegistredUser.IsRegistered = registredUser.IsRegistered;
                        return checkedRegistredUser;
                    }
                }
                return checkedRegistredUser;
            }
        }

        public bool CheckAviableAdministrator()
        {
            bool result = false;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    List<RegistredUser> registredUsers = GetAllRegistredUsers();
                    RegistredUser admin = registredUsers.Where(e => e.UserRole == UserRole.Administrator).FirstOrDefault();
                    if (admin != null) result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в проверке администратора в базе данных");
            }
            return result;
        }
    }
}
