using MoMo.Common.Core.Events;
using MoMo.Common.Helpers;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;


namespace MoMo.Modules.Notifications.Features.EmailWelcome
{
    public class EmailNotifier(ILogger<EmailNotifier> _logger,
            [FromKeyedServices(MqAccess.RO)] IConnection conn) : BackgroundService
    {





        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var channel = await conn.CreateChannelAsync(cancellationToken: stoppingToken);
            var queueName = typeof(UserCreatedEvent).Name;

            await channel.QueueDeclareAsync(queueName, false, false, false, null, cancellationToken: stoppingToken);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (asyncBasicConsumer, eventArgs) =>
            {                
                var @object = ObjectSerializer.ToObject<UserCreatedEvent>(eventArgs.Body.ToArray());
                _logger.LogInformation($"Enviando notificacion a {@object!.Name}, destino {@object!.Email}");
                await channel.BasicAckAsync(eventArgs.DeliveryTag, false);
                return;

            };

            await channel.BasicConsumeAsync(queueName, false, consumer, cancellationToken: stoppingToken);
            await Task.CompletedTask;
        }
    }

}
