using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pdi_Car_Rent.Areas.Identity.Data
{
    public class Role
    {
        public int Id { get; set; }
        [Display(Name = "Rola")]
        public string RoleName { get; set; }
    }
}
