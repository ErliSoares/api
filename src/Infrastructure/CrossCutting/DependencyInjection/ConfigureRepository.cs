using System;
using Data;
using Domain;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CrossCutting.DependencyInjection
{
    public static class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(this IServiceCollection serviceCollection,
            IConfiguration Configuration)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddScoped<IProdutoRepository, ProdutoRepository>();

            serviceCollection.AddDbContext<AppDbContext>(
                options => options.UseSqlite(Configuration.GetConnectionString("AppDbContext"))
            );
        }

        public static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<AppDbContext>();
                    context.Database.EnsureCreated();
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao iniciar o banco de dados", ex);
                }
            }
        }
    }
}
