using BankSystem.Application.Services.Implementations;
using BankSystem.Application.Services.Interfaces;
using BankSystem.Common.Configurations;
using BankSystem.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;

namespace BankSystem.Application
{
    //Registration of services that belong to the application layer
    public static class ApplicationModule
    {
        public static void AddApplicationModule([NotNull] this IServiceCollection services, IConfiguration configuration)
        {
            var authOptionsConfiguration = configuration.GetSection("Auth");

            services.Configure<AuthOptions>(authOptionsConfiguration);

            var autOptions = configuration.GetSection("Auth").Get<AuthOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = autOptions.Issuer,
                        ValidateAudience = true,
                        ValidAudience = autOptions.Audience,
                        ValidateLifetime = true,
                        IssuerSigningKey = autOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                    };
                });

            services.AddScoped<IQueryService, QueryService>();

            services.AddScoped<IUrlQueryService, UrlQueryService>();

            services.AddScoped<IClientService, ClientService>();

            services.AddScoped<IAccountService, AccountService>();

            services.AddPersistenceModule(configuration);

        }

    }
}

