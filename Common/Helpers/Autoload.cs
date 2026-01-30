using MoMo.Common.Adapters;
using MoMo.Common.Ports;
using System.Reflection;

namespace MoMo.Common.Helpers;


public static partial class Autoload
{

    const string DEFAULT_MODULE = "default";

    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        return services;
    }

    public static WebApplication MapModuleEndpoints(this WebApplication app, string module = DEFAULT_MODULE)
    {

        var endpointMapClasses = Assembly.GetExecutingAssembly()
            .GetTypes()
             .SelectMany(t => t.GetMethods(BindingFlags.Static | BindingFlags.Public))
            .Where(m => m.GetCustomAttribute<ApiEndpointAttribute>() != null);


        foreach (var endpoint in endpointMapClasses)
        {
            var attrib = endpoint.GetCustomAttribute<ApiEndpointAttribute>();

            if (module == DEFAULT_MODULE || (attrib is not null && attrib.ModuleName == module))
            {
                endpoint.Invoke(null, [app]);                
            }
        }

        return app;
    }

}
