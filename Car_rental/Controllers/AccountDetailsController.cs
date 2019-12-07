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
    public class AccountDetailsController : Controller
    {
        private CarRentalModel db = new CarRentalModel();

        // GET: AccountDetails
        public async Task<ActionResult> Index()
        {
            var accountDetails = db.AccountDetails.Include(a => a.Discount);
            return View(await accountDetails.ToListAsync());
        }

        // GET: AccountDetails/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountDetail accountDetail = await db.AccountDetails.FindAsync(id);
            if (accountDetail == null)
            {
                return HttpNotFound();
            }
            return View(accountDetail);
        }

        // GET: AccountDetails/Create
        public ActionResult Create()
        {
            ViewBag.TotalPoints = new SelectList(db.Discounts, "Points", "Points");
            return View();
        }

        // POST: AccountDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Email,Password,FirstName,LastName,PhoneNo,TotalPoints,IssueCountry,IssueAuthority,DriverLicenceNo")] AccountDetail accountDetail)
        {
            accountDetail.TotalPoints = 2000;
           
            if (ModelState.IsValid)
            {
                db.AccountDetails.Add(accountDetail);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            SignUpModel a = new SignUpModel();
            a.Email = accountDetail.Email;
            a.FirstName = accountDetail.FirstName;
            a.LastName = accountDetail.LastName;
            a.PhoneNo = accountDetail.PhoneNo;
            a.Password = accountDetail.Password;
            a.IssueAuthority = accountDetail.IssueAuthority;
            a.IssueCountry = accountDetail.IssueCountry;
           

            ViewBag.TotalPoints = new SelectList(db.Discounts, "Points", "Points", accountDetail.TotalPoints);
          // return View(a);

            // ViewBag.TotalPoints = new SelectList(db.Discounts, "Points", "Points", accountDetail.TotalPoints);
            return View(a);
        }

        // GET: AccountDetails/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountDetail accountDetail = await db.AccountDetails.FindAsync(id);
            if (accountDetail == null)
            {
                return HttpNotFound();
            }

            SignUpModel a = new SignUpModel();
            a.Email = accountDetail.Email;
            a.FirstName = accountDetail.FirstName;
            a.LastName = accountDetail.LastName;
            a.PhoneNo = accountDetail.PhoneNo;
            a.Password = accountDetail.Password;
            a.IssueAuthority = accountDetail.IssueAuthority;
            a.IssueCountry = accountDetail.IssueCountry;
            
            ViewBag.TotalPoints = new SelectList(db.Discounts, "Points", "Points", accountDetail.TotalPoints);
            return View(a);
        }

        // POST: AccountDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Email,Password,FirstName,LastName,PhoneNo,TotalPoints,IssueCountry,IssueAuthority,DriverLicenceNo")] AccountDetail accountDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accountDetail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.TotalPoints = new SelectList(db.Discounts, "Points", "Points", accountDetail.TotalPoints);
            return View(accountDetail);
        }

        // GET: AccountDetails/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountDetail accountDetail = await db.AccountDetails.FindAsync(id);
            if (accountDetail == null)
            {
                return HttpNotFound();
            }
            return View(accountDetail);
        }

        // POST: AccountDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            AccountDetail accountDetail = await db.AccountDetails.FindAsync(id);
            db.AccountDetails.Remove(accountDetail);
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
