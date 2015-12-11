using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FBlungu.Controllers
{
    public class SendMsgController : Controller
    {
        //
        // GET: /SendMsg/

        public ActionResult Index()
        {
            return View();
        }
        public JsonResult SendMsg()
        {
 
            return Json(new { });
        }
    }
}
