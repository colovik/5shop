using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _5shop.Models
{
    public class AddToRoleViewModel
    {
        [Display(Name ="Email на корисникот")]
        public string email { get; set; }
        public List<string> allEmails { get; set; }
        [Display(Name = "Улоги во системот")]
        public List<Role> roles { get; set; }
        public string selectedRole { get; set; }
    }
}