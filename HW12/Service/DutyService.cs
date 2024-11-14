using HW12.Entities;
using HW12.Enum;
using HW12.Interface;
using HW12.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW12.Service
{
    public class DutyService : IDutyService
    {
        IDutyRrepo DutyRepo = new DutyRepo();  
        public Result AddDuty(Duty duty)
        {
            if (duty == null) throw new ArgumentNullException();
            if(IsDutyPropNull(duty)) throw new ArgumentNullException();
            if(DutyRepo.Add(duty).IsDone)
            {
                return new Result(true,"Duty seuccessfuly Added.");
            }
            return new Result(false, "Could not Add duty.");
        }

        public Result ChangeDutyStat(int id, StateEnum state)
        {
            var duty = DutyRepo.GetbyId(id);
            if(duty == null)
            {
                return new Result(false, "Duty Could not be found! try again.");
            }
            duty.State = state;
            DutyRepo.Update();
            return new Result(true, "Duty status changed.");
        }

        public Result DeleteDuty(int id)
        {
          var duty = DutyRepo.GetbyId(id);
            if (duty == null)
            {
                return new Result(false, "Duty Could not be found! try again.");
            }
          return DutyRepo.Delete(duty);
        }

        public Result EditDuty()
        {
           return DutyRepo.Update();

        }

        public List<Duty>? GetDutyList()
        {
          var Dutylist = DutyRepo.GetAll();
            if(Dutylist == null)
            {
                throw new ArgumentNullException("There is no duty to be found.");
            }
            return Dutylist;
        }

        public Duty SearchUserDuty(string Title,User user)
        {
            try
            {
                return DutyRepo.GetAll().Where(d => d.UserId==user.Id).FirstOrDefault(d => d.Title == Title);
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }
        public bool IsDutyPropNull(Duty duty)
        {
            if (duty.DueDate == null || duty.State == null 
                || duty.Title == null || duty.order==null)
            {
                return true;
            }
            return false;
        }
        public Duty GetDuty(int id)
        {
            return DutyRepo.GetbyId(id);
        }
    }
}
