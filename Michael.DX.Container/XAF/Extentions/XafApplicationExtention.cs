using DevExpress.ExpressApp;
using Michael.DX.Container;

namespace DevExpress.ExpressApp
{
    public static class XafApplicationExtention
    {
        public static IContainerRegistry GetContainerRegistry(this XafApplication app) => ContainerHolder.Instance.ContainerRegistry;
        public static IContainerProvider GetContainerProvider(this XafApplication app) => ContainerHolder.Instance.ContainerProvider;

    }
}
