using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SpoiltAPI.Data;
using SpoiltAPI.Models.Interfaces;
using SpoiltAPI.Models.Services;
using NJsonSchema;
using NSwag.AspNetCore;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Formatters.Json;
using Newtonsoft.Json;

namespace SpoiltAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder().AddEnvironmentVariables();
            builder.AddUserSecrets<Startup>();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// Adds services to runtime
        /// </summary>
        /// <param name="services">Services to be used</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SpoiltContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                }
             );

            services.AddTransient<IMovie, MovieService>();
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(options => {
                var settings = options.SerializerSettings;
                settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            }); ;

            services.AddSwagger();
        }

        /// <summary>
        /// Configures HTTP request pipeline
        /// </summary>
        /// <param name="app">Applications</param>
        /// <param name="env">Environmental Variables</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseStaticFiles();

            // Register the Swagger generator and the Swagger UI middlewares
            app.UseSwaggerUi3WithApiExplorer(settings =>
            {
                settings.SwaggerUiRoute = "";
                settings.GeneratorSettings.DefaultPropertyNameHandling =
                    PropertyNameHandling.CamelCase;
                settings.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Spoilt API";
                    document.Info.Description = "A simple api for Movie Spoilers.";
                    document.Info.TermsOfService = "None";
                    
                };
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
