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
    public class ExtraFeaturesController : Controller
    {
        private CarRentalModel db = new CarRentalModel();

        // GET: ExtraFeatures
        public async Task<ActionResult> Index()
        {
            return View(await db.ExtraFeatures.ToListAsync());
        }

        // GET: ExtraFeatures/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtraFeature extraFeature = await db.ExtraFeatures.FindAsync(id);
            if (extraFeature == null)
            {
                return HttpNotFound();
            }
            return View(extraFeature);
        }

        // GET: ExtraFeatures/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExtraFeatures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "FeatureId,FeatureName,Price,Quantity,FeatureDesc")] ExtraFeature extraFeature)
        {
            if (ModelState.IsValid)
            {
                db.ExtraFeatures.Add(extraFeature);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(extraFeature);
        }

        // GET: ExtraFeatures/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtraFeature extraFeature = await db.ExtraFeatures.FindAsync(id);
            if (extraFeature == null)
            {
                return HttpNotFound();
            }
            return View(extraFeature);
        }

        // POST: ExtraFeatures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "FeatureId,FeatureName,Price,Quantity,FeatureDesc")] ExtraFeature extraFeature)
        {
            if (ModelState.IsValid)
            {
                db.Entry(extraFeature).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(extraFeature);
        }

        // GET: ExtraFeatures/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtraFeature extraFeature = await db.ExtraFeatures.FindAsync(id);
            if (extraFeature == null)
            {
                return HttpNotFound();
            }
            return View(extraFeature);
        }

        // POST: ExtraFeatures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ExtraFeature extraFeature = await db.ExtraFeatures.FindAsync(id);
            db.ExtraFeatures.Remove(extraFeature);
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
