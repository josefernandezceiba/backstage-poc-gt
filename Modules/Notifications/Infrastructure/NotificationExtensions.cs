using MoMo.Modules.Notifications.Features.EmailWelcome;

namespace MoMo.Modules.Notifications.Infrastructure
{
    public static class NotificationExtensions
    {
        public static IServiceCollection AddNotificationServices(this IServiceCollection services)
        {
            services.AddHostedService<EmailNotifier>();
            return services;
        }
    }
}
