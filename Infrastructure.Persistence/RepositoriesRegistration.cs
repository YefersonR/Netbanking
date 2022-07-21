using Core.Application.Interfaces.Repositories;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repositories;
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
            if (configuration.GetValue<bool>("DatabaseInMemory"))
            {
                services.AddDbContext<NetBankingContext>(option => option.UseInMemoryDatabase("InMemoryDB"));

            }
            else
            {
                services.AddDbContext<NetBankingContext>(option =>
                    option.UseSqlServer(configuration.GetConnectionString("NetBankingString"),
                    m => m.MigrationsAssembly(typeof(NetBankingContext).Assembly.FullName)));
            }

            services.AddTransient(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddTransient<IBeneficiaryRepository, BeneficiaryRepository>();
            services.AddTransient<ICreditCardRepository, CreditCardRepository>();
            services.AddTransient<ILoansRepository, LoansRepository>();
            services.AddTransient<ISavingsAccountRepository, SavingsAccountRepository>();
            services.AddTransient<ITransationsRepository, TransationsRepository>();

        }
    }
}
