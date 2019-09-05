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
    public class AdminPermissionController : Controller
    {
        public IPermissionService Permission { get; set; }
        // GET: AdminRole
        //查询所有权限
        public ActionResult Index()
        {
            ViewData["count"] = Permission.GetTotalCount();
            return View(Permission.GetAll());
        }
        //添加权限
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(PermissionAdd p)
        {

            if (ModelState.IsValid)
            {
                //说明校验通过
                
                var id = Permission.Insert(p.Name, p.Description);
                //AJAX请求必须返回json数据
               

                return Json("ok");
            }

            else
            {
                return Json("no");
            }
        }
        //删除权限
        [HttpPost]
        public ActionResult Delete(long id)
        {
           bool b= Permission.MarkDeleted(id);
            if(b)
            {
                ViewData["count"] = Permission.GetTotalCount();
                return Json("ok");
                //return View("Index",Permission.GetAll());
            }else
            {
                return Json("no");
            }
        }
        //批量删除
        [HttpPost]
        public ActionResult DeleteAll(long[] selectIDs)
        {
            for (int i = 0; i < selectIDs.Length; i++)
            {
                bool b = Permission.MarkDeleted(selectIDs[i]);
            }
            return Json("ok");
        }
        //修改权限
        [HttpGet]
        public ActionResult Update(long id)
        {
            var permission = Permission.GetById(id);
            return View(permission);
        }
        [HttpPost]
        public ActionResult Update(PermissionEdit p)
        {
            if (ModelState.IsValid)
            {
                Permission.Update(p.Id, p.Name, p.Description);
                return Json( "ok" );
            }
            else
            {
                return Json("no");
            }
        }
        [HttpPost]
        public ActionResult Index(string name)
        {
            var permission = Permission.GetAll().Where(m=>m.Name.Contains(name)).ToArray(); 
            ViewData["count"] = Permission.GetTotalCount();
            return View(permission);
        }
    }
}