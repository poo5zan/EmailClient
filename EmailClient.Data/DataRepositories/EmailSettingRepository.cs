using EmailClient.Data.DataContracts;
using EmailClient.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Data.DataRepositories
{
    [Export(typeof(IEmailSettingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EmailSettingRepository : DataRepositoryBase<EmailSetting> , IEmailSettingRepository
    {
        public bool IsMailSettingAvailable(string email)
        {
            using (EmailClientContext entityContext = new EmailClientContext()) {
                var setting = (from e in entityContext.EmailSettingSet
                               where e.Email == email
                               select e).FirstOrDefault();
                if (setting == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        protected override EmailSetting AddEntity(EmailClientContext entityContext, EmailSetting entity)
        {
            return entityContext.EmailSettingSet.Add(entity);
        }

        protected override IEnumerable<EmailSetting> GetEntities(EmailClientContext entityContext)
        {
            return from e in entityContext.EmailSettingSet
                   select e;
        }

        protected override EmailSetting GetEntity(EmailClientContext entityContext, string id)
        {
            //return (from e in entityContext.EmailSettingSet
            //        where e.LoginEmail == id
            //        select e).FirstOrDefault();
            var r = (from e in entityContext.EmailSettingSet
                    where e.LoginEmail == id
                    select e).FirstOrDefault();
            return r;
        }

        protected override EmailSetting UpdateEntity(EmailClientContext entityContext, EmailSetting entity)
        {
            return (from e in entityContext.EmailSettingSet
                    where e.LoginEmail == entity.LoginEmail
                    select e).FirstOrDefault();
        }


    }
}
