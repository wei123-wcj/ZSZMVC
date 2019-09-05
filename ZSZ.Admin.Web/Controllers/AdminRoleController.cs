using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSZ.Admin.Web.Models;
using ZSZ.DTO;
using ZSZ.IService;
using ZSZ.Service;

namespace ZSZ.Admin.Web.Controllers
{
    public class AdminRoleController : Controller
    {
        public IRoleService Irole { get; set; }
        public IPermissionService Permission { get; set; }
        // GET: AdminRole
        //查询全部角色
        public ActionResult Index()
        {
            ViewBag.Tocount = Irole.GetTotalCount();
            return View(Irole.GetAll());
        }
        [HttpPost]
        public ActionResult Index(string name)
        {
            ViewBag.Tocount = Irole.GetTotalCount();
            return View(Irole.GetAll().Where(m=>m.Name.Contains(name)).ToArray());
        }
        //添加角色
        [HttpGet]
        public ActionResult Add()
        {
            var data = Permission.GetAll();
            return View(data);
        }
        [HttpPost]
        public ActionResult Add(RoleAdd role)
        {
            if(ModelState.IsValid)
            {
                var id= Irole.Insert(role.Name);
                Irole.RolePer(id, role.DesId);
                return Json("ok");
            }
            else
            {
                return Json("no");
            }
        }
        //修改角色
        [HttpGet]
        public ActionResult Update(long id)
        {
            var data = Irole.GetById(id);
            var GetRoles = Permission.GetPermissions(id);
            RoleDTO role = new RoleDTO { Id=data.Id, Name = data.Name, Permission = data.Permission, PermissionRoleId = GetRoles };
            return View(role);
        }
        [HttpPost]
        public ActionResult Update(RoleEdit role)
        {
           if(ModelState.IsValid)
            {
                Irole.Update(role.Id, role.Name,role.DesId);
                return Json("ok");
            }
            else
            {
                return Json("no");
            }

        }
        //删除角色
        [HttpPost]
        public ActionResult Delete(long id)
        {
            bool i = Irole.MarkDeleted(id);
            if (i)
            {
                ViewBag.Tocount = Irole.GetTotalCount();
                return Json("ok");
            }
            else
            {
                return Json("no");
            }
        }
        public ActionResult DeleteAll(long[] selectIDs)
        {
            for (int i = 0; i < selectIDs.Length; i++)
            {
                Irole.MarkDeleted(selectIDs[i]);
            }
            ViewBag.Tocount = Irole.GetTotalCount();
            return Json("ok");
        }
    }
}