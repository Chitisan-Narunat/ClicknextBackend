using System.Text.Json.Serialization;

namespace kanbanboardAPI.Models{
    public class Column{ //หัวข้อของboard
        public int Id { get; set; } //pk รหัสประจำตัวของคอลัมน์
        public string? Name { get; set; }
        public int Order { get; set; }
        public int BoardId { get; set; } //fk เชื่อมโยงกับ Board ที่คอลัมน์นี้อยู่

        [JsonIgnore]
        public Board? Board { get; set; }

        public List<TaskItem> Tasks { get; set; } = new();
        
    }
}