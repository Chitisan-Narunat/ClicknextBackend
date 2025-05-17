using Microsoft.EntityFrameworkCore;
using kanbanboardAPI.Models;

namespace kanbanboardAPI.Data{
    public class AppDbContext : DbContext{
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users{get; set;}
        public DbSet<Board> Boards{get; set;}
        public DbSet<BoardMember> boardMembers{get; set;}
        public DbSet<Column> Columns{get; set;}
        public DbSet<TaskItem> TaskItems{get; set;}
    
        protected override void OnModelCreating(ModelBuilder modelBuilder){

            modelBuilder.Entity<BoardMember>()
            .HasKey(bm => new { bm.UserId, bm.BoardId }); //กำหนดสองอย่างเป็น PM ของ Boardmember

            modelBuilder.Entity<BoardMember>()
            .HasOne(bm => bm.Board) 
            .WithMany(b => b.Members) 
            .HasForeignKey(bm => bm.BoardId); 

            modelBuilder.Entity<BoardMember>()
            .HasOne(bm => bm.User)
            .WithMany(u => u.Boards)
            .HasForeignKey(bm => bm.UserId);

            modelBuilder.Entity<Board>()
            .HasOne(b => b.Owner)
            .WithMany()
            .HasForeignKey(b => b.OwnerId);

            modelBuilder.Entity<Column>()
            .HasOne(c => c.Board)
            .WithMany(b => b.Columns)
            .HasForeignKey(c => c.BoardId);

            modelBuilder.Entity<TaskItem>()
            .HasOne(t => t.Column)
            .WithMany(c => c.Tasks)
            .HasForeignKey(t => t.ColumnId);

            modelBuilder.Entity<TaskItem>()
            .HasOne(t => t.Assignee)
            .WithMany()
            .HasForeignKey(t => t.AssigneeId);

            modelBuilder.Entity<TaskItem>()
            .Property(t => t.Tags)
            .HasConversion(v => string.Join(",", v), v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
        }
    }
}