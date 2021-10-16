using UStart.Domain.Contracts.Repositories;
using UStart.Domain.Workflows;
using UStart.Infrastructure.Context;
using UStart.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using System;
using System.Collections.Generic;
using UStart.Domain.Helpers.TokenHelper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using UStart.Domain.UoW;
using UStart.Infrastructure.UoW;

namespace UStart.API
{
    public class Bootstrap
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {            

            // Exemplo de claim policies
            services.AddAuthorization(options =>
            {
                options.AddPolicy("PolicyName", policy => policy.RequireClaim("SomeClaim"));
                
            });
            
            // Add CORS
            services.AddCors();

            // Add MVC stack
            //services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddMvcCore();
            services.AddControllersWithViews();

            // Add API versioning service
            services.AddApiVersioning(setup =>
            {
                setup.AssumeDefaultVersionWhenUnspecified = true;
                setup.DefaultApiVersion = new ApiVersion(1, 0);                               
                setup.ReportApiVersions = true;
            });

            services.AddControllers();
            
            ConfigureAuthentication(services, configuration);

            //Swagger configuration
            RegisterSwagger(services);

            // Register custom services
            RegisterDbContext(services, configuration);
            RegisterRepositories(services);
            RegisterWorkflows(services);

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void RegisterSwagger(IServiceCollection services) {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

                //carrega os comentários dos métodos quando utilizados com ///
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                        Reference = new OpenApiReference
                            {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });
        } 

        public static void RegisterDbContext(IServiceCollection services, IConfiguration configuration)
        {                        
            services.AddDbContext<UStartContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), b => 
                b.MigrationsAssembly("Infrastructure"))
                .UseSnakeCaseNamingConvention();
            });
        }

        public static void RegisterRepositories(IServiceCollection services)
        {
            // Register your repositories here
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
        }

        public static void RegisterWorkflows(IServiceCollection services)
        {
            // Register your workflows here
            services.AddTransient<UsuarioWorkflow, UsuarioWorkflow>();
        }

        public static void ConfigureAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            var tokenContextCfg = configuration.GetSection("TokenContext");
            var secretToken = tokenContextCfg.GetSection("SecretToken").Get<string>();
            var expiredInMinutes = tokenContextCfg.GetSection("ExpiresInMinutes").Get<int>();

            Domain.Helpers.TokenHelper.TokenContext tokenContext = new Domain.Helpers.TokenHelper.TokenContext(secretToken, expiredInMinutes);
            services.AddScoped<Domain.Helpers.TokenHelper.TokenContext, Domain.Helpers.TokenHelper.TokenContext>(token => tokenContext);

            services.AddTransient<UStart.Domain.Helpers.TokenHelper.TokenHelper>();


            // Add authetication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(builder =>
                {                                        
                    builder.RequireHttpsMetadata = false;
                    builder.SaveToken = true;
                    builder.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(tokenContext.getTokenBytes()),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        RequireExpirationTime =true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

        }
    }
}
