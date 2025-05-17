using System.Data;

namespace kanbanboardAPI.Models{
    public class Board{
        public int Id { get; set; } //pk รหัสประจำตัวของบอร์ด
        public string? BoardName { get; set; } 
        public int? OwnerId { get; set; }
        public User? Owner { get; set; } 

        public DateTime FirstCreated { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

        public List<BoardMember> Members { get; set; } = new();
        public List<Column> Columns { get; set; } = new();
        
    }
}