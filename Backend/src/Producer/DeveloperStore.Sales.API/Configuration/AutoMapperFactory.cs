using AutoMapper;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace DeveloperStore.Sales.API.Configuration
{
    [ExcludeFromCodeCoverage]
    public static class AutoMapperFactory
    {
        public static IConfigurationProvider CreateMapperConfiguration(ILoggerFactory loggerFactory)
        {
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.AddMaps(new[] { Assembly.GetExecutingAssembly() });
                },
                loggerFactory
            );

            return config;
        }
    }
}