using IT_Service_Desk.Models;

namespace IT_Service_Desk.Data
{
    public static class DbSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            // If the database is already populated, skip seeding
            if (context.WorkOrders.Any()) return;

            var workOrders = new List<WorkOrder>
            {
                // EMERGENCY (The "Urgent" cases)
                new WorkOrder {
                    Title = "HVAC Failure - Suite 102",
                    Description = "Unit 4A blowing hot air. Server room temp rising.",
                    Status = WorkOrderStatus.Open,
                    Urgency = WorkOrderUrgency.Urgent,
                    CreatedBy = "IT_Manager",
                    AssignedTo = "Unassigned"
                },
                new WorkOrder {
                    Title = "Water Leak - Breakroom",
                    Description = "Ceiling tile saturated near the vending machine.",
                    Status = WorkOrderStatus.InProgress,
                    Urgency = WorkOrderUrgency.High,
                    CreatedBy = "Office_Mgr",
                    AssignedTo = "Nick"
                },
                
                // MAINTENANCE (The "Medium/Routine" cases)
                new WorkOrder {
                    Title = "Parking Gate Repair",
                    Description = "East entrance gate motor sticking during morning peak.",
                    Status = WorkOrderStatus.Open,
                    Urgency = WorkOrderUrgency.High,
                    CreatedBy = "Security_Lead",
                    AssignedTo = "Unassigned"
                },
                new WorkOrder {
                    Title = "Furniture Assembly",
                    Description = "Assemble 4 new standing desks for Suite 200.",
                    Status = WorkOrderStatus.InProgress,
                    Urgency = WorkOrderUrgency.Low,
                    CreatedBy = "HR_Director",
                    AssignedTo = "Nick"
                },

                // COMPLETED (The "Done" cases for history)
                new WorkOrder {
                    Title = "Fire Extinguisher Check",
                    Description = "Quarterly safety inspection for floor 1 & 2.",
                    Status = WorkOrderStatus.Closed,
                    Urgency = WorkOrderUrgency.Low,
                    CreatedBy = "Safety_Officer",
                    AssignedTo = "System"
                },
                new WorkOrder {
                    Title = "Replace Lobby Lighting",
                    Description = "Replace failed LED strips in main entryway.",
                    Status = WorkOrderStatus.Closed,
                    Urgency = WorkOrderUrgency.Medium,
                    CreatedBy = "Ops_Manager",
                    AssignedTo = "Nick"
                }
            };

            context.WorkOrders.AddRange(workOrders);
            context.SaveChanges();
        }
    }
}