using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using task.DAL.Models.Comments;
using task.DAL.Models.Information;
using task.DAL.Models.Users;

namespace task.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddComment(string comment)
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                Comment post = new Comment
                {
                    Text = comment
                };
                User user = null;
                if (User.Identity.IsAuthenticated)
                {
                    user = unit.Users.GetAll().FirstOrDefault(x => x.Email == User.Identity.Name);
                }
                else
                {
                    if ((User)(Session["CurrentUnauthorizedUser"]) == null)
                    {
                        user = new User();
                        user.Email = "";
                        user.Password = "";
                        user.Comments = new List<Comment>();
                        user.RoleId = 3;
                        unit.Users.Create(user);
                        Session["CurrentUnauthorizedUser"] = user;
                        unit.Save();
                    }
                    else
                    {
                        user = (User)(Session["CurrentUnauthorizedUser"]);
                        string kek = "";
                    }

                }
                user.Comments.Add(post);
                post.User = user;
                post.UserId = user.Id;
                post.time = DateTime.Now;
                user.Comments.Add(post);

                unit.Comments.Create(post);
                unit.Save();



            }
            return RedirectToAction("Index");
        }
    }
}