using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security
{
    public static class CoreSecurityServiceRegistration
    {
        public static IServiceCollection AddCoreSecurityServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
