using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Data.AdoDotNet
{
    public abstract class AdoNetBaseClass<T>
    {
        protected IEnumerable<T> ToList(IDbCommand command)
        {
            using (var reader = command.ExecuteReader())
            {
                List<T> items = new List<T>();
                while (reader.Read())
                {
                    var item = CreateEntity();
                    Map(reader, item);
                    items.Add(item);
                }
                return items;
            }
        }

        protected abstract void Map(IDataRecord record, T entity);
        protected abstract T CreateEntity();
    }
}
