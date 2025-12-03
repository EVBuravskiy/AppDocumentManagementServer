using AppDocumentManagement.DB.Controllers;
using AppDocumentManagement.DB.Entities;
using AppDocumentManagement.ExternalDocumentService;
using AppDocumentManagement.ExternalDocumentService.Converters;
using Grpc.Core;

namespace AppDocumentManagement.ExternalDocumentService.Services
{
    public class ExternalDocumentAPI : externalDocumentAPI.externalDocumentAPIBase
    {
        private readonly ILogger<ExternalDocumentAPI> _logger;
        public ExternalDocumentAPI(ILogger<ExternalDocumentAPI> logger)
        {
            _logger = logger;
        }

        //ExternalDocument
        public override Task<BoolReply> AddExternalDocument(MExternalDocument mExternalDocument, ServerCallContext context)
        {
            ExternalDocument externalDocument = MExternalDocumentConverter.ConvertToExternalDocument(mExternalDocument);
            ExternalDocumentController externalDocumentController = new ExternalDocumentController();
            bool result = externalDocumentController.AddExternalDocument(externalDocument);
            BoolReply boolReply = new BoolReply()
            {
                Result = result,
            };
            return Task.FromResult(boolReply);
        }

        public override Task<MExternalDocumentList> GetAllExternalDocuments(EmptyRequest request, ServerCallContext context)
        {
            ExternalDocumentController externalDocumentController = new ExternalDocumentController();
            List<ExternalDocument> externalDocuments = externalDocumentController.GetAllExternalDocuments();
            MExternalDocumentList mExternalDocumentList = new MExternalDocumentList();
            foreach (ExternalDocument externalDocument in externalDocuments)
            {
                MExternalDocument mExternalDocument = MExternalDocumentConverter.ConvertToMExternalDocument(externalDocument);
                mExternalDocumentList.MExternalDocuments.Add(mExternalDocument);
            }
            return Task.FromResult(mExternalDocumentList);
        }

        public override Task<MExternalDocumentList> GetExternalDocumentsByEmployeeReceivedDocumentID(IDRequest request, ServerCallContext context)
        {
            ExternalDocumentController externalDocumentController = new ExternalDocumentController();
            List<ExternalDocument> externalDocuments = externalDocumentController.GetExternalDocumentsByEmployeeReceivedDocumentID(request.ID);
            MExternalDocumentList mExternalDocumentList = new MExternalDocumentList();
            foreach (ExternalDocument externalDocument in externalDocuments)
            {
                MExternalDocument mExternalDocument = MExternalDocumentConverter.ConvertToMExternalDocument(externalDocument);
                mExternalDocumentList.MExternalDocuments.Add(mExternalDocument);
            }
            return Task.FromResult(mExternalDocumentList);
        }

        public override Task<BoolReply> UpdateExternalDocument(MExternalDocument mExternalDocument, ServerCallContext context)
        {
            ExternalDocument externalDocument = MExternalDocumentConverter.ConvertToExternalDocument(mExternalDocument);
            ExternalDocumentController externalDocumentController = new ExternalDocumentController();
            bool result = externalDocumentController.UpdateDocument(externalDocument);
            BoolReply boolReply = new BoolReply()
            {
                Result = result,
            };
            return Task.FromResult(boolReply);
        }

        public override Task<BoolReply> RemoveExternalDocument(IDRequest request, ServerCallContext context)
        {
            ExternalDocumentController externalDocumentController = new ExternalDocumentController();
            bool result = externalDocumentController.RemoveDocument(request.ID);
            BoolReply boolReply = new BoolReply()
            {
                Result = result,
            };
            return Task.FromResult(boolReply);
        }

        //ExternalDocumentFile
        public override Task<BoolReply> AddExternalDocumentFile(MExternalDocumentFile mExternalDocumentFile, ServerCallContext context)
        {
            ExternalDocumentFile externalDocumentFile = MExternalDocumentFileConverter.ConvertToExternalDocumentFile(mExternalDocumentFile);
            ExternalDocumentFileController externalDocumentFileController = new ExternalDocumentFileController();
            bool result = externalDocumentFileController.AddExternalDocumentFile(externalDocumentFile, externalDocumentFile.ExternalDocumentID);
            BoolReply boolReply = new BoolReply()
            {
                Result = result,
            };
            return Task.FromResult(boolReply);
        }

        public override Task<BoolReply> AddExternalDocumentFiles(MExternalDocumentFileList mExternalDocumentFileList, ServerCallContext context)
        {
            List<ExternalDocumentFile> externalDocumentFiles = new List<ExternalDocumentFile>();
            int documentID = 0;
            foreach (MExternalDocumentFile mExternalDocumentFile in mExternalDocumentFileList.MEsternalDocumentFiles)
            {
                ExternalDocumentFile externalDocumentFile = MExternalDocumentFileConverter.ConvertToExternalDocumentFile(mExternalDocumentFile);
                externalDocumentFiles.Add(externalDocumentFile);
                documentID = externalDocumentFile.ExternalDocumentID;
            }
            ExternalDocumentFileController externalDocumentFileController = new ExternalDocumentFileController();
            bool result = externalDocumentFileController.AddExternalDocumentFiles(externalDocumentFiles, documentID);
            BoolReply boolReply = new BoolReply()
            {
                Result = result,
            };
            return Task.FromResult(boolReply);
        }

