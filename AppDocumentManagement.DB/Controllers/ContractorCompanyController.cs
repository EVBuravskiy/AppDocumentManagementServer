using AppDocumentManagement.DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppDocumentManagement.DB.Controllers
{
    public class ContractorCompanyController
    {
        public bool AddContractorCompany(ContractorCompany contractorCompany)
        {
            if (contractorCompany == null) return false;
            bool result = false;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    context.ContractorCompanies.Add(contractorCompany);
                    context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в сохранении компании-контрагента в базу данных");
                result = false;
            }
            return result;
        }
        public List<ContractorCompany> GetContractorCompanies()
        {
            List<ContractorCompany> contractorCompanies = new List<ContractorCompany>();
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    contractorCompanies = context.ContractorCompanies.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в получении списка компаний-контрагентов");
            }
            return contractorCompanies;
        }

        public List<ContractorCompany> GetAvailableContractorCompanies()
        {
            List<ContractorCompany> contractorCompanies = new List<ContractorCompany>();
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    contractorCompanies = context.ContractorCompanies.Where(c => c.isDeleted == false).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в получении списка компаний-контрагентов");
            }
            return contractorCompanies;
        }

        public ContractorCompany GetContractorCompanyByID(int contractorCompanyID)
        {
            ContractorCompany contractorCompany = null;
            if (contractorCompanyID != 0)
            {
                try
                {
                    using (ApplicationContext context = new ApplicationContext())
                    {
                        contractorCompany = context.ContractorCompanies.SingleOrDefault(c => c.ContractorCompanyID == contractorCompanyID);
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Ошибка! Ошибка в получении компании-контрагента по ID");
                }
            }
            return null;
        }
        public bool UpdateContractorCompany(ContractorCompany contractorCompany)
        {
            bool result = false;
            if (contractorCompany != null)
            {
                try
                {
                    using (ApplicationContext context = new ApplicationContext())
                    {
                        ContractorCompany aviableContractorCompany = context.ContractorCompanies.SingleOrDefault(x => x.ContractorCompanyID == contractorCompany.ContractorCompanyID);
                        if (aviableContractorCompany != null)
                        {
                            aviableContractorCompany.ContractorCompanyTitle = contractorCompany.ContractorCompanyTitle;
                            aviableContractorCompany.ContractorCompanyShortTitle = contractorCompany.ContractorCompanyShortTitle;
                            aviableContractorCompany.ContractorCompanyAddress = contractorCompany.ContractorCompanyAddress;
                            aviableContractorCompany.ContractorCompanyPhone = contractorCompany.ContractorCompanyPhone;
                            aviableContractorCompany.ContractorCompanyEmail = contractorCompany.ContractorCompanyEmail;
                            aviableContractorCompany.ContractorCompanyInformation = contractorCompany.ContractorCompanyInformation;
                            context.ContractorCompanies.Update(aviableContractorCompany);
                            context.SaveChanges();
                            result = true;
                        }
                        else
                        {
                            result = AddContractorCompany(contractorCompany);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка! Ошибка в обновлении данных компании-контрагента");
                }
            }
            return result;
        }

        public bool RemoveContractorCompany(int contractorCompanyID)
        {
            bool result = false;
            if (contractorCompanyID != 0)
            {
                try
                {
                    using (ApplicationContext context = new ApplicationContext())
                    {
                        ContractorCompany aviableContractorCompany = context.ContractorCompanies.SingleOrDefault(c => c.ContractorCompanyID == contractorCompanyID);
                        if (aviableContractorCompany != null)
                        {
                            aviableContractorCompany.isDeleted = true;
                            context.ContractorCompanies.Update(aviableContractorCompany);
                            context.SaveChanges();
                            result = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка! Ошибка в удалении компании");
                }
            }
            return result;
        }
    }
}
