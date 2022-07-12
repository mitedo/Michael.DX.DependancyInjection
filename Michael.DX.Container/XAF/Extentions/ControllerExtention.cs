using Michael.DX.Container;

namespace DevExpress.ExpressApp
{
    public static class ControllerExtention
    {
        public static IContainerProvider GetContainerProvider(this ViewController controller)
        {
            return ContainerHolder.Instance.ContainerProvider;
        }
        public static IContainerProvider GetContainerProvider(this DevExpress.Xpo.XPBaseObject xpo)
        {
            return ContainerHolder.Instance.ContainerProvider;
        }

    }
}
