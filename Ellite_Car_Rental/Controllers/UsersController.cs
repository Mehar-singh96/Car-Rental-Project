using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ellite_Car_Rental.Models;

namespace Ellite_Car_Rental.Controllers
{
    public class UsersController : Controller
    {
        private CarModel db = new CarModel();

        // GET: Users
        public async Task<ActionResult> Index()
        {
            return View(await db.Users.ToListAsync());
        }


        // GET: signin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignInAfter()
        {
            string email = Request["email"].ToString();
            string pass = Request["pass"].ToString();
            var user = db.Users.Where(users => users.Email.Equals(email));

            foreach (var users in user)
            {
                string password = users.Password;
                if (password.Equals(pass))
                {
                    Session["UserID"] = users.ID;
                    return RedirectToAction("fromSigninAfter", "Cars");
                }
            }

            ViewBag.error = "Your credentials are wrong";
           
            return View("SignIn");
        }

        public ActionResult Home()
        {
            return View("Home");
        }

        public ActionResult signin_start()
        {
            return View("SignIn");
        }

        public ActionResult signup_start()
        {
            return View("Create");
        }




        // GET: Users/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,F_Name,L_Name,Email,User_name,Password,Role,Street_Address,City,Province,Country,Postal_Code")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                await db.SaveChangesAsync();
                Session["userID"] = user.ID;

                
            }
            return this.RedirectToAction("redirecthome","Cars");
           // return View(user);
        }
        public ActionResult SignIn()
        {
            return View("SignIn");
        }
        // GET: Users/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,F_Name,L_Name,Email,User_name,Password,Role,Street_Address,City,Province,Country,Postal_Code")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            User user = await db.Users.FindAsync(id);
            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
