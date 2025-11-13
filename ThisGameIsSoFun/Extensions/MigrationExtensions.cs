using Microsoft.EntityFrameworkCore;

namespace ThisGameIsSoFun.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<Database.ApplicationDbContext>();
                dbContext.Database.Migrate();
            }
        }
    }
}
