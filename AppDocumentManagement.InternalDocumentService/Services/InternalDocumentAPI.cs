using AppDocumentManagement.DB.Controllers;
using AppDocumentManagement.DB.Entities;
using AppDocumentManagement.InternalDocumentService;
using AppDocumentManagement.InternalDocumentService.Converters;
using Grpc.Core;

namespace AppDocumentManagement.InternalDocumentService.Services
{
    public class InternalDocumentAPI : internalDocumentAPI.internalDocumentAPIBase
    {
        private readonly ILogger<InternalDocumentAPI> _logger;
        public InternalDocumentAPI(ILogger<InternalDocumentAPI> logger)
        {
            _logger = logger;
        }

        //InternalDocument
        public override Task<BoolReply> AddInternalDocument(MInternalDocument mInternalDocument, ServerCallContext context)
        {
            InternalDocument internalDocument = MInternalDocumentConverter.ConvertToInternalDocument(mInternalDocument);
            InternalDocumentController internalDocumentController = new InternalDocumentController();
            bool result = internalDocumentController.AddInternalDocument(internalDocument);
            BoolReply boolReply = new BoolReply()
            {
                Result = result,
            };
            return Task.FromResult(boolReply);
        }
        public override Task<MInternalDocumentList> GetInternalDocuments(EmptyRequest request, ServerCallContext context)
        {
            InternalDocumentController internalDocumentController = new InternalDocumentController();
            List<InternalDocument> internalDocuments = internalDocumentController.GetInternalDocuments();
            MInternalDocumentList mInternalDocumentList = new MInternalDocumentList();
            foreach (InternalDocument internalDocument in internalDocuments)
            {
                MInternalDocument mInternalDocument = MInternalDocumentConverter.ConvertToMInternalDocument(internalDocument);
                mInternalDocumentList.MIntrernalDocuments.Add(mInternalDocument);
            }
            return Task.FromResult(mInternalDocumentList);
        }

        public override Task<MInternalDocumentList> GetInternalDocumentsByEmployeeRecievedDocumentID(IDRequest request, ServerCallContext context)
        {
            InternalDocumentController internalDocumentController = new InternalDocumentController();
            List<InternalDocument> internalDocuments = internalDocumentController.GetInternalDocumentsByEmployeeRecievedDocumentID(request.ID);
            MInternalDocumentList mInternalDocumentList = new MInternalDocumentList();
            foreach (InternalDocument internalDocument in internalDocuments)
            {
                MInternalDocument mInternalDocument = MInternalDocumentConverter.ConvertToMInternalDocument(internalDocument);
                mInternalDocumentList.MIntrernalDocuments.Add(mInternalDocument);
            }
            return Task.FromResult(mInternalDocumentList);
        }

        public override Task<BoolReply> UpdateInternalDocument(MInternalDocument mInternalDocument, ServerCallContext context)
        {
            InternalDocument internalDocument = MInternalDocumentConverter.ConvertToInternalDocument(mInternalDocument);
            InternalDocumentController internalDocumentController = new InternalDocumentController();
            bool result = internalDocumentController.UpdateInternalDocument(internalDocument);
            BoolReply boolReply = new BoolReply()
            {
                Result = result,
            };
            return Task.FromResult(boolReply);
        }
        public override Task<BoolReply> RemoveInternalDocument(IDRequest request, ServerCallContext context)
        {
            InternalDocumentController internalDocumentController = new InternalDocumentController();
            bool result = internalDocumentController.RemoveInternalDocument(request.ID);
            BoolReply boolReply = new BoolReply()
            {
                Result = result,
            };
            return Task.FromResult(boolReply);
        }

        //InternalDocumentFile
        public override Task<BoolReply> AddInternalDocumentFile(MInternalDocumentFile mInternalDocumentFile, ServerCallContext context)
        {
            InternalDocumentFile internalDocumentFile = MInternalDocumentFileConverter.ConvertToInternalDocumentFile(mInternalDocumentFile);
            InternalDocumentFileController internalDocumentFileController = new InternalDocumentFileController();
            bool result = internalDocumentFileController.AddInternalDocumentFile(internalDocumentFile, internalDocumentFile.InternalDocumentID);
            BoolReply boolReply = new BoolReply()
            {
                Result = result,
            };
            return Task.FromResult(boolReply);
        }

        public override Task<BoolReply> AddInternalDocumentFiles(MInternalDocumentFileList mInternalDocumentFileList, ServerCallContext context)
        {
            List<InternalDocumentFile> internalDocumentFiles = new List<InternalDocumentFile>();
            foreach (MInternalDocumentFile file in mInternalDocumentFileList.MInternalDocumentFiles)
            {
                InternalDocumentFile internalDocumentFile = MInternalDocumentFileConverter.ConvertToInternalDocumentFile(file);
                internalDocumentFiles.Add(internalDocumentFile);
            }
            InternalDocumentFileController internalDocumentFileController = new InternalDocumentFileController();
            bool result = internalDocumentFileController.AddInternalDocumentFiles(internalDocumentFiles);
            BoolReply boolReply = new BoolReply()
            {
                Result = result,
            };
            return Task.FromResult(boolReply);
        }

        public override Task<MInternalDocumentFileList> GetInternalDocumentFiles(IDRequest request, ServerCallContext context)
        {
            InternalDocumentFileController internalDocumentFileController = new InternalDocumentFileController();
            List<InternalDocumentFile> internalDocumentFiles = internalDocumentFileController.GetInternalDocumentFiles(request.ID);
            MInternalDocumentFileList mInternalDocumentFileList = new MInternalDocumentFileList();
            foreach(InternalDocumentFile internalDocumentFile in internalDocumentFiles)
            {
                MInternalDocumentFile mInternalDocumentFile = MInternalDocumentFileConverter.ConvertToMInternalDocumentFile(internalDocumentFile);
                mInternalDocumentFileList.MInternalDocumentFiles.Add(mInternalDocumentFile);
            }
            return Task.FromResult(mInternalDocumentFileList);
        }

        public override Task<BoolReply> RemoveInternalDocumentFile(IDRequest request, ServerCallContext context)
        {
            InternalDocumentFileController internalDocumentFileController = new InternalDocumentFileController();
            bool result = internalDocumentFileController.RemoveInternalDocumentFile(request.ID);
            BoolReply boolReply = new BoolReply()
            {
                Result = result,
            };
            return Task.FromResult(boolReply);
        }
    }
}
