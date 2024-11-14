using HW12.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW12.Interface
{
    public interface IUserRepo
    {
        Result Add(User user);
        User Get(int id);
        List<User> GetAll();
        bool DoesExist(string name, string password);
    }
}
