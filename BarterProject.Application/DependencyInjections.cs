using System.Reflection;
using AutoMapper;
using BarterProject.Application.AutoMapper;
using BarterProject.Application.PipelineBehaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BarterProject.Application;

public static class DependencyInjections
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        });
        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);

        services.AddMediatR(Assembly.GetExecutingAssembly());
        
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviour<,>));


        return services;
    }
}