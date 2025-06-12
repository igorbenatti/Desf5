using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Desf5.Application.Interfaces;
using Desf5.Application.Services;
using Desf5.Domain.Interfaces;
using Desf5.Infra.Data.Context;
using Desf5.Infra.Data.Repositories;

namespace Desf5.Infra.IoC;

public static class DependencyInjectionApi
{
    public static IServiceCollection AddInfrastructureApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<Desf5DbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Desf5"),
            b => b.MigrationsAssembly(typeof(Desf5DbContext).Assembly.FullName)));

        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IProdutoService, ProdutoService>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        return services;
    }
}
