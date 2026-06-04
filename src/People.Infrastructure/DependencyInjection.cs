using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using People.Domain.Interfaces;
using People.Infrastructure.Persistence;
using People.Infrastructure.Repositories;

namespace People.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") 
            ?? configuration.GetSection("ConnectionStrings:DefaultConnection").Value;

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new System.InvalidOperationException("Connection string 'DefaultConnection' not found in configuration.");
        }

        services.AddDbContext<PeopleDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IPersonRepository, PersonRepository>();

        return services;
    }
}
