using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSZ.Admin.Web.Models;
using ZSZ.Common;
using ZSZ.IService;

namespace ZSZ.Admin.Web.Controllers
{
    public class DefaultController : Controller
    {
        public IAdminUserLogin Logins { get; set; }
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(AdminLogin adminLogin)
        {
            if (ModelState.IsValid)
            {
                if (adminLogin.Code == Session["code"].ToString())
                {
                    bool i = Logins.Login(adminLogin.PhoneNum, adminLogin.Pwd);
                    if (i)
                    {
                        return Json(new AjaxReault { Statin = "ok" });
                    }
                    else
                    {
                        return Json(new AjaxReault { Statin = "no" });
                    }
                }
                else
                {
                    return Json(new AjaxReault { Statin = "no" });
                }
            }
            else
            {
                return Json(new AjaxReault { Statin = "no" });
            }
        }
    }
}