namespace OritsoTaskApp.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime DueDate { get; set; }

        public string Status { get; set; } = "Pending";

        // ✅ Optional remarks with default
        public string Remarks { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public DateTime UpdatedOn { get; set; } = DateTime.Now;

        // ✅ Auto-set by server, not required from client
        public int CreatedById { get; set; }

        public string CreatedByName { get; set; } = string.Empty;

        public int LastUpdatedById { get; set; }

        public string LastUpdatedByName { get; set; } = string.Empty;
    }
}
