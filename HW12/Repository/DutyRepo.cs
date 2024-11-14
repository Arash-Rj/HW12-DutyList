using HW12.DataBase;
using HW12.Entities;
using HW12.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW12.Repository
{
    public class DutyRepo : IDutyRrepo
    {
        private HW12DbContext HW12Dbb = new HW12DbContext(); 
        public Result Add(Duty duty)
        {
            HW12Dbb.Duties.Add(duty);
            HW12Dbb.SaveChanges();
            return new Result(true);
        }

        public Result Delete(Duty duty)
        {
            HW12Dbb.Duties.Remove(duty);
            HW12Dbb.SaveChanges();
            return new Result(true,"Duty is deleted successfully.");
        }

        public List<Duty> GetAll()
        {
            var Duties = HW12Dbb.Duties.OrderByDescending(d => d.DueDate).ToList();
            if(Duties.Count==0)
            {
                return null;
            }
            return Duties;
        }

        public Duty Get(string title)
        {
            var Duty = HW12Dbb.Duties.First(d => d.Title == title);
            return Duty;
        }

        public Result Update()
        {
            HW12Dbb.SaveChanges();
            return new Result(true,"Duty updated successfuly.");
        }

        public Duty GetbyId(int id)
        {
            return HW12Dbb.Duties.FirstOrDefault(d =>d.Id == id);
        }
       
    }
}
