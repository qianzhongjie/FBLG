using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBlungu.BLL;
using FBlungu.Common.DEncrypt;
namespace FBlungu.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Login()
        {
            HttpCookie cookie = new HttpCookie("MyCook");//初使化并设置Cookie的名称
            string cookieName;
            int limit = Request.Cookies.Count;
            for (int i = 0; i < limit; i++)
            {
                cookieName = Request.Cookies[i].Name;
                cookie = new HttpCookie(cookieName);
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }
            return View();
        }
        /// <summary>
        ///登录验证
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Loading()
        {

            if (string.IsNullOrEmpty(Request["user"]) || string.IsNullOrEmpty(Request["pwd"]))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                BLL.f_user bll_user = new f_user();
                string pwd = MD5Encode.GetMd5Str(Request["pwd"]);
                var userDB = bll_user.GetList("F_userid='" + Request["user"] + "' and F_pwd='" + pwd + "'");
                if (userDB.Tables[0].Rows.Count > 0)
                {
                    HttpCookie cookie = new HttpCookie("MyCook");//初使化并设置Cookie的名称
                    DateTime dt = DateTime.Now;
                    TimeSpan ts = new TimeSpan(0, 0, 20, 0, 0);//过期时间为15分钟
                    cookie.Expires = dt.Add(ts);//设置过期时间
                    cookie.Values.Add("nowtime", dt.ToString());
                    cookie.Values.Add("userid", Request["user"]);
                    Response.AppendCookie(cookie);
                    return Json(new { code = 0, msg = "登录成功" });
                }
                else
                {
                    return Json(new { code = 1, msg = "登录失败,请检查账号和密码是否有误" });
                }
            }
        }
    }
}
