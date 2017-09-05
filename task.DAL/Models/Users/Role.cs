using System.ComponentModel.DataAnnotations;

namespace task.DAL.Models.Users
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
