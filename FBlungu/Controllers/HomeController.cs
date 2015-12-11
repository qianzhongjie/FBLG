using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBlungu.AdminActionFilter;
namespace FBlungu.Controllers
{
    [AdminSessionFilter]
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Home()
        {
            if (Request.Cookies["MyCook"] != null)
            {
                string user = Request.Cookies["MyCook"]["userid"];
                if (string.IsNullOrEmpty(user))
                {
                    return RedirectToAction("Login", "Login");
                }
                return View();
            }
            return RedirectToAction("Login", "Login");
        }

    }
}
