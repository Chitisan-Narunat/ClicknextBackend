using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using kanbanboardAPI.Models;
using kanbanboardAPI.Data;
using kanbanboardAPI.AllAboutBoard;

namespace kanbanboardAPI.Controllers{
    [ApiController, Route("api/[controller]")]
    public class BoardController : ControllerBase {
        private readonly AppDbContext _context;
        public BoardController(AppDbContext context) {
            _context = context;
        }


        [HttpPost] //requirement 2 สร้างboard
        public async Task<IActionResult> CreateBoard(CreateBoardRQ dto){
            var owner = await _context.Users.FindAsync(dto.OwnerId);
            if (owner == null){
                return NotFound(new{message = " OwnerID does not exist"});
            }
            
            var board = new Board{
                BoardName = dto.BoardName,
                OwnerId = dto.OwnerId,
                FirstCreated = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow,
            };

            _context.Boards.Add(board);
            await _context.Entry(board)
                .Reference(b => b.Owner)
                .LoadAsync();

            await _context.SaveChangesAsync();

            return Ok(board);
        }


        [HttpDelete("{id}")] ////requirement 2 ลบ board ผ่าน id
        public async Task<IActionResult> DeleteBoard(int id){
            var board = await _context.Boards.FindAsync(id);
            if (board == null){
                return NotFound(new{message = "Board not found"});
            }

            _context.Boards.Remove(board);

            await _context.SaveChangesAsync();

            return Ok(new{message = "Deleted Successfully"});
        }


        [HttpPut("{id}")] ////requirement 2 เปลี่ยนชื่อboard ผ่าน id
        public async Task<IActionResult> RenameBoard(int id, RenameBoardRQ dto){
            var board = await _context.Boards.FindAsync(id);
            if(board == null){
                return NotFound(new{message = "Board not found"});
            }

            board.BoardName = dto.NewBoardName;
            board.LastUpdated = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(board);
        }


        [HttpPost("Invite")] //requirement 3 เชิญคนเข้าboard
        public async Task<IActionResult> InviteMember(InviteMemberRQ dto){
            var board = await _context.Boards.FindAsync(dto.BoardId);
            var user = await _context.Users.FindAsync(dto.UserId);

            if(board == null || user == null){
                return NotFound(new{message = "No Board and Member found"});
            }
            
            var exists = await _context.boardMembers
                .AnyAsync(bm => bm.BoardId == dto.BoardId && bm.UserId == dto.UserId);
            if(exists){
                return BadRequest(new{message = "User is already a member of this board"});
            }

            var newMember = new BoardMember{
                BoardId = dto.BoardId,
                UserId = dto.UserId,
                Role = dto.Role,
            };

            _context.boardMembers.Add(newMember);
            await _context.SaveChangesAsync();

            return Ok(new{message = "User invited successfully"});
        }
    }
}
