using HW12.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW12.Interface
{
    public interface IDutyRrepo
    {
        Result Add(Duty duty);
        Result Delete(Duty duty);
        Duty Get(string title);
        List<Duty> GetAll();
        Result Update();
        Duty GetbyId(int id);
    }
}
