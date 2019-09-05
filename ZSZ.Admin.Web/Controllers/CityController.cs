using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            //CityService = new CityService();
            var s = CityService.GetAll();
            return Content(s.Length.ToString());
        }
    }
}