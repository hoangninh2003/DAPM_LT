using DAPM_LT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAPM_LT.Design_Pattern.Froxy
{
    public class LoginProxy : IRoleManager
    {
        private dapmEntities db = new dapmEntities();

        private LoginService _login;

        public LoginProxy()
        {
            _login = new LoginService(db);
        }
        public bool ValidateLogin(LoginModel info)
        {
            return _login.ValidateLogin(info);
        }

        public string CheckUserRole(LoginModel info)
        {
            return _login.CheckUserRole(info);
        }
    }
}