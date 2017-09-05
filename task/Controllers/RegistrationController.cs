using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using task.DAL.EF;
using task.DAL.Models.Information;
using task.DAL.Models.Users;

namespace task.Controllers
{
    public class RegistrationController : Controller
    {
        UnitOfWork unit = new UnitOfWork();



        public ActionResult Register()
        {
            return View();
        }

        // GET: Registration
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (TaskContext db = new TaskContext())
                {
                    user = unit.Users.GetAll().FirstOrDefault(u => u.Email == model.Email);
                }
                if (user == null)
                {
                    using (TaskContext db = new TaskContext())
                    {
                        unit.Users.Create(new User
                        {
                            Email = model.Email,
                            Password = model.Password,
                            RoleId = 1
                        });
                        unit.Save();
                        user = unit.Users.GetAll().Where(u => u.Email == model.Email && u.Password == model.Password).FirstOrDefault();
                    }
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Email, true);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "Пользователь с таким логином уже существует");
                }
            }
            return View(model);
        }
    }
}