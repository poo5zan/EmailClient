using EmailClient.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Data
{
    public abstract class DataRepositoryBase<T> : DataRepositoryBase<T,EmailClientContext>
        where T : class, new()
    {
    }
}
