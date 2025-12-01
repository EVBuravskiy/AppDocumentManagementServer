using AppDocumentManagement.DB.Entities;

namespace AppDocumentManagement.DB.Controllers
{
    public class InternalDocumentFileController
    {
        public bool AddInternalDocumentFile(InternalDocumentFile internalDocumentFile, int internalDocumentID)
        {
            bool result = false;
            if (internalDocumentFile == null) return result;
            if (internalDocumentID == 0) return result;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    InternalDocument aviableInternalDocument = context.InternalDocuments.SingleOrDefault(d => d.InternalDocumentID == internalDocumentID);
                    if (aviableInternalDocument == null) return result;
                    internalDocumentFile.InternalDocument = aviableInternalDocument;
                    context.InternalDocumentFiles.Add(internalDocumentFile);
                    context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в сохранении файла внутреннего документа в базу данных");
            }
            return result;
        }

        public bool AddInternalDocumentFiles(List<InternalDocumentFile> internalDocumentFiles)
        {
            bool result = false;
            if (internalDocumentFiles == null || internalDocumentFiles.Count == 0) return result;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    context.InternalDocumentFiles.AddRange(internalDocumentFiles);
                    context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в сохранении списка файлов внутреннего документа в базу данных");
            }
            return result;
        }

        public List<InternalDocumentFile> GetInternalDocumentFiles(int documentID)
        {
            if (documentID == 0) return null;
            List<InternalDocumentFile> documentFiles = new List<InternalDocumentFile>();
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    documentFiles = context.InternalDocumentFiles.Where(df => df.InternalDocumentID == documentID).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в получении файла внутреннего документа из базы данных");
            }
            return documentFiles;
        }

        public bool RemoveInternalDocumentFile(InternalDocumentFile internalDocumentFile)
        {
            bool result = false;
            if (internalDocumentFile == null) return result;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    InternalDocumentFile aviableDocumentFile = context.InternalDocumentFiles.SingleOrDefault(f => f.InternalDocumentFileID == internalDocumentFile.InternalDocumentFileID);
                    if (aviableDocumentFile == null) return result;
                    context.InternalDocumentFiles.Remove(aviableDocumentFile);
                    context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в удалении файла внутреннего документа из базы данных");
            }
            return result;
        }
    }
}
