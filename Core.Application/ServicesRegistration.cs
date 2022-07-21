using Core.Application.Interfaces.Services;
using Core.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application
{
    public static class ServicesRegistration
    {
        public static void AddAplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IGenericService<,,>),typeof(GenericService<,,>));
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IBeneficiaryService, BeneficiaryService>();
            services.AddTransient<ICreditCardService, CreditCardService>();
            services.AddTransient<ILoansService, LoansService>();
            services.AddTransient<ISavingsAccountService, SavingAccountService>();
            services.AddTransient<ITransationService, TransationsService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

        }
    }
}
