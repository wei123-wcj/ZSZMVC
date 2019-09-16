using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSZ.Common;
using ZSZ.IService;
using ZSZ.Service;

namespace ZSZ.Admin.Web.App_Start
{
    public class ZSZAuthorizationFilter : IAuthorizationFilter
    {
        public IAdminUserService AdminUser { get; set; }
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            CheckPermissionAttribute[] checkPermissions = (CheckPermissionAttribute[])filterContext.ActionDescriptor.GetCustomAttributes(typeof(CheckPermissionAttribute), false);
            if (checkPermissions.Length <= 0)
            {
                //说明没有标记任何特性
                return;//跳出过滤器，继续执行后续的action
            }

            //取出session的值，看是否为null
            long? loginID = (long?)filterContext.HttpContext.Session["LoginID"];
            if (loginID == null)
            {
                //说明未登录，返回登录页面
                //思考：如何判断请求是来自form提交请求还是ajax请求，进而做不同的处理
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    //这是AJAX请求
                    AjaxReault aj = new AjaxReault();
                    aj.Data = "/main/Login";
                    aj.Statin = "redirect";
                    aj.Msg = "用户未登录";
                    //filterContext.Result只要被赋值，就不会再继续执行原本想要执行的action，而是会返回result的结果
                    filterContext.Result = new JsonResult() { Data = aj };
                }
                else
                {
                    //表单请求
                    filterContext.Result = new RedirectResult("~/Main/login");

                }
            }
            else
            {
                AdminUser = new AdminUserService();//获取接口实现类的对象

                //推荐下面的用法：
                //由于ZSZAuthorizeFilter不是被autofac创建，因此不会自动进行属性的注入
                //需要手动获取Service对象
                //IAdminUserService userService =
                //    DependencyResolver.Current.GetService<IAdminUserService>();


                //已登录。做权限的判断
                foreach (var permAtt in checkPermissions)
                {
                    //判断当前登录用户是否具有permAtt.Permission权限
                    //(long)userId   userId.Value
                    if (!AdminUser.HasPermission(loginID.Value, permAtt.Name))
                    {
                        //只要碰到任何一个没有的权限，就禁止访问
                        //在IAuthorizationFilter里面，只要修改filterContext.Result 
                        //那么真正的Action方法就不会执行了
                        if (filterContext.HttpContext.Request.IsAjaxRequest())
                        {
                            AjaxReault ajaxResult = new AjaxReault();
                            ajaxResult.Statin = "error";
                            ajaxResult.Msg = "没有权限" + permAtt.Name;
                            filterContext.Result = new JsonResult { Data = ajaxResult };
                        }
                        else
                        {
                            filterContext.Result
                           = new ContentResult { Content = "没有" + permAtt.Name + "这个权限" };
                        }
                        return;
                    }
                }
            }
        }

    }
}
