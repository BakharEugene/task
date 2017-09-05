using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using task.DAL.Models.Comments;

namespace task.DAL.Models.Users
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [ForeignKey("Role")]
        public int? RoleId { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
