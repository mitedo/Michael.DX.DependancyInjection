using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Michael.DX.Container
{
    public class ContainerResolverOverrides : List<ContainerResolverOverride>
    {
        public static ContainerResolverOverrides Create(Type type, object Object)
        {
            var item = new ContainerResolverOverride(type, Object);
            var list = new ContainerResolverOverrides();
            list.Add(item);
            return list;
        }

        public static ContainerResolverOverrides Empty=> new ContainerResolverOverrides();

        public static ContainerResolverOverrides Create<T>(object Object)
        {
            return Create(typeof(T), Object);
        }
    }
}
