using AppDocumentManagement.DB.Entities;

namespace AppDocumentManagement.DB.Controllers
{
    public class InternalDocumentController
    {
        public bool AddInternalDocument(InternalDocument internalDocument)
        {
            bool result = false;
            if (internalDocument == null) return result;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    if (internalDocument.SignatoryID == 0) return false;
                    Employee signatory = context.Employees.SingleOrDefault(e => e.EmployeeID == internalDocument.SignatoryID);
                    internalDocument.Signatory = signatory;
                    if (internalDocument.ApprovedManagerID != 0)
                    {
                        Employee approvedManager = context.Employees.SingleOrDefault(e => e.EmployeeID == internalDocument.ApprovedManagerID);
                        internalDocument.ApprovedManager = approvedManager;
                    }
                    if (internalDocument.EmployeeRecievedDocumentID != 0)
                    {
                        Employee recievedEmployee = context.Employees.SingleOrDefault(e => e.EmployeeID == internalDocument.EmployeeRecievedDocumentID);
                        internalDocument.EmployeeRecievedDocument = recievedEmployee;
                    }
                    context.InternalDocuments.Add(internalDocument);
                    context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в сохранении документа в базу данных");
            }
            return result;
        }

        public List<InternalDocument> GetInternalDocuments()
        {
            List<InternalDocument> internalDocuments = new List<InternalDocument>();
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    internalDocuments = context.InternalDocuments.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в получении списка внутренних документов");
            }
            return internalDocuments;
        }

        public List<InternalDocument> GetInternalDocumentsByEmployeeRecievedDocumentID(int employeeRecievedDocumentID)
        {
            List<InternalDocument> internalDocuments = new List<InternalDocument>();
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    internalDocuments = context.InternalDocuments.Where(d => d.EmployeeRecievedDocumentID == employeeRecievedDocumentID).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в получении списка внутренних документов по получившему пользователю");
            }
            return internalDocuments;
        }

        public bool RemoveInternalDocument(int internalDocumentID)
        {
            bool result = false;
            if (internalDocumentID == 0) return result;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    InternalDocument aviableInternalDocument = context.InternalDocuments.SingleOrDefault(x => x.InternalDocumentID == internalDocumentID);
                    if (aviableInternalDocument != null)
                    {
                        context.InternalDocuments.Remove(aviableInternalDocument);
                        context.SaveChanges();
                    }
                    List<InternalDocumentFile> files = context.InternalDocumentFiles.Where(d => d.InternalDocumentID == internalDocumentID).ToList();
                    if (files != null && files.Count > 0)
                    {
                        context.InternalDocumentFiles.RemoveRange(files);
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в удалении внутреннего документа из базы данных или его файлов");
            }
            return result;
        }

        public bool UpdateInternalDocument(InternalDocument inputDocument)
        {
            bool result = false;
            if (inputDocument == null) return result;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    InternalDocument aviableDocument = context.InternalDocuments.SingleOrDefault(x => x.InternalDocumentID == inputDocument.InternalDocumentID);
                    if (aviableDocument != null)
                    {
                        aviableDocument.InternalDocumentType = inputDocument.InternalDocumentType;
                        Employee signatory = context.Employees.SingleOrDefault(e => e.EmployeeID == inputDocument.SignatoryID);
                        aviableDocument.SignatoryID = signatory.EmployeeID;
                        if (inputDocument.ApprovedManagerID != 0)
                        {
                            Employee approvedManager = context.Employees.SingleOrDefault(e => e.EmployeeID == inputDocument.ApprovedManagerID);
                            aviableDocument.ApprovedManagerID = approvedManager.EmployeeID;
                        }
                        if (inputDocument.EmployeeRecievedDocumentID != 0)
                        {
                            Employee recievedManager = context.Employees.SingleOrDefault(e => e.EmployeeID == inputDocument.EmployeeRecievedDocumentID);
                            aviableDocument.EmployeeRecievedDocumentID = recievedManager.EmployeeID;
                            aviableDocument.SendingDate = inputDocument.SendingDate;
                        }
                        aviableDocument.InternalDocumentFiles = inputDocument.InternalDocumentFiles;
                        aviableDocument.RegistrationDate = inputDocument.RegistrationDate;
                        aviableDocument.InternalDocumentDate = inputDocument.InternalDocumentDate;
                        aviableDocument.IsRegistated = inputDocument.IsRegistated;
                        aviableDocument.InternalDocumentStatus = inputDocument.InternalDocumentStatus;
                        aviableDocument.InternalDocumentTitle = inputDocument.InternalDocumentTitle;
                        aviableDocument.InternalDocumentContent = inputDocument.InternalDocumentContent;
                        aviableDocument.InternalDocumentStatus = inputDocument.InternalDocumentStatus;
                        context.InternalDocuments.Update(aviableDocument);
                        context.SaveChanges();
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в обновлении внутреннего документа в базе данных или его файлов");
            }
            return result;
        }
    }
}
