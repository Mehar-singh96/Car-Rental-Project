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
    public class BookingsController : Controller
    {
        private CarRentalModel db = new CarRentalModel();

        // GET: Bookings
        public async Task<ActionResult> Index()
        {
            var bookings = db.Bookings.Include(b => b.AccountDetail).Include(b => b.CarLocation).Include(b => b.ExtraFeature);
            return View(await bookings.ToListAsync());
        }

        // GET: Bookings/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = await db.Bookings.FindAsync(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {
            ViewBag.Email = new SelectList(db.AccountDetails, "Email", "Password");
            ViewBag.CarNo = new SelectList(db.CarLocations, "CarNo", "Location");
            ViewBag.ExtraFeatureId = new SelectList(db.ExtraFeatures, "FeatureId", "FeatureName");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "BookingId,Email,CarNo,BookedFrom,BookedTill,BookingDate,ExtraFeatureId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Email = new SelectList(db.AccountDetails, "Email", "Password", booking.Email);
            ViewBag.CarNo = new SelectList(db.CarLocations, "CarNo", "Location", booking.CarNo);
            ViewBag.ExtraFeatureId = new SelectList(db.ExtraFeatures, "FeatureId", "FeatureName", booking.ExtraFeatureId);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = await db.Bookings.FindAsync(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.Email = new SelectList(db.AccountDetails, "Email", "Password", booking.Email);
            ViewBag.CarNo = new SelectList(db.CarLocations, "CarNo", "Location", booking.CarNo);
            ViewBag.ExtraFeatureId = new SelectList(db.ExtraFeatures, "FeatureId", "FeatureName", booking.ExtraFeatureId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "BookingId,Email,CarNo,BookedFrom,BookedTill,BookingDate,ExtraFeatureId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Email = new SelectList(db.AccountDetails, "Email", "Password", booking.Email);
            ViewBag.CarNo = new SelectList(db.CarLocations, "CarNo", "Location", booking.CarNo);
            ViewBag.ExtraFeatureId = new SelectList(db.ExtraFeatures, "FeatureId", "FeatureName", booking.ExtraFeatureId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = await db.Bookings.FindAsync(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Booking booking = await db.Bookings.FindAsync(id);
            db.Bookings.Remove(booking);
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
