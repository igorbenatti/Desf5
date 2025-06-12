using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using Desf5.Infra.IoC;

namespace Desf5.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructureApi(Configuration);
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = Configuration["ApiDocTitle"],
                    Description = "2025-4A - POS - Bootcamp Arquiteto(a) de Software | Atividade do Desafio Final",
                    Version = "v1"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddControllers();
            services.AddDistributedMemoryCache();
            
            services.ConfigureApplicationCookie(opts =>
            {
                opts.ExpireTimeSpan = TimeSpan.FromHours(1);
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(1);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options => options
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint($"/swagger/v1/swagger.json", "v1");
                s.DefaultModelsExpandDepth(-1);
                s.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
                s.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            app.UseSession();
            app.UseStatusCodePages();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
        }
    }
}
