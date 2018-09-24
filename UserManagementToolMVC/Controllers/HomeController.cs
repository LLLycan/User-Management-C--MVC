using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UserManagementToolMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Description = ConfigurationManager.AppSettings["appDescription"];
            ViewBag.Author = ConfigurationManager.AppSettings["appAuthor"];
            ViewBag.AppVersion = ConfigurationManager.AppSettings["appVersion"];

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Address = ConfigurationManager.AppSettings["contactAddress"];
            ViewBag.Phone = ConfigurationManager.AppSettings["contactPhone"];
            ViewBag.Email = ConfigurationManager.AppSettings["contactEmail"];

            return View();
        }
    }
}