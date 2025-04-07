using BarterProject.DAL.SqlServer;
using BarterProject.Application;
using BarterProject.Repository.Common;
using BarterProject.Repository.Repositories;
using MediatR;
using System.Reflection;
using BarterProject.Security;
using BarterProject.Application.Security;
using BarterProject.Infrastructure;
using BarterProject.Middlewares;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerService();

var connectionString = builder.Configuration.GetConnectionString("MyConn");

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSqlServerServices(connectionString!);
builder.Services.AddApplicationServices();
builder.Services.AddAuthenticationDependency(builder.Configuration);
builder.Services.AddScoped<IUserContext, HttpUserContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//Custom Middlewares
app.UseMiddleware<RateLimitMiddleware>(5, TimeSpan.FromMinutes(1));
app.UseMiddleware<ExceptionHandlerMiddleWare>();

app.MapControllers();

app.Run();
