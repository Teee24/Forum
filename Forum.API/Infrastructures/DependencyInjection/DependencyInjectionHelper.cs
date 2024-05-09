using Forum.API.Infrastructures.Database;
using Forum.API.Repositories.Implements;
using Forum.API.Repositories.Interfaces;
using Forum.API.Services.Implements;
using Forum.API.Services.Interfaces;

namespace Forum.API.Infrastructures.DependencyInjection;

public static class DependencyInjectionHelper
{
    public static void DIConfigurator(this IServiceCollection services)
    { 
        //service
        services.AddScoped<IForumService, ForumService>();

        // repository
        services.AddScoped<IForumRepository, ForumRepository>();

        //other
        services.AddScoped<DatabaseConnHelper>();
    }
 }
