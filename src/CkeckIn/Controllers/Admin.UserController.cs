using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CkeckIn.Data;
using CkeckIn.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
namespace CkeckIn.Controllers
{
    public class UserController : Controller
    {
        // GET: UserManager
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult HurryDarryIamASheep()
        {
            using (var db = new ApplicationDbContext())
            {
                var adminRole = db.Roles.FirstOrDefault(r => r.Name == "Admin");
                if (adminRole == null)
                {
                    db.Roles.Add(adminRole = new ApplicationRole
                    {
                        Id = Guid.NewGuid(),
                        Name = "Admin",
                        NormalizedName = "Администратор"
                    });
                }

                var user = db.Users.First(e => e.Id == User.GetGuidUserId());
                user.Roles.Add(new IdentityUserRole<Guid>
                {
                    RoleId = adminRole.Id,
                    UserId = user.Id
                });
                db.SaveChanges();
                return View();
            }
        }


        // GET: UserManager/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserManager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserManager/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UserManager/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserManager/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: UserManager/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserManager/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}