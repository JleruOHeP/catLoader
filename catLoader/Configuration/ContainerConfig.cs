using Autofac;
using CatLoader.Providers;
using CatLoader.Services;

namespace CatLoader.Configuration
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<CatLoaderService>().As<ICatLoaderService>();
            builder.RegisterType<AggregationService>().As<IAggregationService>();
            builder.RegisterType<PersonProvider>().As<IPersonProvider>();            
            builder.RegisterType<PersonSourceConfig>().As<IPersonSourceConfig>();
            // add other registrations here...

            return builder.Build();
        }
    }
}