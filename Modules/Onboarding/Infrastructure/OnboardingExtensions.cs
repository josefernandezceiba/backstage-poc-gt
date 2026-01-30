using MoMo.Modules.Onboarding.Core.Ports;
using MoMo.Modules.Onboarding.Features.RegisterUser;
using MoMo.Modules.Onboarding.Infrastructure.Adapters;

namespace MoMo.Modules.Onboarding.Infrastructure
{
    public static class OnboardingExtensions
    {
        public static IServiceCollection AddOnboardingServices(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserSqlRepository>();
            services.AddTransient<CreateUserService>();            
            return services;
        }
        
    }
}
