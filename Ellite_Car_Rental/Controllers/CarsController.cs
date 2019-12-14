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
    public class CarsController : Controller
    {
        private CarModel db = new CarModel();

        // GET: Cars
        public async Task<ActionResult> Index()
        {
            var cars = db.Cars.Include(c => c.Car_Type);
            return View(await cars.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // GET: Cars
        public async Task<ActionResult> afterSign()
        {
            return View("HomePage");

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        // GET: Cars
        public  ActionResult afterSignIn([Bind(Include ="email,pass")] Car dsf)
        {
            string email = Request["email"].ToString();
            string pass = Request["pass"].ToString();

            var user=db.Users.Where(users=> users.Email.Equals(email) );

            foreach(var users in user)
            {
                string password = users.Password;
                if (password.Equals(pass))
                    return View("HomePage");
            }

            ViewBag.error = "Your credentials are wrong";
            return View("~/Users/SignIn.cshtml");

        }

        public ActionResult fromSignInAfter()
        {

            return View("HomePage");
        }


        public ActionResult from_cartDelete()
        {

            return RedirectToAction("lookCars");
        }
        

        // GET: HomePage
        public async Task<ActionResult> HomePage()
        {
            var cars = db.Cars.Include(c => c.Car_Type);
            return View(await cars.ToListAsync());
        }


        // GET: lookCars
        public async Task<ActionResult> lookCars()
        {
            var cars = db.Cars.Include(c => c.Car_Type);
            return View("HomePage");
        }



        // GET: Reserve
        public async Task<ActionResult> Reserve(int id)
        {
            //Session["id"] = id;
            Cart cr = new Cart();
            cr.Car_Id = id;
            cr.From_Date =(DateTime) Session["fromDate"];
            cr.Till_Date = (DateTime)Session["tillDate"];
            cr.User_Id = (int)Session["userID"];
            db.Carts.Add(cr);
            db.SaveChanges();

            var car_item = db.Carts.Where(x => x.User_Id == cr.User_Id);

            List<Cart> list = car_item.ToList();

            foreach(Cart cart in list)
            {
                Cart cartobject = cart;
                this.Session["ID"] = cartobject.ID;
                this.Session["Image"] = cartobject.Car.Url_Img;
                this.Session["Desc"] = cartobject.Car.Desc;
                this.Session["Title"] = cartobject.Car.Title;
                this.Session["Rent"] = cartobject.Car.Rent;
                this.Session["Car_ID"] = cartobject.Car.ID;
            }

            

            return this.RedirectToAction("Index", "Carts");
        }


        // GET: Cars/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = await db.Cars.FindAsync(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // GET: Cars/Create
        public ActionResult Create()
        {
            ViewBag.TypeId = new SelectList(db.Car_Type, "ID", "Type");
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Title,Url_Img,Desc,TypeId,Available,Qty,Rent")] Car car)
        {
            if (ModelState.IsValid)
            {
                db.Cars.Add(car);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.TypeId = new SelectList(db.Car_Type, "ID", "Type", car.TypeId);
            return View(car);
        }

        // GET: Cars/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = await db.Cars.FindAsync(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            ViewBag.TypeId = new SelectList(db.Car_Type, "ID", "Type", car.TypeId);
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Title,Url_Img,Desc,TypeId,Available,Qty,Rent")] Car car)
        {
            if (ModelState.IsValid)
            {
                db.Entry(car).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.TypeId = new SelectList(db.Car_Type, "ID", "Type", car.TypeId);
            return View(car);
        }

        // GET: Cars/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = await db.Cars.FindAsync(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Car car = await db.Cars.FindAsync(id);
            db.Cars.Remove(car);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }




        [HttpPost, ActionName("searchCars")]
        public async Task<ActionResult> searchCars(DateTime fromDate, DateTime tillDate)
        {
            Session["fromDate"] = fromDate;
            Session["tillDate"] = tillDate;
            List<HomePage> hps = new List<HomePage>();
            HomePage hp = new HomePage();

            var carsWithDate = db.Bookings.AsEnumerable().Where((x => x.Date_Rent >= fromDate && x.Date_Rent <= tillDate));
            var listCar = carsWithDate.Select(x => x.Car);
            var dictionary = new Dictionary<int, int>();

            var cars = db.Cars.AsEnumerable();
            var bookings = db.Bookings.AsEnumerable();

            foreach(var i in cars)
            {
                int sub_qty = 0;
                
                foreach (var j in bookings)
                {
                    if (i.ID == j.Car_Id)
                    {
                        sub_qty++;
                    }
                }
                dictionary[i.ID] = (int)i.Qty - sub_qty;
            }

            foreach(var car in cars)
            {
                HomePage hpg = new HomePage();
                int car_fqty;
                dictionary.TryGetValue(car.ID, out car_fqty);
                hpg.Qty = car_fqty;
                hpg.Url_Img = car.Url_Img;
                hpg.Car_Type = car.Car_Type;
                hpg.Title = car.Title;
                hpg.Desc = car.Desc;
                hpg.Rent = car.Rent;
                hpg.ID = car.ID;
                hps.Add(hpg);
            }


            if (fromDate == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View("~/Views/Cars/lookCars.cshtml", hps);
        }
        public ActionResult redirecthome()
        {
            return View("HomePage");
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
