using HW12.Entities;
using HW12.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW12.Interface
{
    public interface IDutyService
    {
        Result AddDuty(Duty duty);
        Result DeleteDuty(int id);
        Duty SearchUserDuty(string Title,User user);
        List<Duty> GetDutyList();
        Result EditDuty();
        Result ChangeDutyStat(int id,StateEnum state);

    }
}
