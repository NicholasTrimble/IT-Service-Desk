using System.ComponentModel.DataAnnotations;

namespace IT_Service_Desk.Models
{
    // Define the options for the whole app here
    public enum WorkOrderStatus { Open, InProgress, Closed }
    public enum WorkOrderUrgency { Low, Medium, High, Urgent }

    public class WorkOrder
    {
        public int Id { get; set; } // SQL will handle this (Primary Key)

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        // Use the Enums we defined above
        [Required]
        public WorkOrderStatus Status { get; set; } = WorkOrderStatus.Open;

        [Required]
        public WorkOrderUrgency Urgency { get; set; } = WorkOrderUrgency.Medium;

        [Display(Name = "Created By")]
        public string? CreatedBy { get; set; }

        [Display(Name = "Assigned To")]
        public string AssignedTo { get; set; } = "Unassigned";

        [Display(Name = "Date Created")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<AuditLog> AuditLogs { get; set; } = new();
    }
}