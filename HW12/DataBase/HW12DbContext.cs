using HW12.Configuration;
using HW12.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW12.DataBase
{
    public class HW12DbContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-7648UU0\SQLEXPRESS; Initial Catalog=HW12; User Id=sa; Password=123456; TrustServerCertificate=True;");
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Duty> Duties { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
