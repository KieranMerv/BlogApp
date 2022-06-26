using dotnet_BlogApp.Data;
using dotnet_BlogApp.Data.DbInitializer;
using dotnet_BlogApp.Data.Repositories;
using dotnet_BlogApp.Models.Domain;
using dotnet_BlogApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var appHost = builder.Configuration["AppHost"];
// Add services to the container.
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services
    .AddDbContext<BlogDbContext>(options => 
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });
builder.Services
    .AddIdentityCore<AppUser>(opt =>
    {
        opt.User.RequireUniqueEmail = true;
    })
    .AddRoles<AppRole>()
    .AddRoleManager<RoleManager<AppRole>>()
    .AddSignInManager<SignInManager<AppUser>>()
    .AddRoleValidator<RoleValidator<AppRole>>()
    .AddEntityFrameworkStores<BlogDbContext>();
builder.Services
    .AddAuthentication()
    .AddJwtBearer(options => 
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Server should vaidate token signing key with server's
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])),
            ValidateIssuer = true,
            ValidateAudience = true
        };
    });
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    .WithOrigins(appHost));

await SeedDatabase();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

async Task SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();

        await dbInitializer.Initialize();
    }
}