using Bigschool_TH_11.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bigschool_TH_11.ViewModels
{
    public class UserManagementViewModel
    {
        public List<ApplicationUser> Users { get; set; }
        public List<IdentityRole> Roles { get; set; }
        public List<ChucNang> ChucNangs { get; set; }
        public List<QuyenTruyCap> QuyenTruyCaps { get; set; }
    }
}