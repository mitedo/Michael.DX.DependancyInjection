using DryIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Michael.DX.Container
{
    internal class ContainerProvider : IContainerProvider
    {
        public T Resolve<T>(ContainerResolverOverrides overrides = null)
        {
            return (T)Resolve(typeof(T), overrides);
        }

        public T Resolve<T>(Action<T> options)
        {
            T item = Resolve<T>();
            options?.Invoke(item);
            return item;
        }

        public T Resolve<T>(ContainerResolverOverrides overrides, Action<T> options )
        {
            T item = Resolve<T>(overrides);
            options?.Invoke(item);
            return item;
        }

        public object Resolve(Type type, ContainerResolverOverrides overrides = null)
        {
            if (overrides != null)
                return ResolveComplex(type, overrides);
            try
            {
                return ContainerHolder.Instance.DryIoCContainer.Resolve(type);
            }
            catch (ContainerException)
            {
                return ResolveComplex(type, ContainerResolverOverrides.Empty);
            }
        }

        private object ResolveComplex(Type type, ContainerResolverOverrides overrides)
        {
            object ResolveComplexType(Type mytype)
            {
                foreach (ContainerResolverOverride myOverride in overrides)
                {
                    if (myOverride.Type == mytype)
                        return myOverride.Object;
                }
                return ContainerHolder.Instance.DryIoCContainer.Resolve(mytype);
            }

            IEnumerable<ConstructorInfo> injectMethods = type.GetConstructors().AsEnumerable();

            if (!injectMethods.Any())
                throw new NotImplementedException();

            var injectMethod = injectMethods.First().GetParameters();


            object[] parameters = injectMethods.First().GetParameters()
                            .OrderBy(p => p.Position)
                            .Select(p => ResolveComplexType(p.ParameterType))
                            .ToArray();

            return injectMethods.First().Invoke(parameters);
        }
    }
}
