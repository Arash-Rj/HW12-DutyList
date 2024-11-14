using HW12.DataBase;
using HW12.Entities;
using HW12.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW12.Repository
{
    public class UserRepo : IUserRepo
    {
        private HW12DbContext HW12Db = new HW12DbContext();
        public Result Add(User user)
        {
            HW12Db.Users.Add(user);
            HW12Db.SaveChanges();
            return new Result(true,"User Added.");
        }

        public bool DoesExist(string email, string password)
        {
            bool IsName = HW12Db.Users.Any(u => u.Name==email);
            bool Ispass = HW12Db.Users.Any(u => u.Password==password);
            return IsName && Ispass;
        }

        public User Get(int id)
        {
           return HW12Db.Users.FirstOrDefault(u => u.Id == id);
        }

        public List<User> GetAll()
        {
            return HW12Db.Users.Include(u => u.Duties).ToList();
        }
    }
}
