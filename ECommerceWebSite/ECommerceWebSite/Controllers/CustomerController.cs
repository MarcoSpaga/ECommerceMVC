using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerceWebSite.Models;

namespace ECommerceWebSite.Controllers
{
    public class CustomerController : Controller
    {
        private northwindEntities db = new northwindEntities();

        //
        // GET: /Customer/

        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        //
        // GET: /Customer/Details/5

        public ActionResult Details(string id = null)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        //
        // GET: /Customer/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Customer/Create
        
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                try
                {
                    customer.CustomerID = customer.ContactName.Substring(0, 2);

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }


                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
                catch (Exception ex)
                {

                }
               
            }

            return View(customer);
        }

        //
        // GET: /Customer/Edit/5

        public ActionResult Edit(string id = null)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        //
        // POST: /Customer/Edit/5

        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                    
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return View(customer);
                }
                
            }
            return View(customer);
        }

        //
        // GET: /Customer/Delete/5

        public ActionResult Delete(string id = null)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        //
        // POST: /Customer/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        [HttpPost]
        public ActionResult UpdateField(String ID, Customer customer)
        {

            db.Customers.Find(ID).ContactName = customer.ContactName;
            try
            {
                if (ModelState.IsValid)
                {
                   
                    db.SaveChanges();
                    return View(db.Customers.Find(ID));
                }
            }
            catch (Exception e)
            {

            }
            return View(db.Customers.Find(ID));
        }
    }
}