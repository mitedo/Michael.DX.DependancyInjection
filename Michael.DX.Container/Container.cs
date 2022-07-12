using System;

namespace Michael.DX.Container
{
    public class Container
    {
        public static void Start(out IContainerRegistry registry, out IContainerProvider provider)
        {
            registry = ContainerHolder.Instance.ContainerRegistry;
            provider = ContainerHolder.Instance.ContainerProvider;
        }
    }
}
