﻿using System;
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
    public class BookingsController : Controller
    {
        private CarModel db = new CarModel();

        // GET: Bookings
        public async Task<ActionResult> Index()
        {
            var bookings = db.Bookings.Include(b => b.Car).Include(b => b.Order);
            return View(await bookings.ToListAsync());
        }



        // GET: Bookings
        public async Task<ActionResult> from_proceed()
        {
           
            return View("Payment");
        }


        public async Task<ActionResult> pay_confirm()
        {
            


            Status st = new Status();
            st.Status_Pay = "confirmed";
            db.Status.Add(st);
            db.SaveChanges();

            Order order = new Order();
            order.Usr_Id = (int)Session["userID"];
            order.Pay_Id = st.ID;
            order.Total_Price = Convert.ToDecimal(Session["Rent"]);
            db.Orders.Add(order);


           

            DateTime rentStart = Convert.ToDateTime((DateTime)Session["fromDate"]);
            DateTime rentEnd = Convert.ToDateTime((DateTime)Session["tillDate"]);
           // for (DateTime date = rentStart; date <= rentEnd; date = date.AddDays(1))
            {
                try
                {
                    Booking bk = new Booking();
                    bk.Trans_Id = order.Trans_Id;
                    bk.Car_Id = (int)Session["car_ID"];
                    bk.Date_Rent = rentStart;
                    bk.Order = null;

                    db.Bookings.Add(bk);
                    db.SaveChanges();
                    //await db.SaveChangesAsync();
                }
                catch (Exception e)
                {

                }
            }

            return View("summary");
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
            ViewBag.Car_Id = new SelectList(db.Cars, "ID", "Title");
            ViewBag.Trans_Id = new SelectList(db.Orders, "Trans_Id", "Trans_Id");
            return View();
        }
        public ActionResult Payment()
        {
            return View();
        }
        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Trans_Id,Car_Id,Date_Rent")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Car_Id = new SelectList(db.Cars, "ID", "Title", booking.Car_Id);
            ViewBag.Trans_Id = new SelectList(db.Orders, "Trans_Id", "Trans_Id", booking.Trans_Id);
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
            ViewBag.Car_Id = new SelectList(db.Cars, "ID", "Title", booking.Car_Id);
            ViewBag.Trans_Id = new SelectList(db.Orders, "Trans_Id", "Trans_Id", booking.Trans_Id);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Trans_Id,Car_Id,Date_Rent")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Car_Id = new SelectList(db.Cars, "ID", "Title", booking.Car_Id);
            ViewBag.Trans_Id = new SelectList(db.Orders, "Trans_Id", "Trans_Id", booking.Trans_Id);
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
