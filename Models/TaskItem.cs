using System.Text.Json.Serialization;

namespace kanbanboardAPI.Models
{
    public class TaskItem{ //สิ่งที่ต้องทำในcollumn
        public int Id { get; set; } //pk รหัสประจำตัวของงาน (Task)

        public string? Title { get; set; }
        public string? Description { get; set; }

        public int Order { get; set; }
        public int ColumnId { get; set; } //fk 
        [JsonIgnore]
        public Column? Column { get; set; }
        public int? AssigneeId { get; set; }
        [JsonIgnore]
        public User? Assignee { get; set; }
        public List<string> Tags { get; set; } = new List<string>();

        public DateTime? DueDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    }
}