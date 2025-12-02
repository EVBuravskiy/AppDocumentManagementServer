using AppDocumentManagement.DB.Entities;

namespace AppDocumentManagement.DB.Controllers
{
    public class ExternalDocumentController
    {
        public bool AddExternalDocument(ExternalDocument externalDocument)
        {
            bool result = false;    
            if (externalDocument == null) return result;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    ContractorCompany contractorCompany = context.ContractorCompanies.Where(x => x.ContractorCompanyID == externalDocument.ContractorCompany.ContractorCompanyID).FirstOrDefault();
                    externalDocument.ContractorCompany = contractorCompany;
                    if (externalDocument.ReceivingEmployee != null)
                    {
                        Employee employee = context.Employees.Where(e => e.EmployeeID == externalDocument.ReceivingEmployee.EmployeeID).FirstOrDefault();
                        externalDocument.ReceivingEmployee = employee;
                    }
                    context.ExternalDocuments.Add(externalDocument);
                    context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка в сохранении входящего документа в базу данных");
            }
            return result;
        }

        public List<ExternalDocument> GetAllExternalDocuments()
        {
            List<ExternalDocument> externalDocuments = new List<ExternalDocument>();
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    externalDocuments = context.ExternalDocuments.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в получении списка входящих документов из базы данных");
            }
            return externalDocuments;
        }

        public List<ExternalDocument> GetExternalDocumentsByEmployeeReceivedDocumentID(int employeeReceivedDocumentID)
        {
            List<ExternalDocument> documents = new List<ExternalDocument>();
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    documents = context.ExternalDocuments.Where(d => d.ReceivingEmployeeID == employeeReceivedDocumentID).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в получении списка входящих документов по получившему сотруднику из базы данных");
            }
            return documents;
        }

        public bool UpdateDocument(ExternalDocument document)
        {
            bool result = false;
            if (document == null) return result;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    ExternalDocument aviableDocument = context.ExternalDocuments.SingleOrDefault(x => x.ExternalDocumentID == document.ExternalDocumentID);
                    if (aviableDocument != null)
                    {
                        aviableDocument.ExternalDocumentTitle = document.ExternalDocumentTitle;
                        aviableDocument.ExternalDocumentNumber = document.ExternalDocumentNumber;
                        aviableDocument.ExternalDocumentDate = document.ExternalDocumentDate;
                        ContractorCompany contractorCompany = context.ContractorCompanies.SingleOrDefault(x => x.ContractorCompanyID == document.ContractorCompany.ContractorCompanyID);
                        aviableDocument.ContractorCompany = contractorCompany;
                        aviableDocument.ExternalDocumentType = document.ExternalDocumentType;
                        aviableDocument.ExternalDocumentFiles = document.ExternalDocumentFiles;
                        aviableDocument.RegistrationDate = document.RegistrationDate;
                        aviableDocument.IsRegistated = document.IsRegistated;
                        if (document.ReceivingEmployee != null)
                        {
                            Employee employee = context.Employees.SingleOrDefault(x => x.EmployeeID == document.ReceivingEmployee.EmployeeID);
                            aviableDocument.ReceivingEmployee = employee;
                            aviableDocument.ExternalDocumentSendingDate = document.ExternalDocumentSendingDate;
                        }
                        aviableDocument.ExternalDocumentStatus = document.ExternalDocumentStatus;
                        context.ExternalDocuments.Update(aviableDocument);
                        context.SaveChanges();
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в обновлении документа в базе данных или его файлов");
            }
            return result;
        }

        public bool RemoveDocument(int externalDocumentID)
        {
            bool result = false;
            if (externalDocumentID == 0) return result;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    ExternalDocument aviableDocument = context.ExternalDocuments.SingleOrDefault(x => x.ExternalDocumentID == externalDocumentID);
                    if (aviableDocument != null)
                    {

                        aviableDocument.IsDeleted = true;
                        context.ExternalDocuments.Update(aviableDocument);
                        context.SaveChanges();
                    }
                    List<ExternalDocumentFile> files = context.ExternalDocumentFiles.Where(d => d.ExternalDocumentID == externalDocumentID).ToList();
                    if (files != null && files.Count > 0)
                    {
                        context.ExternalDocumentFiles.RemoveRange(files);
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в удалении входящего документа из базы данных или его файлов");
            }
            return result;
        }
    }
}
