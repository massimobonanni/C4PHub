using C4PHub.Core.Implementations;
using C4PHub.Core.Interfaces;
using C4PHub.OpenAI.Implementations;
using C4PHub.Sessionize.Implementations;
using C4PHub.StorageAccount.Implementations;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.DependencyInjection;
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
builder.Services.AddRateLimiter(_ => _
    .AddFixedWindowLimiter(policyName: "newC4P", options =>
    {
        options.PermitLimit = 1;
        options.Window = TimeSpan.FromSeconds(1);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 2;
    }));

// Add services to the container.
builder.Services.AddC4PManager();
builder.Services.AddScoped<IC4PPersistance, StorageAccountTablePersistance>();
builder.Services.AddScoped<IC4PCalendarGenerator, CoreC4PCalendarGenerator>();

builder.Services.AddControllersWithViews();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseEasyAuth();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
