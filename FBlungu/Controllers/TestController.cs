using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using FBlungu.Common;
namespace FBlungu.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserInfo()
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
        /// <summary>
        ///登录验证
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Loading()
        {

            if (string.IsNullOrEmpty(Request["user"]) || string.IsNullOrEmpty(Request["pwd"]))
            {
                return RedirectToAction("Test", "Index");
            }
            else
            {
                BLL.UserInfo bll_user = new BLL.UserInfo();
                string pwd = Request["pwd"];
                var userDB = bll_user.GetList(" \"User\"='" + Request["user"] + "' And Pwd='" + pwd + "'");
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
        [HttpPost]
        public ActionResult Search()
        {

            BLL.UserInfo bll_user = new BLL.UserInfo();
            var userDb = bll_user.GetList("");
            FBlungu.Models.NewUser newuser = new Models.NewUser();
            List<FBlungu.Models.NewUser> model = data.PutAllVal(newuser, userDb);
            return Json(new { List = model });

        }
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            BLL.UserInfo bll_user = new BLL.UserInfo();
            var userDb = bll_user.Delete(Id);
            if (userDb)
            {
                return Json(new { code = 0, msg = "删除成功" });
            }
            return Json(new { code = 1, msg = "服务器繁忙" });
        }
        [HttpPost]
        public ActionResult LockInfo(int Id)
        {
            BLL.UserInfo bll_user = new BLL.UserInfo();
            var userDb = bll_user.GetModel(Id);

            return Json(new { code = 0, data = userDb });


        }
        [HttpPost]
        public ActionResult Svae(string JsonString)
        {
            BLL.UserInfo bll_user = new BLL.UserInfo();
            var model = ObjectToJson.JsonStringToObject<Model.UserInfo>(JsonString);
            if (model.Id > 0)
            {
                var dbresut = bll_user.Update(model);
                if (dbresut)
                {
                    return Json(new { code = 0, msg = "修改成功" });
                }
                return Json(new { code = 1, msg = "服务器繁忙" });
            }
            else
            {
                var dbresut = bll_user.Add(model);
                if (dbresut > 0)
                {
                    return Json(new { code = 0, msg = "添加成功" });
                }
                return Json(new { code = 1, msg = "服务器繁忙" });
            }
        }
    }
}
