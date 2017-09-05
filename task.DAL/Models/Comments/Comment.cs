using System.ComponentModel.DataAnnotations;
using task.DAL.Models.Users;
using System;

namespace task.DAL.Models.Comments
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public int? UserId { get; set; }
        public virtual User User{ get; set; }
        public DateTime time { get; set; }
    }
}
