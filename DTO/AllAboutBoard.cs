using kanbanboardAPI.Models;

namespace kanbanboardAPI.AllAboutBoard{ //requirement2
    public class CreateBoardRQ{ //สร้าง
        public string BoardName {get; set;} ="";
        public int OwnerId {get; set;}
    }
    
    public class RenameBoardRQ{ //เปลี่ยนชื่อ
        public string NewBoardName { get; set; } = "";
    }
    
    public class InviteMemberRQ{ //เชิญ requirement3
        public int BoardId { get; set; }
        public int UserId { get; set; }
        public string Role { get; set; } = "member";
    }
}