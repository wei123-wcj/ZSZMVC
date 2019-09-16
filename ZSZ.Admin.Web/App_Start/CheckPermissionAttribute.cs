using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZSZ.Admin.Web.App_Start
{
    [AttributeUsage(AttributeTargets.Method,AllowMultiple =true)]
    public class CheckPermissionAttribute: Attribute
    {
        public string Name { get; set; }
        public CheckPermissionAttribute(string name)
        {
            this.Name = name;
        }
    }
}