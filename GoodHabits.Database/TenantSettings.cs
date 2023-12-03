
namespace GoodHabits.Database;

public class TenantSettings
{
    public string? DefaultConnectionString { get; set; }
    public List<Tenant>? Tenants { get; set; }
}

public class Tenant
{
    public string? ConnectionString { get; set; }
    public string? TenantName { get; set; }
}
