using Microsoft.EntityFrameworkCore;

namespace TodoAPI.Models
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {

        }

        public DbSet<TodoList>? TodoList { get; set; }

    }

    
}
 