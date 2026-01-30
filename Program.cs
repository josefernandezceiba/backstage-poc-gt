using Microsoft.EntityFrameworkCore;
using MoMo.Common.DataAccess;
using MoMo.Common.Helpers;
using MoMo.Common.Helpers.Mediator;
using MoMo.Modules.Notifications.Infrastructure;
using MoMo.Modules.Onboarding.Infrastructure;
using MoMoo.Common.Adapters;
using MoMoo.Common.Ports;
using Scalar.AspNetCore;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

var _config = builder.Configuration;
_config.AddEnvironmentVariables();
var currentEnv = Environment.GetEnvironmentVariable("CURRENT_MODULE") ?? "Modulith App";

Log.Logger = new LoggerConfiguration()
    .WriteTo.Async(a => a.Console())
    .CreateLogger();

builder.Services.AddSerilog();
builder.Services.AddSingleton<ITenantResolver, TenantResolver>();


builder.Services.AddOpenApi();

builder.Services.AddDbContext<ModuleContext>((svc, cfg) =>
{
    var tenantResolver = svc.GetRequiredService<ITenantResolver>();
    var connString = tenantResolver.BuildConnectionStringHelper();
    var host = Environment.GetEnvironmentVariable("DBHOST") ?? "127.0.0.1";    
    var port = Environment.GetEnvironmentVariable("DBPORT") ?? "4433";
    cfg.UseSqlServer(string.Format(_config.GetConnectionString("dbString")!, host, port));
});


builder.Services.AddCoreServices().AddBrokerBus(_config);

builder.Services.AddMediator();

switch (currentEnv)
{
    case "NOTIFY": builder.Services.AddNotificationServices(); break;
    case "ONBOARDING": builder.Services.AddOnboardingServices(); break;
    default:
        builder.Services.AddOnboardingServices();
        builder.Services.AddNotificationServices(); break;
}

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.MapModuleEndpoints();
    app.MapOpenApi();
    app.MapScalarApiReference();
}
else if (!string.IsNullOrEmpty(currentEnv))
{
    app.MapModuleEndpoints(currentEnv.ToLower());
}

app.Lifetime.ApplicationStopped.Register(Log.CloseAndFlush);

await app.RunAsync();

