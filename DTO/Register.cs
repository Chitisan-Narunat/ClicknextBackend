using kanbanboardAPI.Models;

namespace kanbanboardAPI.Register{ //requirement1
    public class RegisterRQ{
        public string Username {get; set;} = "";
        public string Password {get; set;} = "";
        public string Email {get; set;} = "";
        
    }
}