using CaptchaGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSZ.Admin.Web.App_Start;
using ZSZ.Admin.Web.Models;
using ZSZ.Common;
using ZSZ.IService;

namespace ZSZ.Admin.Web.Controllers
{
    public class DefaultController : Controller
    {
        public IAdminUserLogin Logins { get; set; }
        public IAdminUserService adminUserService { get; set; }
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
                if (adminLogin.Code == TempData["code"].ToString())
                {
                    bool i = Logins.Login(adminLogin.PhoneNum, adminLogin.Pwd);
                    if (i)
                    {
                        Session["LoginId"] = adminUserService.GetPhoneUpdate(adminLogin.PhoneNum);
                        return Json(new AjaxReault { Statin = "ok" });
                    }
                    else
                    {
                        return Json(new AjaxReault { Msg = "用户名或密码不正确" });
                    }
                }
                else
                {
                    return Json(new AjaxReault { Msg = "验证码不一致！" });
                }
            }
            else
            {
                return Json(new AjaxReault { Statin = "no",Msg=MVCHelper.GetValidMsg(ModelState) });
            }
        }
        public ActionResult Code()
        {
            string code = CommonHelper.CreateVerifyCode(4);
            TempData["code"] = code;
            MemoryStream memory = ImageFactory.GenerateImage(code, 55, 100, 20, 6);
            return File(memory, "image/jpeg");
        }
    }
}