using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using UStart.API;
using UStart.Infrastructure.Context;

namespace UStart.API
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

            Bootstrap.Configure(services, Configuration);

        }

        /// <summary>
        /// CUIDADO!
        /// -----------------------------
        /// Quando alterar a ordem de contru��o do ApplicationBulder pode gerar problema da aplica��o        
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            Configure_IsDevelopment(app, env);

             app.UseCors(configure =>
            {
                configure.AllowAnyOrigin();
                configure.AllowAnyHeader();
                configure.AllowAnyMethod();
            });

            Configure_UseExceptionHandler(app, env);

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            this.InitializeDatabase(app);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
        public void Configure_IsDevelopment(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<UStartContext>().Database.Migrate();

                UStartContext dbContext = serviceScope.ServiceProvider.GetService<UStartContext>();
                                
                DatabaseInitializer.Initialize(dbContext);
            }
        }

        public void Configure_UseExceptionHandler(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the global error handler
            app.UseExceptionHandler((options) =>
            {
                options.Run(
                async context =>
                {
                    var ex = context.Features.Get<IExceptionHandlerFeature>();
                    string err = string.Format("StatusCode: {0}\n", context.Response.StatusCode);
                    string additionalInfo = "";
                    if (ex != null)
                    {
                        err += ex.Error.Message;
                        additionalInfo = ex.Error.StackTrace;
                    }

                    string appNameSpace = this.GetType().Namespace;

                    
                });
            });
            app.UseHsts();
        }
    }
}
