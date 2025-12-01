using AppDocumentManagement.DB.Entities;

namespace AppDocumentManagement.DB.Controllers
{
    public class ExternalDocumentFileController
    {
        public bool AddExternalDocumentFile(ExternalDocumentFile documentFile, int externalDocumentID)
        {
            bool result = false;
            if (documentFile == null || externalDocumentID == 0) return result;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    ExternalDocument aviableExternalDocument = context.ExternalDocuments.SingleOrDefault(d => d.ExternalDocumentID == externalDocumentID);
                    if (aviableExternalDocument == null) return false;
                    documentFile.ExternalDocument = aviableExternalDocument;
                    context.ExternalDocumentFiles.Add(documentFile);
                    context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в добавлении файла входящего документа в базу данных");
            }
            return result;
        }

        public bool AddExternalDocumentFiles(List<ExternalDocumentFile> documentFiles, int externalDocumentID)
        {
            bool result = false;
            if (documentFiles == null || documentFiles.Count == 0 || externalDocumentID == 0) return result;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    ExternalDocument aviableExternalDocument = context.ExternalDocuments.SingleOrDefault(d => d.ExternalDocumentID == externalDocumentID);
                    if (aviableExternalDocument == null) return false;
                    foreach (ExternalDocumentFile file in documentFiles)
                    {
                        file.ExternalDocument = aviableExternalDocument;
                    }
                    context.ExternalDocumentFiles.AddRange(documentFiles);
                    context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Ошибка в сохранении списка файлов документа в базу данных");
            }
            return result;
        }

        public List<ExternalDocumentFile> GetExternalDocumentFiles(int documentID)
        {
            if (documentID == 0) return null;
            List<ExternalDocumentFile> documentFiles = new List<ExternalDocumentFile>();
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    documentFiles = context.ExternalDocumentFiles.Where(df => df.ExternalDocumentID == documentID).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка в получении файла документа из базы данных");
            }
            return documentFiles;
        }

        public bool RemoveDocumentFile(ExternalDocumentFile documentFile)
        {
            bool result = false;
            if (documentFile == null) return result;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    ExternalDocumentFile aviableDocumentFile = context.ExternalDocumentFiles.SingleOrDefault(f => f.ExternalDocumentFileID == documentFile.ExternalDocumentFileID);
                    if (aviableDocumentFile == null) return false;
                    context.ExternalDocumentFiles.Remove(aviableDocumentFile);
                    context.SaveChanges();
                    return true;
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
