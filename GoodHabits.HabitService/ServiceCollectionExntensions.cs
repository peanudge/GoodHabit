
namespace GoodHabits.HabitService;

using GoodHabits.Database;
using Microsoft.EntityFrameworkCore;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAndMigrateDatabases(this IServiceCollection services, IConfiguration config)
    {
        var options = services.GetOptions<TenantSettings>(nameof(TenantSettings));
        var defaultConnectionString = options.DefaultConnectionString;

        if (defaultConnectionString is null)
        {
            throw new Exception("DefaultConnectionString is not defined.");
        }


        services.AddDbContext<GoodHabitsDbContext>(
            m => m.UseSqlServer(
                defaultConnectionString,
                e => e.MigrationsAssembly(typeof(GoodHabitsDbContext).Assembly.FullName)));

        // INFO: Migration
        // var tenants = options.Tenants ?? new();

        // foreach (var tenant in tenants)
        // {
        //     string connectionString;
        //     if (string.IsNullOrEmpty(tenant.ConnectionString))
        //     {
        //         connectionString = defaultConnectionString;
        //     }
        //     else
        //     {
        //         connectionString = tenant.ConnectionString;
        //     }
        //     using var scope = services.BuildServiceProvider().CreateScope();
        //     var dbContext = scope.ServiceProvider.GetRequiredService<GoodHabitsDbContext>();
        //     dbContext.Database.SetConnectionString(connectionString);

        //     if (dbContext.Database.GetMigrations().Count() > 0)
        //     {
        //         dbContext.Database.Migrate();
        //     }
        // }

        return services;
    }
    public static T GetOptions<T>(this IServiceCollection
     services, string sectionName) where T : new()
    {
        using var serviceProvider =
          services.BuildServiceProvider();
        var configuration =
          serviceProvider.GetRequiredService<
          IConfiguration>();
        var section = configuration.GetSection(
          sectionName);
        var options = new T();
        section.Bind(options);
        return options;
    }
}
