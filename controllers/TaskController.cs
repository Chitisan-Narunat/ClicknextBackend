using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using kanbanboardAPI.Models;
using kanbanboardAPI.Data;
using kanbanboardAPI.AllAboutTask;

namespace kanbanboardAPI.AllAboutTask{
    [ApiController, Route("api/[controller]")]
    public class TaskController : ControllerBase{
        private readonly AppDbContext _context;
        public TaskController(AppDbContext context){
            _context = context;
        }


        [HttpPost] //requirement 5 สร้าง task
        public async Task<ActionResult> CreateTask(CreateTaskRQ dto){
            var column = await _context.Columns.FindAsync(dto.ColumnId);
            if (column == null){
                return NotFound(new{message = "Column not found"});
            }
            if (dto.AssigneeId.HasValue){
                var userExists = await _context.Users.AnyAsync(u => u.Id == dto.AssigneeId.Value);
                if (!userExists){
                    return NotFound(new{message = "AssigneeID does not exist"});
                }
            }
            
            var task = new TaskItem {
                Title = dto.Title,
                Description = dto.Description,
                ColumnId = dto.ColumnId,
                Order = dto.Order,
                AssigneeId = dto.AssigneeId,//เพิ่ม สมาชิกผู้รับผิดชอบ requirement 6
                Tags = dto.Tags, //เพิ่ม tag ในtask requirement 5 b
                DueDate = dto.DueDate,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            _context.TaskItems.Add(task);
            await _context.SaveChangesAsync();

            return Ok(task);
        }


        [HttpDelete("{id}")] //requirement 5 ลบ task จาก id
        public async Task<ActionResult> DeleteTask(int id){
            var task = await _context.TaskItems.FindAsync(id);
            if (task == null){
                return NotFound(new { message = "Task not found" });
            }

            _context.TaskItems.Remove(task);
            await _context.SaveChangesAsync();

            return Ok(new{message = "Task deleted successfully"});
        }


        [HttpPut("{id}")] //requirement 5 แก้ไขชื่อ + รายละเอียด task
        public async Task<IActionResult> ReDetailTask(int id,ReDetailTaskRQ dto){
            var task = await _context.TaskItems.FindAsync(id);
            if (task == null){
                return NotFound(new { message = "Task not found" });
            }
             if (dto.AssigneeId.HasValue){
                var userExists = await _context.Users.AnyAsync(u => u.Id == dto.AssigneeId.Value);
                if (!userExists){
                    return NotFound(new { message = "AssigneeID does not exist" });
                }
            }
            
            task.Title = dto.Title;
            task.Description = dto.Description;
            task.Order = dto.Order;
            task.AssigneeId = dto.AssigneeId;//แก้ไข สมาชิกผู้รับผิดชอบ requirement 6
            task.Tags = dto.Tags; //แก้ไข tag ในtaskk requirement 5 b
            task.DueDate = dto.DueDate;
            task.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(task);
        }

        
        [HttpPut("{id}/move")] //requirement 5 ปรับตำแหน่ง task
        public async Task<IActionResult> MoveTask(int id, MoveTaskRQ dto){
            var task = await _context.TaskItems.FindAsync(id);
            if (task == null){
                return NotFound(new{message = "Task not found"});
            }
            var targetColumn = await _context.Columns.FindAsync(dto.NewColumnId);
            if (targetColumn == null){
                return NotFound(new{message = "Target column not found"});
            }

            task.ColumnId = dto.NewColumnId;
            task.Order = dto.NewOrder;
            task.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(task);
        }
    }
}