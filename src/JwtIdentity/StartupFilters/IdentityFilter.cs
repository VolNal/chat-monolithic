using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace JwtIdentity.StartupFilters;

public class IdentityFilter : IStartupFilter
{
    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        return app =>
        {
            app.UseAuthentication();
            app.UseAuthorization();
            next(app);
        };
    }
}