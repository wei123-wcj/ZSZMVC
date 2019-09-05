using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZSZ.Front.Web.App_Start
{
    public class ZSZExceptionFilter:IExceptionFilter
    {
        private static ILog log = LogManager.GetLogger(typeof(ZSZExceptionFilter));//声明Log4NET对象，建议一个类就声明一个ILog对象
        public void OnException(ExceptionContext filterContext)
        {
            log.ErrorFormat("出现未处理的异常{0}", filterContext.Exception);

        }
    }
}