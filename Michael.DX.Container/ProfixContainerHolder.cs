using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using DryIoc;


namespace Michael.DX.Container
{
    /// <summary>
    /// This class will only be used when you'd like to make a special resolvement.
    /// </summary>
    internal class ContainerHolder
    {


        public DryIoc.Container DryIoCContainer {get;private set;}

        public IContainerRegistry ContainerRegistry { get; private set; }
        public IContainerProvider ContainerProvider { get;private set; }

        private static ContainerHolder _instance = null;
        public static ContainerHolder Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ContainerHolder();
                    _instance.DryIoCContainer = new DryIoc. Container();
                    _instance.ContainerRegistry = new ContainerRegistry(_instance);
                    _instance.ContainerProvider = new ContainerProvider();
                    _instance.ContainerRegistry.RegisterInstance(typeof(IContainerRegistry), _instance.ContainerRegistry);
                    _instance.ContainerRegistry.RegisterInstance(typeof(IContainerProvider), _instance.ContainerProvider);
                }
                return _instance;
            }
        }
    }
}
