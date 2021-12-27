using Microsoft.EntityFrameworkCore;

using DI_EFCore.Models;

using DI_EFCore.Repositories;
using DI_EFCore.Repositories.Interfaces;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();

var connectionString = builder.Configuration.GetConnectionString("Main");
var serverVersion = new MySqlServerVersion(new Version(8, 0, 25));

builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, serverVersion));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseDeveloperExceptionPage();
}

app.MapControllers();

app.Run();
