using MoMo.Common.Helpers.Mediator;

namespace MoMo.Modules.Onboarding.Features.RegisterUser
{
    public record CreateUserDto(string nid, string name, string email) : IRequest<Unit>;
    
}
