using EFConnStringPOC.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EFConnStringPOC.Helper
{
    public static class SetupHelper
    {
        public static IApplicationBuilder UseDatabaseInitialization(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<MainDbContext>().Database.Migrate();
            }

            return app;
        }
    }
}
