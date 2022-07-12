using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public static class RepositoriesRegistration
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<NetBankingContext>(option => option.UseSqlServer(configuration.GetConnectionString("NetBankingString"),m => m.MigrationsAssembly(typeof(NetBankingContext).Assembly.FullName)));
        }
    }
}
