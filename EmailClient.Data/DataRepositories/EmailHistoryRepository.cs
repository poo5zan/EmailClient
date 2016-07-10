//using EmailClient.Data.DataContracts;
//using EmailClient.Data.Entities;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.Composition;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace EmailClient.Data.DataRepositories
//{
//    [Export(typeof(IEmailHistoryRepository))]
//    [PartCreationPolicy(CreationPolicy.NonShared)]
//    public class EmailHistoryRepository : DataRepositoryBase<EmailHistory> , IEmailHistoryRepository
//    {
//        protected override EmailHistory AddEntity(EmailClientContext entityContext, EmailHistory entity)
//        {
//            return entityContext.EmailHistorySet.Add(entity);
//        }

//        protected override IEnumerable<EmailHistory> GetEntities(EmailClientContext entityContext)
//        {
//            return from e in entityContext.EmailHistorySet
//                   select e;
//        }

//        protected override EmailHistory GetEntity(EmailClientContext entityContext, string id)
//        {
//            return (from e in entityContext.EmailHistorySet
//                    where e.email == id
//                    select e).FirstOrDefault();
//        }

//        protected override EmailHistory UpdateEntity(EmailClientContext entityContext, EmailHistory entity)
//        {
//            return (from e in entityContext.EmailHistorySet
//                    where e.email == entity.email
//                    select e).FirstOrDefault();
//        }
//    }
//}
