using System.IO;
using System.Reflection;
using CrossCutting.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureDependenciesRepository(Configuration);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "API de Demonstração",
                        Version = "v1",
                        Description = "API Somente para demonstração, muita coisa pode de ser melhorada para se colocar em produção. <br><br>Como exemplo de validação foi colocado para não deixar salvar com o valor unitário igual a zero e não deixar excluir caso a quantidade for maior que zero"
                    }
                );

                var directoryFiles = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                var xmlFiles = Directory.GetFiles(directoryFiles, "*.xml");
                foreach (var xmlFile in xmlFiles)
                {
                    var dllfile = Path.ChangeExtension(xmlFile, "dll");
                    //Ignora os arquivos que não são documentação
                    if (File.Exists(dllfile))
                    {
                        c.IncludeXmlComments(xmlFile);
                    }
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "application v1");
                c.RoutePrefix = "docs";
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseRewriter(new RewriteOptions().AddRedirect("^$", "docs"));
        }
    }
}
