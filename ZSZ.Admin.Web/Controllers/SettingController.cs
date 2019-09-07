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
    public class SettingController : Controller
    {
        public ISettingService SettingService { get; set; }
        // GET: Setting
        public ActionResult Index()
        {
            return View(SettingService.GetAll());
        }
        [HttpPost]
        public ActionResult Index(string name)
        {
            return View(SettingService.GetAll().Where(m=>m.Name.Contains(name)).ToArray());
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(SettingAddEdit set)
        {
            if (ModelState.IsValid)
            {
                SettingService.Insert(set.Name, set.Value);
                return Json(new AjaxReault { Statin = "ok" });
            }
            else
            {
                return Json(new AjaxReault { Statin = "no" });
            }
        }
        [HttpGet]
        public ActionResult Update(long id)
        {
            var data = SettingService.GetById(id);
            return View(data);
        }
        [HttpPost]
        public ActionResult Update(SettingAddEdit set)
        {
            if (ModelState.IsValid)
            {
                SettingService.Update(set.Id, set.Name, set.Value);
                return Json(new AjaxReault { Statin = "ok" });
            }
            else
            {
                return Json(new AjaxReault { Statin = "no" });
            }
        }
        public ActionResult Delete(long id)
        {
            bool i = SettingService.MarkDeleted(id);
            if(i)
            {
                return Json(new AjaxReault { Statin = "ok" });
            }
            else
            {
                return Json(new AjaxReault { Statin = "no" });
            }
        }
        public ActionResult DeleteAll(long[] selectIDs)
        {
            for (int i = 0; i < selectIDs.Length; i++)
            {
                SettingService.MarkDeleted(selectIDs[i]);
            }
            return Json(new AjaxReault { Statin = "ok" });
        }
    }
}