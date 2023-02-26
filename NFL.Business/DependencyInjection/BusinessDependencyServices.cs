using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFL.Business
{
    public static class BusinessDependancyServices
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            // service instantiations
            services.AddScoped<IGamesService, GamesService>();
            services.AddScoped<IAdminService, AdminService>();

            // return
            return services;
        }
    }
}
