using kanbanboardAPI.Models;

namespace kanbanboardAPI.AllAboutColumn{ //requirement 4
    public class CreateColumnRQ{ //สร้าง
        public string? Name{get; set;}
        public int BoardId{get; set;}
        public int Order{get; set;}
    }

    public class ReNameColumnRQ{ //เปลี่ยนชื่อ
        public string NewColumnName{get; set;} = "";
        public int Order{get; set;}
        public int BoardId {get; set;}
    }
}