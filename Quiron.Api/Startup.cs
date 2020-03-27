using AutoMapper;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quiron.Api.Configuracoes;
using Quiron.CrossCutting;
using Quiron.Service.AutoMapper;

namespace Quiron.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Version = Configuration.GetValue<string>("Application:Version");
            Name = Configuration.GetValue<string>("Application:Name");
        }

        public IConfiguration Configuration { get; }

        public string Version { get; }

        public string Name { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOData();
            services.AddControllers(mvcOptions => mvcOptions.EnableEndpointRouting = false);
            services.AddSwaggerSetup(Name, Version);
            services.AddAutoMapper(typeof(AutoMapping));
            services.RegisterDependencies();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.EnableFilter();
                s.DocumentTitle = Name + " | Documenta��o";
                s.InjectStylesheet("/swagger-ui/custom.css");
                s.SwaggerEndpoint("/swagger/v" + Version + "/swagger.json", Name + " V" + Version);
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseMvc(routeBuilder =>
            {
                routeBuilder.MapODataServiceRoute("ODataRoute", "OData", EdmModelConfig.GetEdmModel());
                routeBuilder.Select().Expand().Filter().OrderBy().MaxTop(100).Count();
                routeBuilder.EnableDependencyInjection();
            });
        }
    }
}