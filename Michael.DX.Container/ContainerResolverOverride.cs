using System;

namespace Michael.DX.Container
{
    public class ContainerResolverOverride
    {
        public Type Type { get; }
        public object Object { get; }
        public ContainerResolverOverride(Type type, object theobject)
        {
            this.Type = type;
            this.Object = theobject;
        }
    }
}