global using Microsoft.AspNetCore.Mvc;
global using NFL.Business;
global using NFL.Common;
global using NFL.Database;
global using Microsoft.EntityFrameworkCore;
global using Serilog;
using Newtonsoft.Json;
using ILogger = Serilog.ILogger;

var builder = WebApplication.CreateBuilder(args);


ILogger logger = new LoggerConfiguration()
  .ReadFrom.AppSettings()
  .CreateLogger();

// add logger with singleton lifetime
builder.Services.AddSingleton(x => logger);

// prepare connection string
string? connectionString = builder.Configuration.GetConnectionString("DatabaseConnectionString");

// add supporting services
builder.Services.AddCommonServices();
builder.Services.AddBusinessServices();
builder.Services.AddRepositoryServices(connectionString);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
);

JsonConvert.DefaultSettings = () => new JsonSerializerSettings
{
    Formatting = Formatting.Indented,
    ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
    PreserveReferencesHandling = PreserveReferencesHandling.Objects
};

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: "CorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
