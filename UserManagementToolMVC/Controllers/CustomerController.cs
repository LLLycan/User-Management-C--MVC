using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UserManagementToolMVC.DBContext;
using System.Globalization;
using UserManagementToolMVC.MessageHelper;
using UserManagementToolMVC.DataConvertHelper;
using UserManagementToolMVC.Models;

namespace UserManagementToolMVC.Controllers
{
    public class CustomerController : Controller
    {
        private CustomerEntities db = new CustomerEntities();

        ICustomerRepository _repository;

        public CustomerController(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public CustomerController()
        {
        }

        /// <summary>
        /// Get Top 5 Oldest Customers order by Last Name
        /// </summary>
        public ActionResult Index()
        {
            List<tblCustomer> customers = new List<tblCustomer>();
            try
            {
                var customerList = db.tblCustomers.ToList();

                //Filter customer which doesn't have DOB
                foreach (var customer in customerList)
                {
                    if (customer.DOB != null)
                    {
                        customers.Add(customer);
                    }
                }

                //Select top 5 oldest customers, then order by last name
                customers = customers.OrderBy(c => c.DOB).Take(5).OrderBy(x => x.LastName).ToList();
            }
            catch (Exception)
            {
                TempData["message"] = Message.LoadingFailed;
            }

            return View(customers);
        }

        /// <summary>
        /// Get All Customers
        /// </summary>
        public ActionResult ShowAllCustomers()
        {
            List<tblCustomer> customers = new List<tblCustomer>();

            try
            {
                //List all customer from DB
                customers = db.tblCustomers.ToList();

            }
            catch (Exception)
            {
                TempData["message"] = Message.LoadingFailed;
            }

            return View(customers);
        }

        /// <summary>
        /// GET Customer Details by Id
        /// </summary>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCustomer tblCustomer = db.tblCustomers.Find(id);

            if (tblCustomer == null)
            {
                TempData["message"] = Message.CustomerLoadingFailed;

                return RedirectToAction("Index");
            }

            return View(tblCustomer);
        }

        /// <summary>
        /// Show create view 
        /// </summary>
        public ActionResult Create()
        {
            return View("Create");
        }

        /// <summary>
        /// Create new customer by using HTTP POST 
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,FirstName,LastName,Email,DOB,CustCode")] tblCustomer tblCustomer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var formatedDOB = DataConvertor.ConvertDate(tblCustomer.DOB.ToString());

                    tblCustomer.CustCode = DataConvertor.GenerateCustCode(tblCustomer.FirstName, tblCustomer.LastName, formatedDOB);

                    db.tblCustomers.Add(tblCustomer);

                    db.SaveChanges();

                    TempData["message"] = Message.CreateSucceed;

                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                TempData["message"] = Message.CreateFailed;

                return RedirectToAction("Index");
            }

            return View(tblCustomer);
        }

        /// <summary>
        /// Show Update customer View by Id 
        /// </summary>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tblCustomer tblCustomer = db.tblCustomers.Find(id);

            if (tblCustomer == null)
            {
                TempData["message"] = Message.UpdateFailed;

                return RedirectToAction("Index");
            }

            return View(tblCustomer);
        }

        /// <summary>
        /// Update customer by Id 
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,FirstName,LastName,Email,DOB,CustCode")] tblCustomer tblCustomer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(tblCustomer).State = EntityState.Modified;

                    var formatedDOB = DataConvertor.ConvertDate(tblCustomer.DOB.ToString());

                    tblCustomer.CustCode = DataConvertor.GenerateCustCode(tblCustomer.FirstName, tblCustomer.LastName, formatedDOB);

                    db.SaveChanges();

                    TempData["message"] = Message.UpdateSucceed;

                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                TempData["message"] = Message.UpdateFailed;

                return RedirectToAction("Index");
            }

            return View(tblCustomer);
        }

        /// <summary>
        /// Show Delete customer view by Id 
        /// </summary>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tblCustomer tblCustomer = db.tblCustomers.Find(id);

            if (tblCustomer == null)
            {
                TempData["message"] = Message.CustomerLoadingFailed;

                return RedirectToAction("Index");
            }

            return View(tblCustomer);
        }

        /// <summary>
        /// Delete customer by Id using HTTP POST
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                tblCustomer tblCustomer = db.tblCustomers.Find(id);

                db.tblCustomers.Remove(tblCustomer);

                db.SaveChanges();

                TempData["message"] = Message.DeleteSucceed;

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["message"] = Message.DeleteFailed;

                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Dispose controller resource 
        /// </summary>
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
