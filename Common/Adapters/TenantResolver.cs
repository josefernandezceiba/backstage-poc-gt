using MoMoo.Common.Ports;

namespace MoMoo.Common.Adapters;

public class TenantResolver : ITenantResolver
{

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;

    public TenantResolver(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
    {
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
    }

    public string BuildConnectionStringHelper()
    {
        // Get the tenant identifier from the x-tenant-id header.
        var tenantId = _httpContextAccessor.HttpContext?.Request.Host.Value;

        if (string.IsNullOrEmpty(tenantId))
        {
            // Handle cases where the tenantId is missing, e.g., throw an exception
            // or use a default connection string.
            throw new Exception("Tenant ID not found in x-tenant-id header.");
        }

        // Return the connection string from configuration based on the tenantId.
        var connectionString = _configuration.GetConnectionString(tenantId);

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new Exception($"Connection string for tenant '{tenantId}' not found.");
        }

        return connectionString;
    }

}
