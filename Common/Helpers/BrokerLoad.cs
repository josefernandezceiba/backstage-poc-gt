using RabbitMQ.Client;

namespace MoMo.Common.Helpers;

public enum MqAccess { RO, RW }

public static partial class Autoload
{
    public static IServiceCollection AddBrokerBus(this IServiceCollection services, IConfiguration config)
    {

        var conn = new ConnectionFactory
        {
            HostName = config.GetValue<string>("BROKER_HOST", "localhost"),
            UserName = config.GetValue<string>("BROKER_USER", "guest"),
            Password = config.GetValue<string>("BROKER_PASS", "guest"),
            Port = config.GetValue<int>("BROKER_PORT", 5672),
        };

        services.AddKeyedSingleton<IConnection>(MqAccess.RO, conn.CreateConnectionAsync().GetAwaiter().GetResult());

        services.AddKeyedSingleton<IConnection>(MqAccess.RW, conn.CreateConnectionAsync().GetAwaiter().GetResult());

        return services;
    }
}

