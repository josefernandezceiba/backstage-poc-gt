using MoMo.Common.Helpers;
using MoMo.Common.Helpers.Mediator;

namespace MoMo.Modules.Onboarding.Features.RegisterUser
{
    public static partial class CreateUserEndpoint
    {
        [ApiEndpoint("onboarding")]
        public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder builder)
        {
            builder.MapPost("/api/onboarding", async (CreateUserDto req, IMediator mediator, CancellationToken token) =>
            {
                await mediator.Send(req, token);
            });

            return builder;
        }
    }
}
