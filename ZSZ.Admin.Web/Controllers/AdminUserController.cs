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
    public class AdminUserController : Controller
    {
        public IAdminUserService UserService { get; set; }
        public IRoleService RoleService { get; set; }
        // GET: AdminUser
        [HttpGet]
        public ActionResult Index()
        {
            return View(UserService.GetAll());
        }
        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(string name)
        {
            return View(UserService.GetAll().Where(m=>m.Name.Contains(name)).ToArray());
        }
        /// <summary>
        /// 查询修改时手机号是否被注册
        /// </summary>
        /// <param name="PhoneNum"></param>
        /// <returns></returns>
        public ActionResult GetPhone(string PhoneNum,long adminId)
        {
            long i = UserService.GetPhoneUpdate(PhoneNum);
            if(adminId==i)
            {
                return Json(new AjaxReault { Statin = "ok" });
            }
            else
            {
                return Json(new AjaxReault { Statin = "no" });
            }
        }
        /// <summary>
        /// 查询添加时手机号是否被注册
        /// </summary>
        /// <param name="PhoneNum"></param>
        /// <returns></returns>
        public ActionResult GetPhoneAdd(string PhoneNum)
        {
            bool i = UserService.GetPhoneAdd(PhoneNum);
            if (i)
            {
                return Json(new AjaxReault { Statin = "ok" });
            }
            else
            {
                return Json(new AjaxReault { Statin = "no" });
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Add()
        {
            var city = UserService.GetCities();
            var Role = RoleService.GetAll();
            GetAdminRoleAdd getAdmin = new GetAdminRoleAdd
            {
                City = city,
                Role = Role,
            };
            return View(getAdmin);
        }
        [HttpPost]
        public ActionResult Add(AdminUserAdd UserAdd)
        {
            if (ModelState.IsValid)
            {
                bool a=  UserService.GetPhoneAdd(UserAdd.PhoneNum);
                if (a)
                {
                    UserService.Insert(UserAdd.Name, UserAdd.PhoneNum, UserAdd.Pwd, UserAdd.Email, UserAdd.CityId, UserAdd.RoleId);
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
        
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Update(long id)
        {
            var GetUser = UserService.GetById(id);
            if (GetUser.CityId == null)
            {
                GetUser.CityId = 0;
            }
            var city = UserService.GetCities();
            var Role = RoleService.GetAll();
            var GetUserRole = RoleService.GetAdminRole(id);
            GetAdminRoleAdd getAdmin = new GetAdminRoleAdd
            {
                GetCity = GetUser,
                City = city,
                Role = Role,
                GetAdminRole = GetUserRole,
            };
            return View(getAdmin);
        }
        [HttpPost]
        public ActionResult Update(AdminUserEdit AdminUser)
        {
            if (ModelState.IsValid)
            {
                var a = UserService.GetPhoneUpdate(AdminUser.PhoneNum);
                if(a!=0)
                {
                    UserService.Update(AdminUser.Id, AdminUser.Name, AdminUser.PhoneNum, AdminUser.Pwd, AdminUser.Email, AdminUser.CityId, AdminUser.RoleId);
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
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(long id)
        {
            bool i=UserService.MarkDeleted(id);
            if (i)
            {
                return Json(new AjaxReault { Statin = "ok" });
            }
            else
            {
                return Json(new AjaxReault { Statin = "no" });
            }
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="selectIDs"></param>
        /// <returns></returns>
        public ActionResult DeleteAll(long[] selectIDs)
        {
            for (int i = 0; i < selectIDs.Length; i++)
            {
                UserService.MarkDeleted(selectIDs[i]);
            }
            return Json(new AjaxReault { Statin = "ok" });
        }
    }
}