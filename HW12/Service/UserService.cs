using HW12.Entities;
using HW12.Interface;
using HW12.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW12.Service
{
    public class UserService: IUserService
    {
        public IUserRepo UserRepo = new UserRepo();
        public User onlineUser;
        public Result Login(string email, string password)
        {
           List<User> users = UserRepo.GetAll();
            users.ForEach(u =>
            {
                if (u.Email == email && u.Password == password)
                { onlineUser = u; }
            });
            if(onlineUser != null)
            {
                return new Result(true, "Logged in successfuly.");
            }
            else
            {
                return new Result(false,"User wan not found.");
            }
        }

        public Result Logout()
        {
            onlineUser = null;
            return new Result(true, "Log out.");
        }

        public Result Register(string name,string email, string password)
        {
            if (UserRepo.DoesExist(email, password))
            {
                return new Result(false, "User already exists.");
            }
            else
            {
                User newuser = new User(name,email,password);
               return UserRepo.Add(newuser);                
            }
        }
    }
}
