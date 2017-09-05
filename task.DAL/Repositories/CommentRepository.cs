using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using task.DAL.EF;
using task.DAL.Models.Comments;

namespace task.DAL.Repositories
{
    public class CommentRepository:IRepository<Comment>
    {
        private TaskContext _applicationDbContext;

        public CommentRepository(TaskContext context)
        {
            this._applicationDbContext = context;
        }

        public IEnumerable<Comment> GetAll()
        {
            List<Comment> Comments = _applicationDbContext.Comments.ToList();
            return Comments.ToList();
        }

        public Comment GetById(int? id)
        {
            return _applicationDbContext.Comments.Find(id);
        }

        public void Create(Comment item)
        {
            _applicationDbContext.Comments.Add(item);
        }

        public void Update(Comment item)
        {
            _applicationDbContext.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            if (_applicationDbContext.Comments.Find(id) != null)
            {
                _applicationDbContext.Comments.Remove(_applicationDbContext.Comments.Find(id));
            }
        }
    }
}
