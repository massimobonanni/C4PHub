using C4PHub.Core.Implementations;
using C4PHub.Core.Interfaces;
using C4PHub.StorageAccount.Implementations;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true)
    .AddJsonFile("appsettings.local.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

builder.Services.AddRateLimiter(_ => _
    .AddFixedWindowLimiter(policyName: "base", options =>
    {
        options.PermitLimit = 10;
        options.Window = TimeSpan.FromSeconds(1);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 2;
    }));

// Add services to the container.
builder.Services.AddScoped<IFeedService,CoreFeedService>();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IC4PPersistance,StorageAccountTablePersistance>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
