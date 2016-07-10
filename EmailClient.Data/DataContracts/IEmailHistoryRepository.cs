using EmailClient.Common.Contracts;
using EmailClient.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Data.DataContracts
{
    public interface IEmailHistoryRepository : IDataRepository<EmailHistory>
    {
    }
}
