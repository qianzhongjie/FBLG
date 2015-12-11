using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace QYCustomerSubject.NewApplication.AdminActionFilter
{
    public class AdminSystemError : FilterAttribute, IExceptionFilter
    {
        FBlungu.BLL.m_sys_operation_log bll_syslog = new FBlungu.BLL.m_sys_operation_log();
        public void OnException(ExceptionContext filterContext)
        {
            bll_syslog.Add(new FBlungu.Model.m_sys_operation_log()
            {
                error_code = "59000",
                error_desc = "[系统内部错误]" + filterContext.Exception.Message,
                error_source = "应用系统",
                error_time = DateTime.Now
            });
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            filterContext.Result = new RedirectResult("/ErrorPage/ErrorPage");
            return;
        }
    }
}