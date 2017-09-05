using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using task.DAL.EF;
using task.DAL.Models.Users;

namespace task.DAL.Repositories
{
    public class RoleRepository : IRepository<Role>
    {

        private TaskContext _applicationDbContext;

        public RoleRepository(TaskContext context)
        {
            this._applicationDbContext = context;
        }

        public IEnumerable<Role> GetAll()
        {
            List<Role> Roles = _applicationDbContext.Roles.ToList();
            return Roles.ToList();
        }

        public Role GetById(int? id)
        {
            return _applicationDbContext.Roles.Find(id);
        }

        public void Create(Role item)
        {
            _applicationDbContext.Roles.Add(item);
        }

        public void Update(Role item)
        {
            _applicationDbContext.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            if (_applicationDbContext.Roles.Find(id) != null)
            {
                _applicationDbContext.Roles.Remove(_applicationDbContext.Roles.Find(id));
            }
        }
    }
}
