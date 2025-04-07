using System.Reflection;
using AutoMapper;
using BarterProject.Application.AutoMapper;
using BarterProject.Application.PipelineBehaviours;
using BarterProject.Application.Services.Logging_Service;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace BarterProject.Application;

public static class DependencyInjections
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {

        Log.Logger = new LoggerConfiguration()
            .WriteTo.File("logs/app-log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        });

        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);

        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviour<,>));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddScoped<ILoggerService, LoggerService>();

        return services;
    }
}