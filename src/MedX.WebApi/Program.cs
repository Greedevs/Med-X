using Serilog;
using MedX.Data;
using MedX.Service;
using MedX.Data.Contexts;
using MedX.Service.Helpers;
using MedX.Desktop.Extensions;
using MedX.Service.Extensions;
using MedX.Desktop.Middlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Data Layer
builder.Services.AddDataAccess(builder.Configuration);

// Add Service Layer
builder.Services.AddServices(builder.Configuration);

// Add Authorization
builder.Services.ConfigureSwagger();

/*// Logger
var logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(builder.Configuration)
                    .Enrich.FromLogContext()
                    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);*/

PathHelper.WebRootPath = Path.GetFullPath("wwwroot");
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// Init Accessor
app.InitAccessor();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseStaticFiles();

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
