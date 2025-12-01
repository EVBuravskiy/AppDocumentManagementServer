using AppDocumentManagement.DB.Controllers;
using AppDocumentManagement.DB.Entities;
using AppDocumentManagement.Service.Converters;
using Grpc.Core;

namespace AppDocumentManagement.Service.Services
{
    public class DepartmentAPI : departmentApi.departmentApiBase
    {
        private readonly ILogger<DepartmentAPI> _logger;
        public DepartmentAPI(ILogger<DepartmentAPI> logger)
        {
            _logger = logger;
        }

        public override Task<DBoolReply> AddDepartment(MDepartment mDepartment, ServerCallContext context)
        {
            DepartmentController departmentController = new DepartmentController();
            Department department = MDepartmentConverter.ConvertToDepartment(mDepartment);
            bool result = departmentController.AddDepartment(department);
            DBoolReply boolReply = new DBoolReply() { Result = result };
            return Task.FromResult(boolReply);
        }

        public override Task<DepartmentListReply> GetAllDepartments(EmptyRequest request, ServerCallContext context)
        {
            DepartmentController departmentController = new DepartmentController();
            List<Department> departments = departmentController.GetAllDepartments();
            DepartmentListReply departmentListReply = new DepartmentListReply();
            foreach (Department department in departments)
            {
                MDepartment mDepartment = MDepartmentConverter.ConvertToMDepartment(department);
                departmentListReply.MDepartments.Add(mDepartment);
            }
            return Task.FromResult(departmentListReply);
        }

        public override Task<MDepartment> GetDepartmentByID(IDRequest request, ServerCallContext context)
        {
            DepartmentController departmentController = new DepartmentController();
            Department department = departmentController.GetDepartmentByID(request.ID);
            MDepartment mDepartment = MDepartmentConverter.ConvertToMDepartment(department);
            return Task.FromResult(mDepartment);
        }

        public override Task<DBoolReply> UpdateDepartment(MDepartment mDepartment, ServerCallContext context)
        {
            Department department = MDepartmentConverter.ConvertToDepartment(mDepartment);
            DepartmentController departmentController = new DepartmentController();
            bool result = departmentController.UpdateDepartment(department);
            DBoolReply dBoolReply = new DBoolReply()
            {
                Result = result
            };
            return Task.FromResult(dBoolReply);
        }

        public override Task<DBoolReply> RemoveDepartment(IDRequest request, ServerCallContext context)
        {
            DepartmentController departmentController = new DepartmentController();
            bool result = departmentController.RemoveDepartment(request.ID);
            DBoolReply dBoolReply = new DBoolReply()
            {
                Result = result
            };
            return Task.FromResult(dBoolReply);
        }
    }
}
