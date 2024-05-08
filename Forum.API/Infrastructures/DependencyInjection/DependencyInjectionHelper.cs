﻿using Forum.API.Services.Interfaces;

namespace Forum.API.Infrastructures.DependencyInjection;

public static class DependencyInjectionHelper
{
    public static void DIConfigurator(this IServiceCollection services)
    { 
        //service
        services.AddScoped<IForumService, IForumService>();
    }
 }
