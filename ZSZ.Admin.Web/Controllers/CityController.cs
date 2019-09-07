using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSZ.Admin.Web.Models;
using ZSZ.Common;
using ZSZ.IService;
using ZSZ.Service;

namespace ZSZ.Admin.Web.Controllers
{
    public class CityController : Controller
    {
       
       public ICityService CityService { get; set; } 
        // GET: Default
        public ActionResult Index()
        {
            var s = CityService.GetAll();
            return View(s);
        }
        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(string name)
        {
            var s = CityService.GetAll();
            return View(s.Where(m => m.Name.Contains(name)).ToArray());
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(CityAddEdit city)
        {
            if(ModelState.IsValid)
            {
                CityService.Insert(city.Name);
                return Json(new AjaxReault { Statin = "ok" });
            }
            else
            {
                return Json(new AjaxReault { Statin = "no" });
            }
        }
        public ActionResult Delete(long id)
        {
            bool i = CityService.MarkDeleted(id);
            if (i)
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
                CityService.MarkDeleted(selectIDs[i]);
            }
            return Json(new AjaxReault { Statin = "ok" });
        }
    }
}