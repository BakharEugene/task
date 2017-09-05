using System.Data.Entity;
using task.DAL.Models.Users;

namespace task.DAL.EF
{
    public class TaskContext : DbContext
    {
        public TaskContext() : base("TaskContext")
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }

}
