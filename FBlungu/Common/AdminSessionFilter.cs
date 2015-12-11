using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace FBlungu.AdminActionFilter
{
    /// <summary>
    /// 身份认证过滤器
    /// </summary>
    public class AdminSessionFilter : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {

                if (!string.IsNullOrEmpty(filterContext.HttpContext.Request.Cookies["MyCook"]["userid"]) || filterContext.HttpContext.Request.Cookies["MyCook"] != null)
                {
                    string key = filterContext.HttpContext.Request.Cookies["MyCook"]["userid"];
                    DateTime times = DateTime.Parse(filterContext.HttpContext.Request.Cookies["MyCook"]["nowtime"]);
                    // DateTime time = filterContext.HttpContext.Request.Cookies["MyCook"].Expires;
                    TimeSpan time = new TimeSpan(0, 0, 20, 0, 0);
                    DateTime dt = DateTime.Now;
                    if (dt - times > time)
                    {
                        HttpCookie cookie = new HttpCookie("MyCook");//初使化并设置Cookie的名称
                        string cookieName;
                        int limit = filterContext.HttpContext.Request.Cookies.Count;
                        for (int i = 0; i < limit; i++)
                        {
                            cookieName = filterContext.HttpContext.Request.Cookies[i].Name;
                            cookie = new HttpCookie(cookieName);
                            cookie.Expires = DateTime.Now.AddDays(-1);
                            filterContext.HttpContext.Response.Cookies.Add(cookie);
                        }
                        filterContext.Result = new ContentResult { Content = @"<script type='text/javascript'>if(typeof($)!='undefined'){ if(typeof($.messager)!='undefined'){ $.messager.confirm('安全提示','\r\n由于您长时间未操作，为确保安全，系统已经自动帮您下线。\r\n如果需要继续操作，请重新登陆！','',function(returnvalue){if(returnvalue){window.top.document.location='/Login/Login';}});}else{window.top.document.location='/Login/Login'; }}else{window.top.document.location='/Login/Login';}</script>" };//功能权限弹出提示框                          

                    }
                    else
                    {
                        HttpCookie cookie = new HttpCookie("MyCook");//初使化并设置Cookie的名称
                        DateTime dts = DateTime.Now;
                        TimeSpan ts = new TimeSpan(0, 0, 20, 0, 0);//过期时间为15分钟
                        cookie.Expires = dt.Add(ts);//设置过期时间
                        cookie.Values.Add("nowtime", dts.ToString());
                        cookie.Values.Add("userid", key);
                        filterContext.HttpContext.Response.AppendCookie(cookie);
                    }
                }
                else
                {
                    filterContext.Result = new ContentResult { Content = @"<script type='text/javascript'>if(typeof($)!='undefined'){ if(typeof($.messager)!='undefined'){ $.messager.confirm('安全提示','\r\n由于您长时间未操作，为确保安全，系统已经自动帮您下线。\r\n如果需要继续操作，请重新登陆！','',function(returnvalue){if(returnvalue){window.top.document.location='/Login/Login';}});}else{window.top.document.location='/Login/Login'; }}else{window.top.document.location='/Login/Login';}</script>" };//功能权限弹出提示框                         
                }
            }
            catch (Exception ex)
            {
                filterContext.Result = new ContentResult { Content = @"<script type='text/javascript'>if(typeof($)!='undefined'){ if(typeof($.messager)!='undefined'){ $.messager.confirm('安全提示','\r\n由于您长时间未操作，为确保安全，系统已经自动帮您下线。\r\n如果需要继续操作，请重新登陆！','',function(returnvalue){if(returnvalue){window.top.document.location='/Login/Login';}});}else{window.top.document.location='/Login/Login'; }}else{window.top.document.location='/Login/Login';}</script>" };//功能权限弹出提示框              
            }
        }
    }
}