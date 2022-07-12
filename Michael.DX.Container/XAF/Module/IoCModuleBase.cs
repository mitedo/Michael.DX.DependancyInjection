using Michael.DX.Container;

namespace DevExpress.ExpressApp
{
    public class IoCModuleBase : ModuleBase
    {
        public IoCModuleBase():base()
        {
            RegisterDependantTypes(ContainerHolder.Instance.ContainerRegistry);
            ContainerProvider = ContainerHolder.Instance.ContainerProvider;
        }

        protected IContainerProvider ContainerProvider { get; private set; }
        protected virtual void RegisterDependantTypes(IContainerRegistry containerRegistry)
        {
            // register with the container that SomeService implements ISomeService
            // ISomeService is defined in the Infrastructure module, see app architecture diagram
            //containerRegistry.Register<MyApplication.Infrastructure.ISomeService, SomeService>();
        }

        public override void Setup(XafApplication application)
        {
            base.Setup(application);
        }

        /// <summary>
        /// This function runs after the application has all it's own dependancies. Meaning of this function is to start services e.a. From now on IObjectSpace is available
        /// </summary>
        /// <param name="application">The current Xaf Application</param>
        public virtual void StartServices(XafApplication application)
        {

        }
    }

}
