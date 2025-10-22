using GarageBuddy.Data;
using GarageBuddy.Data.Models;
using GarageBuddy.Data.Seeding;
using GarageBuddy.Web.Configurations;
using GarageBuddy.Web.Infrastructure.Common;
using GarageBuddy.Web.Infrastructure.Extensions;

using Microsoft.AspNetCore.Builder;

using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

using Serilog;

StaticLogger.EnsureInitialized();
Log.Information("Server starting ...");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.AddConfigurations().RegisterSerilog();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddDataTables();

    builder.Services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
        .AddRoles<ApplicationRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>();

    // Configure JWT Authentication
    var jwtSettings = builder.Configuration.GetSection("Jwt");
    var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false; // In production, this should be true
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidateAudience = true,
            ValidAudience = jwtSettings["Audience"],
        };
    });

    builder.Services.ConfigureCookiePolicy();
    builder.Services.ConfigureApplicationCookie();
    builder.Services.AddCors();

    builder.Services.AddSingleton<IHtmlSanitizer>(
        new HtmlSanitizer(new HtmlSanitizerOptions()));

    builder.Services.AddControllersWithViews()
        .AddMvcOptions(options => // TODO: Add model binder providers
        {
            options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
        });

    // Add response caching
    builder.Services.AddResponseCaching();

    // Add policies
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy(Policies.AdminPolicy, p =>
            p.RequireRole(Roles.Administrator));
        options.AddPolicy(Policies.ManagerPolicy, p =>
            p.RequireRole(Roles.Administrator, Roles.Manager));
        options.AddPolicy(Policies.MechanicPolicy, p =>
            p.RequireRole(Roles.Administrator, Roles.Manager, Roles.Mechanic));
    });

    var app = builder.Build();

    // Seed data on application startup
    using (var serviceScope = app.Services.CreateScope())
    {
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.Migrate();
        new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
    }

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseMigrationsEndPoint();
    }
    else
    {
        app.UseExceptionHandler($"{ErrorRoute}/500");
        app.UseHsts();
    }

    app.UseStatusCodePagesWithRedirects($"{ErrorRoute}/?statusCode={{0}}");
    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseCookiePolicy();
    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
    app.MapRazorPages();

    await app.RunAsync();
}
catch (Exception ex) when (!ex.GetType().Name.Equals("StopTheHostException", StringComparison.Ordinal))
{
    StaticLogger.EnsureInitialized();
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    StaticLogger.EnsureInitialized();
    Log.Information("Server stopping...");
    Log.CloseAndFlush();
}

// Added to fix Web.Tests project
public partial class Program
{
}
