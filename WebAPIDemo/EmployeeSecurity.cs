using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeeDataAccess;

namespace WebAPIDemo
{
    public class EmployeeSecurity
    {
        

        public static bool Login(string uname, string psw)
        {
            EmployeeDBEntities entities = new EmployeeDBEntities();
            
            return entities.Users.Any(u => u.UserName.Equals(uname, StringComparison.OrdinalIgnoreCase) && u.Password==psw);
        }
    }
}