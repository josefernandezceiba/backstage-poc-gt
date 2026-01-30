using MoMo.Common.Core.Events;
using MoMo.Common.Helpers;
using MoMo.Common.Helpers.Mediator;
using MoMo.Modules.Onboarding.Core;
using RabbitMQ.Client;



namespace MoMo.Modules.Onboarding.Features.RegisterUser
{
    public class CreateUserCommandHandler(
            CreateUserService _userService,
            ILogger<CreateUserCommandHandler> _logger,
            [FromKeyedServices(MqAccess.RW)] IConnection _conn, IHttpContextAccessor _context) : IRequestHandler<CreateUserDto, Unit>
    {

        readonly string queueName = typeof(UserCreatedEvent).Name;





        public async Task<Unit> HandleAsync(CreateUserDto request, CancellationToken cancellationToken)
        {
            var channel = await _conn.CreateChannelAsync(cancellationToken: cancellationToken);
            await channel.QueueDeclareAsync(queueName, false, false, false, null, cancellationToken: cancellationToken);
            var traceParent = _context.HttpContext!.TraceIdentifier;
            var props = new BasicProperties();
            props.Headers = new Dictionary<string, object?> 
            {
                { "requestId", traceParent }
            };

            var currentUser = new User(request.nid, request.name, request.email);
            await _userService.RegisterUser(currentUser, cancellationToken);
            await channel.BasicPublishAsync("", queueName, mandatory: true, props,
                ObjectSerializer.ToMessagePayload(new UserCreatedEvent(currentUser.Name, currentUser.Email)), cancellationToken: cancellationToken);
            _logger.LogInformation("User recorded and message sent to interested parties");    
            return Unit.Value;
        }

       
    }
}
