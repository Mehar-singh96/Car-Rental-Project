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
    public class CartsController : Controller
    {
        private CarModel db = new CarModel();

        // GET: Carts
        public async Task<ActionResult> Index()
        {
           //List<Cart> v = (List<Cart>)Session["cart"];
            var carts = db.Carts.Include(c => c.Car).Include(c => c.User);
            return View(await carts.ToListAsync());
        }



        public async Task<ActionResult> proceed()
        {

            return RedirectToAction("from_proceed", "Bookings");
        }




        // GET: Carts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = await db.Carts.FindAsync(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // GET: Carts/Create
        public ActionResult Create()
        {
            ViewBag.Car_Id = new SelectList(db.Cars, "ID", "Title");
            ViewBag.User_Id = new SelectList(db.Users, "ID", "F_Name");
            return View();
        }


       


        // POST: Carts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,User_Id,Car_Id,From_Date,Till_Date")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Carts.Add(cart);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Car_Id = new SelectList(db.Cars, "ID", "Title", cart.Car_Id);
            ViewBag.User_Id = new SelectList(db.Users, "ID", "F_Name", cart.User_Id);
            return View(cart);
        }

        // GET: Carts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = await db.Carts.FindAsync(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            ViewBag.Car_Id = new SelectList(db.Cars, "ID", "Title", cart.Car_Id);
            ViewBag.User_Id = new SelectList(db.Users, "ID", "F_Name", cart.User_Id);
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,User_Id,Car_Id,From_Date,Till_Date")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cart).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Car_Id = new SelectList(db.Cars, "ID", "Title", cart.Car_Id);
            ViewBag.User_Id = new SelectList(db.Users, "ID", "F_Name", cart.User_Id);
            return View(cart);
        }

        // GET: Carts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = await db.Carts.FindAsync(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Cart cart = await db.Carts.FindAsync(id);
            db.Carts.Remove(cart);
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
