using BankSystem.Persistence.Context;
using BankSystem.Persistence.Repositories.Implementations;
using BankSystem.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace BankSystem.Persistence
{
    public static class PersistenceModule
    {
        public static void AddPersistenceModule([NotNull] this IServiceCollection services, IConfiguration configuration)
        {
            string connection = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<BankSystemContext>(options =>
                options.UseSqlServer(connection));

            services.AddScoped<IUrlQueryRepository, UrlQueryRepository>();

            services.AddScoped<IClientRepository, ClientRepository>();
        }
    }


}
