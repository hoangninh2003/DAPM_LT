using DAPM_LT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAPM_LT.Design_Pattern.Froxy
{
    public class LoginService : IRoleManager
    {
        private readonly dapmEntities db;
        private readonly IRoleManager roleManager;

        public LoginService(dapmEntities dbContext, IRoleManager roleManager)
        {
            db = dbContext;
            this.roleManager = roleManager;
        }

        public LoginService(dapmEntities db)
        {
            this.db = db;
        }

        public bool ValidateLogin(LoginModel info)
        {
            var account = db.TaiKhoans.Any(r => r.Email == info.userMail && r.Matkhau == info.password);
            return account;
        }

        public string CheckUserRole(LoginModel info)
        {
            var taiKhoan = db.TaiKhoans.FirstOrDefault(u => u.PhanQuyen.TenQuyen == info.TenQuyen);
            if (taiKhoan != null)
            {
                return taiKhoan.PhanQuyen.TenQuyen;
            }
            else
            {
                return null;
            }
        }
    }
}