using Microsoft.Data.SqlClient;
using VolNal.Chat.Api.DAL.Factories;
using VolNal.Chat.Api.DAL.Factories.Interfaces;
using VolNal.Chat.Api.DAL.Repositories.Implementation;
using VolNal.Chat.Api.DAL.Repositories.Interfaces;
using VolNal.Chat.Api.Mapping;
using VolNal.Chat.Api.Services;

namespace VolNal.Chat.API;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        #region DataBase

        services.AddScoped<IDbConnectionFactory<SqlConnection>, MsConnectionFactory>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IChatRepository, ChatRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();

        #endregion

        // services.AddMemoryCache();
        services.AddSignalR();

        services.AddAutoMapper(typeof(MappingProfile));
        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
            app.UseCors(builder =>
            {
                builder.WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<ChatHub>("/hub");
            endpoints.MapDefaultControllerRoute();
        });
    }
}