using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Common.Bootstrapper
{
    public static class MefLoader
    {
        public static CompositionContainer Init()
        {
            return Init(null);
        }

        public static CompositionContainer Init(
            ICollection<ComposablePartCatalog> catalogParts)
        {
            AggregateCatalog catalog = new AggregateCatalog();

            if (catalogParts != null)
            {
                foreach (var part in catalogParts)
                {
                    catalog.Catalogs.Add(part);
                }
            }
            
            return new CompositionContainer(catalog);
        }

    }
}
