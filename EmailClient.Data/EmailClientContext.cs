using EmailClient.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Data
{
    public class EmailClientContext : DbContext
    {
        public EmailClientContext()
            :base("name=EmailClientConnection")
        {
            Database.SetInitializer<EmailClientContext>(null);
        }

        public DbSet<EmailSetting> EmailSettingSet { get; set; }
        //public DbSet<EmailHistory> EmailHistorySet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Entity<EmailSetting>().HasKey<int>(e => e.Id);
           // modelBuilder.Entity<EmailHistory>().HasKey<string>(e => e.MailId);
           // base.OnModelCreating(modelBuilder);
        }

    }
}
