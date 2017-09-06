using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using task.DAL.EF;
using task.DAL.Models.Comments;
using task.DAL.Models.Information;
using task.DAL.Models.Users;

namespace task.Controllers
{
    public class AccountController : Controller
    {
        UnitOfWork unit = new UnitOfWork();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                User user = null;

                user = unit.Users.GetAll().Where(u => u.Email == model.Email && u.Password == model.Password).FirstOrDefault();

                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Email", "Пользователя с таким логином и паролем нет");
                }
            }

            return View(model);
        }
        [Authorize]
        public ActionResult AllComments(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var comments = unit.Comments.GetAll();
            comments = sorting(comments, sortOrder).ToList();
            return View(comments);
        }
        public ActionResult Profile(string sortOrder)
        {
            User user = unit.Users.GetAll().Where(x => x.Email == User.Identity.Name).FirstOrDefault();
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var comments = user.Comments.AsEnumerable();
            
            user.Comments = sorting(comments, sortOrder).ToList();

            return View(user);
        }

        private IEnumerable<Comment> sorting(IEnumerable<Comment> comments,string sortOrder)
        {
            switch (sortOrder)
            {
                case "id_desc":
                    comments = comments.OrderByDescending(s => s.Id);
                    break;
                case "Date":
                    comments = comments.OrderBy(s => s.time);
                    break;
                case "date_desc":
                    comments = comments.OrderByDescending(s => s.time);
                    break;
                default:
                    comments = comments.OrderBy(s => s.Id);
                    break;
            }
            return comments;
        }
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}