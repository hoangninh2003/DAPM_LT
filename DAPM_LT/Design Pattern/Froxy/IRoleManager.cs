using DAPM_LT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAPM_LT.Design_Pattern.Froxy
{
    public interface IRoleManager
    {
        bool ValidateLogin(LoginModel info);
        string CheckUserRole(LoginModel info);
    }
}