using HW12.Enum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW12.Entities
{
    public class Duty
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int order {  get; set; }
        public DateTime DueDate { get; set; }
        public StateEnum State { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public Duty() { }
        public Duty(string title, int order, DateTime dueDate, StateEnum state)
        {
            Title = title;
            this.order = order;
            DueDate = dueDate;
            State = state;
        }
        public override string ToString()
        {
            return $"{Title} --- {DueDate.ToString("yyyy/MM/dd")} --- {State} --- Order: {order}";
        }
    }
}
