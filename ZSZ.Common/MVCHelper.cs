using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ZSZ.Common
{
   public class MVCHelper
    {
        public static string GetValidMsg(ModelStateDictionary keys)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var item in keys.Keys)
            {
                if (keys[item].Errors.Count==0)
                {
                    continue;
                }
                builder.Append("属性【").Append(item).Append("】错误: ");
                foreach (var error in keys[item].Errors)
                {
                    builder.AppendLine(error.ErrorMessage);
                }
            }
            return builder.ToString();
        }
        public static string ToQueryString(NameValueCollection mvc)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var item in mvc.AllKeys)
            {
                string value = mvc[item];
                builder.Append(item).Append("=").Append(Uri.EscapeDataString(value)).Append("&");
            }
            return builder.ToString().Trim('&');
        }
        public static string RemoveQueryString(NameValueCollection mvc,string name)
        {
            NameValueCollection newMvc = new NameValueCollection(mvc);
            newMvc.Remove(name);
            return ToQueryString(newMvc);
        }
        public static string UpdateQueryString(NameValueCollection mvc, string name,string value)
        {
            NameValueCollection newMvc = new NameValueCollection(mvc);
            if (newMvc.AllKeys.Contains(name))
            {
                newMvc[name] = value;
            }
            else
            {
                newMvc.Add(name, value);
            }
            return ToQueryString(newMvc);
        }
        public static string RenderViewToString(ControllerContext context, string viewPath, object Model=null)
        {
            ViewEngineResult viewEngineResult = ViewEngines.Engines.FindView(context, viewPath, null);
            if (viewEngineResult==null)
                throw new FileNotFoundException("View" + viewPath + "cannot be found.");
            var view = viewEngineResult.View;
            context.Controller.ViewData.Model = Model;
            using (var sw = new StringWriter())
            {
                var stx = new ViewContext(context, view, context.Controller.ViewData, context.Controller.TempData, sw);
                view.Render(stx, sw);
                return sw.ToString();
            }

        }
    }
}
