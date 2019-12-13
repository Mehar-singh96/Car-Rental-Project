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
    public class Car_TypeController : Controller
    {
        private CarModel db = new CarModel();

        // GET: Car_Type
        public async Task<ActionResult> Index()
        {
            return View(await db.Car_Type.ToListAsync());
        }

        // GET: Car_Type/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car_Type car_Type = await db.Car_Type.FindAsync(id);
            if (car_Type == null)
            {
                return HttpNotFound();
            }
            return View(car_Type);
        }

        // GET: Car_Type/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Car_Type/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Type")] Car_Type car_Type)
        {
            if (ModelState.IsValid)
            {
                db.Car_Type.Add(car_Type);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(car_Type);
        }

        // GET: Car_Type/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car_Type car_Type = await db.Car_Type.FindAsync(id);
            if (car_Type == null)
            {
                return HttpNotFound();
            }
            return View(car_Type);
        }

        // POST: Car_Type/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Type")] Car_Type car_Type)
        {
            if (ModelState.IsValid)
            {
                db.Entry(car_Type).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(car_Type);
        }

        // GET: Car_Type/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car_Type car_Type = await db.Car_Type.FindAsync(id);
            if (car_Type == null)
            {
                return HttpNotFound();
            }
            return View(car_Type);
        }

        // POST: Car_Type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Car_Type car_Type = await db.Car_Type.FindAsync(id);
            db.Car_Type.Remove(car_Type);
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
