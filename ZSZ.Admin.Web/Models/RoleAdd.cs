using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZSZ.Admin.Web.Models
{
    public class RoleAdd
    {
        [Required]
        public long[] DesId { get; set; }
        [Required]
        [StringLength(16, MinimumLength = 2)]
        public string Name { get; set; }
    }
}