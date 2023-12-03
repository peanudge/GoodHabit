namespace GoodHabits.HabitService;

using GoodHabits.Database;
using Microsoft.Extensions.Options;

public class TenantService : ITenantService
{
    private readonly TenantSettings _tenantSettings;
    private HttpContext? _httpContext;
    private Tenant? _tenant;

    public TenantService(
        IOptions<TenantSettings> tenantSettings,
        IHttpContextAccessor httpContextAccessor)
    {
        _tenantSettings = tenantSettings.Value;
        _httpContext = httpContextAccessor.HttpContext;

        if (_httpContext != null)
        {
            if (_httpContext.Request.Headers.TryGetValue("tenant", out var tenantId))
            {
                SetTenant(tenantId!);
            }
            else
            {
                throw new Exception("Invalid Tenant!");
            }
        }
    }

    private void SetTenant(string tenantId)
    {
        _tenant = _tenantSettings.Tenants!.Where(a => a.TenantName == tenantId).FirstOrDefault();

        if (_tenant == null)
        {
            throw new Exception("Invalid tenant");
        }

        if (string.IsNullOrEmpty(_tenant.ConnectionString))
        {
            SetDefaultConnectionStringToCurrentTenant();
        }
    }

    private void SetDefaultConnectionStringToCurrentTenant()
    {
        _tenant!.ConnectionString = _tenantSettings.DefaultConnectionString;
    }

    public string? GetConnectionString() => _tenant?.ConnectionString;
    public Tenant? GetTenant() => _tenant;
}
