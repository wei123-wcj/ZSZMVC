using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZSZ.Admin.Web.Models
{
    public class AdminUserEdit
    {
        public long Id { get; set; }
        [Required]
        [StringLength(16, MinimumLength = 2)]
        public string Name { get; set; }
        [Required]
        [StringLength(11, MinimumLength = 7)]
        public string PhoneNum { get; set; }
        public string Pwd { get; set; }
        [Required]
        public string Email { get; set; }
        public long? CityId { get; set; }
        [Required]
        public long[] RoleId { get; set; }
    }
}