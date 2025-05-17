namespace kanbanboardAPI.Models{
    public class BoardMember{
        public int UserId { get; set; } //fk เชื่อมโยงกับ User เป็นสมาชิกของบอร์ด
        public User? User { get; set; }

        public int BoardId { get; set; } //fk เชื่อมโยงกับ Board ที่สมาชิกนี้อยู่
        public Board? Board { get; set; }

        public string Role { get; set; } = "Member";
        
    }
}