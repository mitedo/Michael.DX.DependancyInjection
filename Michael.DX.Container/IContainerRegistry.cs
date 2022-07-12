using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Michael.DX.Container
{

    public interface IContainerRegistry
    {
        void RegisterInstance(Type type, object instance);
        void RegisterSingleton(Type from, Type to);
        void RegisterSingleton<TFrom, TTo>() where TTo : TFrom;
        bool IsRegistered(Type type);
        void RegisterSingleton<T>(Func<T> factoryMethod);
        void RegisterDelegate<T>(Func<T> factoryMethod);


    }
}
