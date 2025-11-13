using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using HumanResource.Data;
using HumanResource.Interface;
using HumanResource.Interface.Common;
using HumanResource.Interface.Login;
using HumanResource.Models;
using HumanResource.Repositories.Common;
using HumanResource.Repositories.Employee;
using HumanResource.Repositories.Login;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "YourSessionCookieName"; // Give a name to your session cookie
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set the session timeout duration
    // Additional session options can be set here
});

builder.Services.AddControllersWithViews();
builder.Services.AddTransient<DapperDBContext>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<ICommonRepository, CommonRepository>();
builder.Services.AddScoped<ILookupRepository, LookupRepository>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
////builder.Services.AddScoped<AuthenticationMiddleware>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Login/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
// Resolve the middleware instance from the service provider
////////var authenticationMiddleware = app.Services.GetRequiredService<AuthenticationMiddleware>();

//////app.Use(async (context, next) =>
//////{
//////    // Invoke the middleware
//////    await authenticationMiddleware.InvokeAsync(context, next);
//////});
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
