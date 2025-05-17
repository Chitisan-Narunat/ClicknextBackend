using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using kanbanboardAPI.Models;
using kanbanboardAPI.AllAboutColumn;
using kanbanboardAPI.Data;

namespace kanbanboardAPI.Controllers{
    [ApiController, Route("api/[controller]")]
    public class ColumnController : ControllerBase{
        private readonly AppDbContext _context;
        public ColumnController(AppDbContext context){
            _context = context;
        }


        [HttpPost] //requirement 4 สร้าง column
        public async Task<IActionResult> CreateColumn(CreateColumnRQ dto){
            var board = await _context.Boards.FindAsync(dto.BoardId);
            if(board == null){
                return NotFound("Board not found");
            }

            var column = new Column{
                Name = dto.Name,
                BoardId = dto.BoardId,
                Order = dto.Order,
            };

            _context.Columns.Add(column);
            
            await _context.SaveChangesAsync();

            return Ok(column);
        }


        [HttpDelete("{id}")] //requirement 4 ลบ column จาก id
        public async Task<IActionResult> DeleteColumn(int id){
            var column = await _context.Columns.FindAsync(id);
            if (column == null){
                return NotFound(new{message = "Column not found"});
            }

            _context.Columns.Remove(column);

            await _context.SaveChangesAsync();

            return Ok(new{message = "Deleted Successfully"});
        }


        [HttpPut("{id}")] //requirement 4 แก้ไขชื่อ column
        public async Task<IActionResult> ReNameColumn(int id,ReNameColumnRQ dto){
            var column = await _context.Columns.FindAsync(id);
            if(column == null){
                return NotFound(new{message = "Column not found"});
            }

            column.Name = dto.NewColumnName;

            await _context.SaveChangesAsync(); 

            return Ok(column);
        }
    }
}