using System;
using task.DAL.EF;
using task.DAL.Repositories;

namespace task.DAL.Models.Information
{
    public class UnitOfWork:IDisposable
    {
        TaskContext db = new TaskContext();
        private RoleRepository roleRepository;
        private UserRepository userRepository;
        private CommentRepository commentRepository;
        public CommentRepository Comments
        {
            get
            {
                if (commentRepository == null)
                    commentRepository= new CommentRepository(db);
                return commentRepository;
            }

        }


        public RoleRepository Roles
        {
            get
            {
                if (roleRepository == null)
                    roleRepository = new RoleRepository(db);
                return roleRepository;
            }

        }


        public UserRepository Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
