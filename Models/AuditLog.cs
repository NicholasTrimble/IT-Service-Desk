
using System.ComponentModel.DataAnnotations;

namespace IT_Service_Desk.Models
{
    public class AuditLog
    {
        public int Id { get; set; }
        public int WorkOrderId { get; set; }
        public string Action { get; set; } = string.Empty; 
        public string ChangedBy { get; set; } = "System"; 
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}