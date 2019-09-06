using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZSZ.Admin.Web.Models
{
    public class AdminLogin
    {
        [Required]
        [StringLength(11, MinimumLength = 7)]
        public string PhoneNum { get; set; }
        [Required]
        [StringLength(18, MinimumLength = 6)]
        public string Pwd { get; set; }
    }
}