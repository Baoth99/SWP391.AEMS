using AEMS.Data.EF;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AEMS.WebApi.SystemConfigurations
{
    internal static class DatabaseConnectionSetUp
    {
        public static void AddDatabaseConnectionSetUp(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentException(nameof(services));
            }

            services.AddDbContext<AppDbContext>();
        }
    }
}
