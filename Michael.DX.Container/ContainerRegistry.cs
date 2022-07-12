using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DryIoc;

namespace Michael.DX.Container
{
    internal class ContainerRegistry : IContainerRegistry
    {
        public ContainerRegistry(ContainerHolder container)
        {
            ContainerHolder = container;
        }
        private ContainerHolder ContainerHolder { get; }
        public bool IsRegistered(Type type) => ContainerHolder.DryIoCContainer.IsRegistered(type);
        public void RegisterInstance(Type type, object instance) => ContainerHolder.DryIoCContainer.RegisterInstance(type, instance, ifAlreadyRegistered: IfAlreadyRegistered.Replace);

        public void RegisterSingleton<TFrom, TTo>() where TTo : TFrom
        {
            ContainerHolder.DryIoCContainer.Register<TFrom, TTo>(Reuse.Singleton, ifAlreadyRegistered: IfAlreadyRegistered.Replace);
        }
        public void RegisterSingleton(Type from, Type to)
        {
            ContainerHolder.DryIoCContainer.Register(from, to, Reuse.Singleton);
        }
        public void RegisterSingleton<T>(Func<T> factoryMethod) => ContainerHolder.DryIoCContainer.RegisterDelegate(typeof(T), p => factoryMethod, Reuse.Singleton);

        public void RegisterDelegate<T>(Func<T> factoryMethod)
        {
            ContainerHolder.DryIoCContainer.RegisterDelegate<T>( r => factoryMethod(), ifAlreadyRegistered: IfAlreadyRegistered.Replace);
        }
    }
}
