namespace AppDocumentManagement.DB.Entities
{
    public class ProductionTaskComment
    {
        public int ProductionTaskCommentID { get; set; }
        public DateTime ProductionTaskCommentDate { get; set; }
        public string ProductionTaskCommentText { get; set; }
        public ProductionTask ProductionTask {  get; set; }
        public int ProductionTaskID { get; set;}
        public Employee Employee { get; set; }
        public int EmployeeID { get; set;}
    }
}
