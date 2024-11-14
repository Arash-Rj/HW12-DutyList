using HW12.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW12.Interface
{
    public interface IUserService
    {
        Result Login(string username,string password);
        Result Register(string name,string email,string password);
        Result Logout();
    }
}
