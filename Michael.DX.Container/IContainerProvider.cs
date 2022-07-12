using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Michael.DX.Container
{
    public interface IContainerProvider
    {
        T Resolve<T>(ContainerResolverOverrides overrides = null);
        T Resolve<T>(Action<T> options);
        T Resolve<T>(ContainerResolverOverrides overrides, Action<T> options);
        object Resolve(Type T, ContainerResolverOverrides overrides = null);
    }
}
