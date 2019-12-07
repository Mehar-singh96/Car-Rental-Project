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
    public class CarDetailsController : Controller
    {
        private CarRentalModel db = new CarRentalModel();

        // GET: CarDetails
        public async Task<ActionResult> Index()
        {
            return View(await db.CarDetails.ToListAsync());
        }

        // GET: CarDetails/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarDetail carDetail = await db.CarDetails.FindAsync(id);
            if (carDetail == null)
            {
                return HttpNotFound();
            }
            return View(carDetail);
        }

        // GET: CarDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CarDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CarModel,Quantity,VehicleType,PassengerCapacity,Price")] CarDetail carDetail)
        {
            if (ModelState.IsValid)
            {
                db.CarDetails.Add(carDetail);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(carDetail);
        }

        // GET: CarDetails/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarDetail carDetail = await db.CarDetails.FindAsync(id);
            if (carDetail == null)
            {
                return HttpNotFound();
            }
            return View(carDetail);
        }

        // POST: CarDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CarModel,Quantity,VehicleType,PassengerCapacity,Price")] CarDetail carDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carDetail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(carDetail);
        }

        // GET: CarDetails/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarDetail carDetail = await db.CarDetails.FindAsync(id);
            if (carDetail == null)
            {
                return HttpNotFound();
            }
            return View(carDetail);
        }

        // POST: CarDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            CarDetail carDetail = await db.CarDetails.FindAsync(id);
            db.CarDetails.Remove(carDetail);
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
