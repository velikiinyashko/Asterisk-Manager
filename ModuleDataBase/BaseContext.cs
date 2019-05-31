using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using ModuleDataBase.Models;

namespace ModuleDataBase
{

    public interface IBaseContext
    {
        DbSet<CdrModel> CdrModels { get; set; }

        int SaveChanges();
    }

    public class BaseContext : DbContext, IBaseContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(@"server=192.168.0.4;port=3306;database=asterisk;uid=asterisk;password=asterisk");
        }

        public DbSet<CdrModel> CdrModels { get; set; }

    }
}
