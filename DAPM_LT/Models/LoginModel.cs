using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DAPM_LT.Models
{
    public class LoginModel
    {
        [Key]
        [Display(Name = "Email")]
        public string userMail { get; set; }
        [Display(Name = "Mật khẩu")]
        public string password { get; set; }
    }
}