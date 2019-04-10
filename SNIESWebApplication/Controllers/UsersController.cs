namespace SNIESWebApplication.Controllers
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using SNIESWebApplication.Models;
    using SNIESWebApplication.ModelViews;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
   
        public ActionResult Index()
        {
            var usuarioManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var usuarios = usuarioManager.Users.ToList();
            var usersView = new List<UserView>();

            foreach (var usuario in usuarios)
            {
                var userView = new UserView()
                {
                    EMail = usuario.Email,
                    Name = usuario.UserName,
                    UserID = usuario.Id
                };

                usersView.Add(userView);
            }

            return View(usersView);
        }

        public ActionResult Roles(string userID)
        {
            var rolMarager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var usuarioManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            var roles = rolMarager.Roles.ToList();
            var usuarios = usuarioManager.Users.ToList();
            var usuario = usuarios.Find(u => u.Id == userID);

            var rolesView = new List<RolView>();
            if (usuario.Roles != null)
            {
                foreach (var item in usuario.Roles)
                {
                    var role = roles.Find(r => r.Id == item.RoleId);
                    var roleView = new RolView
                    {
                        RoleID = role.Id,
                        Name = role.Name,
                    };
                    rolesView.Add(roleView);
                }
            }
            

            var userView = new UserView()
            {
                EMail = usuario.Email,
                Name = usuario.UserName,
                UserID = usuario.Id,
                Roles = rolesView
            };

            return View(userView); 
        }

        // GET: Users/AddRole/5
        public ActionResult AddRole(int id)
        {
            return View();
        }

        // POST: Users/AddRole/5
        [HttpPost]
        public ActionResult AddRole(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Users/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
