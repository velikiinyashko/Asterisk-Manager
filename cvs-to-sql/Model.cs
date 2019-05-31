using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using FileHelpers;

namespace cvs_to_sql
{
    #region описания контекста работы с базой
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CdrModel>().HasKey(k => new { k.calldate, k.dst, k.accountcode, k.uniqueid});
        }

        public DbSet<CdrModel> CdrModels { get; set; }

    }
    #endregion

    #region Модели описывающие таблицы в базе
    [Table("cdr")]
    public class CdrModel
    {
        [Key]
        public DateTime calldate { get; set; }
        public string src { get; set; }
        [Key]
        public string dst { get; set; }
        public string dcontext { get; set; }
        public string clid { get; set; }
        public string channel { get; set; }
        public string dstchannel { get; set; }
        public string lastapp { get; set; }
        public string lastdata { get; set; }
        public DateTime start { get; set; }
        public DateTime answer { get; set; }
        public DateTime end { get; set; }
        public int duration { get; set; }
        public int billsec { get; set; }
        public string disposition { get; set; }
        public int amaflags { get; set; }
        [Key]
        public string accountcode { get; set; }
        public string userfield { get; set; }
        [Key]
        public string uniqueid { get; set; }
    }
    #endregion
}
