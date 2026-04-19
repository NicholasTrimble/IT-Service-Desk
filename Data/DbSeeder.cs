using IT_Service_Desk.Models;

namespace IT_Service_Desk.Data
{
    public static class DbSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
           
            if (context.Tickets.Any()) return;

            var tickets = new List<Ticket>
            {
                new Ticket { Title = "Printer on Fire", Description = "Room 302, please hurry.", Status = TicketStatus.Open, Priority = TicketPriority.Urgent, CreatedBy = "manager@test.com", AssignedTo = "Unassigned", CreatedAt = DateTime.Now.AddHours(-5) },
                new Ticket { Title = "New Laptop Setup", Description = "Onboarding for new dev.", Status = TicketStatus.InProgress, Priority = TicketPriority.Medium, CreatedBy = "hr@test.com", AssignedTo = "Nick", CreatedAt = DateTime.Now.AddDays(-1) },
                new Ticket { Title = "Password Reset", Description = "User locked out of AD.", Status = TicketStatus.Closed, Priority = TicketPriority.Low, CreatedBy = "user@test.com", AssignedTo = "System", CreatedAt = DateTime.Now.AddDays(-2) },
                new Ticket { Title = "Wi-Fi is slow", Description = "Happening in the breakroom.", Status = TicketStatus.Open, Priority = TicketPriority.High, CreatedBy = "guest@test.com", AssignedTo = "Unassigned", CreatedAt = DateTime.Now.AddMinutes(-30) }
            };

            context.Tickets.AddRange(tickets);
            context.SaveChanges();
        }
    }
}