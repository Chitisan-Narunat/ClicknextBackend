using kanbanboardAPI.Models;

namespace kanbanboardAPI.AllAboutTask{ //requirement 5 
    public class CreateTaskRQ{ //สร้าง
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int ColumnId { get; set; }
        public int Order { get; set; } = 0;
        public int? AssigneeId { get; set; }
        public List<string> Tags { get; set; } = new();
        public DateTime? DueDate { get; set; }
    }

    public class ReDetailTaskRQ { //แก้ไขชื่อช้อมูล
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int Order { get; set; }
        public int? AssigneeId { get; set; }
        public List<string> Tags { get; set; } = new();
        public DateTime? DueDate { get; set; }
    }

    public class MoveTaskRQ{ //ปรับตำแหน่ง
        public int NewColumnId { get; set; }
        public int NewOrder { get; set; }
    }
}