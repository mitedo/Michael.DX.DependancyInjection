using DevExpress.ExpressApp.Core;
using DevExpress.Utils.Extensions;
using Michael.DX.Container;
using Michael.DX.Container.XAF;
using System;

namespace DevExpress.ExpressApp.Win
{
    public class IoCWinApplication : WinApplication
    {
        public IoCWinApplication() : base()
        {
            RegisterDependantTypes(this.GetContainerRegistry());
            ContainerProvider = this.GetContainerProvider();
            this.SetupComplete += this.StartModuleServices;
        }

        protected virtual void RegisterDependantTypes(IContainerRegistry containerRegistry)
        {
        }

        protected virtual void StartModuleServices(object sender, EventArgs e)
        {
            this.SetupComplete -= this.StartModuleServices;
            this.Modules.ForEach(action: x =>
            {
                if (x is IoCModuleBase ioc)
                {
                    ioc.StartServices(this);
                }
            });

        }

        protected override ControllersManager CreateControllersManager()
        {
            return new IoCControllersManager(() => ObjectSpaceProvider.CreateObjectSpace());
        }
        /*
        public override void StopSplash()
        {
            this.Modules.ForEach(x =>
            {
                if (x is IoCModuleBase ioc)
                {
                    ioc.StartServices(this);
                }
            });
            base.StopSplash();
        }*/

        protected IContainerProvider ContainerProvider { get; private set; }
    }
}
