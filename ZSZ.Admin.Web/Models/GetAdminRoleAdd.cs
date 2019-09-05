using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZSZ.DTO;

namespace ZSZ.Admin.Web.Models
{
    public class GetAdminRoleAdd
    {
        public AdminUserDTO GetCity { get; set; }
        public CityDTO[] City { get; set; }
        public RoleDTO[] Role { get; set; }
        public RoleDTO[] GetAdminRole { get; set; }
    }
}