        public override Task<MExternalDocumentFileList> GetExternalDocumentFiles(IDRequest request, ServerCallContext context)
        {
            ExternalDocumentFileController externalDocumentFileController = new ExternalDocumentFileController();
            List<ExternalDocumentFile> externalDocumentFiles = externalDocumentFileController.GetExternalDocumentFiles(request.ID);
            MExternalDocumentFileList mExternalDocumentFileList = new MExternalDocumentFileList();
            foreach (ExternalDocumentFile file in externalDocumentFiles)
            {
                MExternalDocumentFile mExternalDocumentFile = MExternalDocumentFileConverter.ConvertToMExternalDocumentFile(file);
                mExternalDocumentFileList.MEsternalDocumentFiles.Add(mExternalDocumentFile);
            }
            return Task.FromResult(mExternalDocumentFileList);
        }

        public override Task<BoolReply> RemoveExternalDocumentFile(IDRequest request, ServerCallContext context)
        {
            ExternalDocumentFileController externalDocumentFileController = new ExternalDocumentFileController();
            bool result = externalDocumentFileController.RemoveExternalDocumentFile(request.ID);
            BoolReply boolReply = new BoolReply()
            {
                Result = result,
            };
            return Task.FromResult(boolReply);
        }

        //ContractorCompany
        public override Task<BoolReply> AddContractorCompany(MContractorCompany mContractorCompany, ServerCallContext context)
        {
            ContractorCompany contractorCompany = MContractorCompanyConverter.ConvertToContractorCompany(mContractorCompany);
            ContractorCompanyController contractorCompanyController = new ContractorCompanyController();
            bool result = contractorCompanyController.AddContractorCompany(contractorCompany);
            BoolReply boolReply = new BoolReply()
            {
                Result = result,
            };
            return Task.FromResult(boolReply);
        }

        public override Task<MContractorCompanyList> GetContractorCompanies(EmptyRequest emptyRequest, ServerCallContext context)
        {
            ContractorCompanyController contractorCompanyController = new ContractorCompanyController();
            List<ContractorCompany> contractorCompanies = contractorCompanyController.GetContractorCompanies();
            MContractorCompanyList mContractorCompanyList = new MContractorCompanyList();
            foreach (ContractorCompany contractorCompany in contractorCompanies)
            {
                MContractorCompany mContractorCompany = MContractorCompanyConverter.ConvertToMContractorCompany(contractorCompany);
                mContractorCompanyList.MContractorCompanyes.Add(mContractorCompany);
            }
            return Task.FromResult(mContractorCompanyList);
        }
        public override Task<MContractorCompanyList> GetAvailableContractorCompanies(EmptyRequest emptyRequest, ServerCallContext context)
        {
            ContractorCompanyController contractorCompanyController = new ContractorCompanyController();
            List<ContractorCompany> contractorCompanies = contractorCompanyController.GetAvailableContractorCompanies();
            MContractorCompanyList mContractorCompanyList = new MContractorCompanyList();
            foreach (ContractorCompany contractorCompany in contractorCompanies)
            {
                MContractorCompany mContractorCompany = MContractorCompanyConverter.ConvertToMContractorCompany(contractorCompany);
                mContractorCompanyList.MContractorCompanyes.Add(mContractorCompany);
            }
            return Task.FromResult(mContractorCompanyList);
        }

        public override Task<MContractorCompany> GetContractorCompanyByID(IDRequest request, ServerCallContext context)
        {
            ContractorCompanyController contractorCompanyController = new ContractorCompanyController();
            ContractorCompany contractorCompany = contractorCompanyController.GetContractorCompanyByID(request.ID);
            MContractorCompany mContractorCompany = MContractorCompanyConverter.ConvertToMContractorCompany(contractorCompany);
            return Task.FromResult(mContractorCompany);
        }

        public override Task<BoolReply> UpdateContractorCompany(MContractorCompany mContractorCompany, ServerCallContext context)
        {
            ContractorCompany contractorCompany = MContractorCompanyConverter.ConvertToContractorCompany(mContractorCompany);
            ContractorCompanyController contractorCompanyController = new ContractorCompanyController();
            bool result = contractorCompanyController.UpdateContractorCompany(contractorCompany);
            BoolReply boolReply = new BoolReply()
            {
                Result = result,
            };
            return Task.FromResult(boolReply);
        }

        public override Task<BoolReply> RemoveContractorCompany(IDRequest request, ServerCallContext context)
        {
            ContractorCompanyController contractorCompanyController = new ContractorCompanyController();
            bool result = contractorCompanyController.RemoveContractorCompany(request.ID);
            BoolReply boolReply = new BoolReply()
            {
                Result = result,
            };
            return Task.FromResult(boolReply);
        }
    }
}
