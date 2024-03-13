using MagicVilla_VillaAPI.DATA;
using MagicVilla_VillaAPI.Logging;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Repository;
using MagicVilla_VillaAPI.Repository.IReposository;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContect>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("defaultSqlConnection"));
});

builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IVillaRepository, VillaRepository>();
builder.Services.AddScoped<IVillaNumberRepository, VillaNumberRepository>();


//Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
//                    .WriteTo.File("log/villalog.txt", rollingInterval: RollingInterval.Day)
//                    .CreateLogger();
//builder.Host.UseSerilog();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ILogging, Logging>();

var app = builder.Build();

// Configure the HTTP request pipeline. 
//if (app.Environment.IsDevelopment())
//{

//}

app.UseSwagger();
app.UseSwaggerUI(option =>
{
    option.SwaggerEndpoint("/swagger/v1/swagger.json", "MagicVilla_Villa");
    option.SwaggerEndpoint("/swagger/v2/swagger.json", "MagicVilla_Villa");

    option.RoutePrefix=string.Empty;
}
    );

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
