using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Core;
using DevExpress.ExpressApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Michael.DX.Container.XAF
{
    public class IoCControllersManager : ControllersManager
    {
        
        protected readonly Func<IObjectSpace> createObjectSpace;

        public IoCControllersManager(Func<IObjectSpace> createObjectSpace)
        {
            this.createObjectSpace = createObjectSpace;
        }

        protected virtual void RegisterDependencies(IContainerRegistry containerRegistry)
        {
            if (!containerRegistry.IsRegistered(typeof(IObjectSpace)))
                containerRegistry.RegisterDelegate(createObjectSpace);

        }


        protected override Controller CreateController(Controller sourceController, IModelApplication modelApplication)
        {
            Controller controller = base.CreateController(sourceController, modelApplication);

            InterceptForDependencyInjection(controller);

            return controller;
        }

        private void InterceptForDependencyInjection(Controller controller)
        {
            IEnumerable<MethodInfo> injectMethods = controller.GetType()
                                                              .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                                                              .Where(m => m.Name == "OnLoadedDependencies")
                                                              .OrderByDescending(m => m.GetParameters().Count());
            if (!injectMethods.Any())
            {

                 injectMethods = controller.GetType()
                                           .GetMethods(BindingFlags.Public| BindingFlags.Instance)
                                           .Where(m => m.Name == "OnLoadedDependencies")
                                           .OrderByDescending(m => m.GetParameters().Count());

            }
            if (!injectMethods.Any())
                return;

            RegisterDependencies(ContainerHolder.Instance.ContainerRegistry);

            MethodInfo injectMethod = injectMethods.FirstOrDefault();


            controller.Activated += (s, e) =>
            {
                var scope = ContainerHolder.Instance.DryIoCContainer.GetServiceRegistrations();
                InvokeInjection(injectMethod, controller);
            };
        }

        private void InvokeInjection(MethodInfo method, Controller controller)
        {
            object[] parameters = method.GetParameters().OrderBy(p => p.Position)
                                        .Select(p => ContainerHolder.Instance.ContainerProvider.Resolve(p.ParameterType))
                                        .ToArray();

            method.Invoke(controller, parameters);
        }
    }
}