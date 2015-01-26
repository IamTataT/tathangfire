using System;
using Castle.MicroKernel.Registration;


namespace tathangfire.IoC
{
    public class WindsorRegistrar
    {
        public static void RegisterSingleton(Type interfaceType, Type implementationType)
        {
            IoC.Container.Register(Component.For(interfaceType).ImplementedBy(implementationType).LifeStyle.Singleton);
        }

        public static void Register(Type interfaceType, Type implementationType)
        {
            IoC.Container.Register(Component.For(interfaceType).ImplementedBy(implementationType).LifeStyle.HybridPerWebRequestTransient());
        }

        public static void Register(Type interfaceType, Object implementationInstance)
        {
            IoC.Container.Register(Component.For(interfaceType).Instance(implementationInstance)
                .LifeStyle.HybridPerWebRequestTransient().Named(Guid.NewGuid().ToString()).IsDefault());
        }

        public static void RegisterAllFromAssemblies(string a)
        {
            IoC.Container.Register(
                AllTypes.FromAssemblyNamed(a).Pick().WithService.DefaultInterfaces().Configure(t => t.LifeStyle.HybridPerWebRequestTransient()));
        }
    }
}