using System.ComponentModel.DataAnnotations;

namespace kanbanboardAPI.Models{
    public class User{
        public int Id {get; set;} //pk

        [Required] 
        public string Username { get; set; } = string.Empty; 
        [Required]
        public string Email { get; set; } = string.Empty; 
        [Required]
        public string PasswordHash { get; set; } = string.Empty; 
        public List<BoardMember> Boards {get; set;} = new(); 

    }    
}