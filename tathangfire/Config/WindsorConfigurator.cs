using Castle.MicroKernel.Registration;
using Zeekpipo.Core.Service;
using Zeekpipo.CrowdFundingDomain.Repository;
using Zeekpipo.Infrastructure.Repository.Pledge;
using Zeekpipo.Infrastructure.Repository.Project;
using Zeekpipo.Service.Utils;
using tathangfire.IoC;

namespace tathangfire.Config
{
    public class WindsorConfigurator
    {
        public static void Configure()
        {
            WindsorRegistrar.Register(typeof(IHasher), typeof(Hasher));
            //IoC.IoC.Container.Register(
            //    Component.For<ISGExtendedProjectRepository, IProjectRepository>()
            //        .ImplementedBy<MemoryMockProjectRepository>().LifeStyle.Singleton);
            //IoC.IoC.Container.Register(
            //    Component.For<IPledgeRepository>()
            //        .ImplementedBy<InMemoryPledgeItemRepository>().LifeStyle.Singleton);
            IoC.IoC.Container.Register(
                Component.For<ISGExtendedProjectRepository, IProjectRepository>()
                    .ImplementedBy<SqlProjectRepository>().LifeStyle.HybridPerWebRequestTransient());
            IoC.IoC.Container.Register(
                Component.For<IPledgeRepository>()
                    .ImplementedBy<SqlPledgeRepository>().LifeStyle.HybridPerWebRequestTransient());
            WindsorRegistrar.Register(typeof(IProjectLanguage), new ProjectLanguage(new[] { "JP", "EN" }));
            WindsorRegistrar.RegisterAllFromAssemblies("Zeekpipo.SharedDomain");
            WindsorRegistrar.RegisterAllFromAssemblies("Zeekpipo.Core");
            WindsorRegistrar.RegisterAllFromAssemblies("Zeekpipo.CrowdFundingDomain");
            WindsorRegistrar.RegisterAllFromAssemblies("Zeekpipo.Data");
            WindsorRegistrar.RegisterAllFromAssemblies("Zeekpipo.Infrastructure");
            WindsorRegistrar.RegisterAllFromAssemblies("tathangfire");
        }
    }
}