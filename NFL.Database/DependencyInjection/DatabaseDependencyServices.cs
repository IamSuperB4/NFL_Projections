using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFL.Database
{
    /// <summary>
     /// Create Dependency Injection objects for repository services
     ///     - Used for HostBuilder
     /// </summary>
    public static class DatabaseDependencyServices
    {
        /// <summary>
        /// Create Dependency Injection objects for repository services
        ///     - Used for HostBuilder
        /// </summary>
        /// <param name="services">Repository services</param>
        /// <param name="connectionString">Connection string to database</param>
        /// <returns>Dependency Injection objects for repository services</returns>
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services, string? connectionString)
        {
            // chaser context factory
            services.AddDbContext<NflContext>(options =>
                options
                    .ConfigureWarnings(w => w.Throw(RelationalEventId.MultipleCollectionIncludeWarning))
                    .UseSqlServer(connectionString, o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)),
                ServiceLifetime.Scoped);

            // repository instantiation
            services.AddScoped<IGamesRepository, GamesRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();

            /*
             * If adding another database
            if (!string.IsNullOrWhiteSpace(secondConnectionString))
            {
                services.AddDbContext<Context2>(options => options.UseSqlServer(secondConnectionString), ServiceLifetime.Scoped);
                services.AddScoped<IRepository2, Repository2>();
            }
            */

            // return
            return services;
        }
    }
}
