using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Car_rental.Models;

namespace Car_rental.Controllers
{
    public class CarLocationsController : Controller
    {
        private CarRentalModel db = new CarRentalModel();

        // GET: CarLocations
        public async Task<ActionResult> Index()
        {
            var carLocations = db.CarLocations.Include(c => c.CarDetail);
            return View(await carLocations.ToListAsync());
        }

        // GET: CarLocations/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarLocation carLocation = await db.CarLocations.FindAsync(id);
            if (carLocation == null)
            {
                return HttpNotFound();
            }
            return View(carLocation);
        }

        // GET: CarLocations/Create
        public ActionResult Create()
        {
            ViewBag.CarModel = new SelectList(db.CarDetails, "CarModel", "VehicleType");
            return View();
        }

        // POST: CarLocations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CarNo,Location,CarModel,Color")] CarLocation carLocation)
        {
            if (ModelState.IsValid)
            {
                db.CarLocations.Add(carLocation);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CarModel = new SelectList(db.CarDetails, "CarModel", "VehicleType", carLocation.CarModel);
            return View(carLocation);
        }

        // GET: CarLocations/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarLocation carLocation = await db.CarLocations.FindAsync(id);
            if (carLocation == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarModel = new SelectList(db.CarDetails, "CarModel", "VehicleType", carLocation.CarModel);
            return View(carLocation);
        }

        // POST: CarLocations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CarNo,Location,CarModel,Color")] CarLocation carLocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carLocation).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CarModel = new SelectList(db.CarDetails, "CarModel", "VehicleType", carLocation.CarModel);
            return View(carLocation);
        }

        // GET: CarLocations/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarLocation carLocation = await db.CarLocations.FindAsync(id);
            if (carLocation == null)
            {
                return HttpNotFound();
            }
            return View(carLocation);
        }

        // POST: CarLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            CarLocation carLocation = await db.CarLocations.FindAsync(id);
            db.CarLocations.Remove(carLocation);
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
