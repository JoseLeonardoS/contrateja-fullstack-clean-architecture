using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Application.Abstractions.Services;
using ContrateJa.Infrastructure.Persistence;
using ContrateJa.Infrastructure.Repositories;
using ContrateJa.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContrateJa.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IJobRepository, JobRepository>();
        services.AddScoped<IProposalRepository, ProposalRepository>();
        
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ITokenGenerator, TokenGenerator>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}