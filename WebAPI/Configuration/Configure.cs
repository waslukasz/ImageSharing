using System.Text;
using Infrastructure.Database;
using Infrastructure.EF.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace WebAPI.Configuration;

public static class Configure
{
    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(
                "CorsPolicy",
                builder =>
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
        });
    }

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services
            .AddDbContext<ImageSharingDbContext>()
            .AddIdentity<UserEntity, RoleEntity>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Lockout.MaxFailedAccessAttempts = 3;
            })
            .AddEntityFrameworkStores<ImageSharingDbContext>()
            .AddDefaultTokenProviders();
    }

    public static void ConfigureJwt(this IServiceCollection services, JwtSettings jwtSettings)
    {
        services.AddAuthorization(opt =>
        {
            opt.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build());
            opt.AddPolicy("Email", policy => { policy.RequireClaim("email"); });
        });
        services
            .AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;

                if (jwtSettings.Secret != null)
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                        ClockSkew = TimeSpan.FromSeconds(60)
                    };
                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenException))
                        {
                            context.Response.Headers.Add("Token-expired", "true");
                        }

                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject("401 Not authorized");
                        return context.Response.WriteAsync(result);
                    },
                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = 403;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject("403 Not authorized");
                        return context.Response.WriteAsync(result);
                    },
                };
            });
    }

    public static async void AddUsers(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetService<UserManager<UserEntity>>();

            if (await userManager.FindByNameAsync("admin") is null)
            {
                var adminAccount = new UserEntity()
                {
                    UserName = "admin",
                    Email = "admin@example.com",
                };
                await userManager.CreateAsync(adminAccount, "Test123#");
                await userManager.AddToRolesAsync(adminAccount, new[] { "Admin", "User" });
            }

            if (await userManager.FindByNameAsync("user") is null)
            {
                var userAccount = new UserEntity()
                {
                    UserName = "user",
                    Email = "user@example.com"
                };
                await userManager.CreateAsync(userAccount, "Test123#");
                await userManager.AddToRoleAsync(userAccount, "User");
            }
        }
    }

    public static async void AddRoles(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetService<RoleManager<RoleEntity>>();
            if (await roleManager.FindByNameAsync("admin") is null)
            {
                var role = new RoleEntity();
                role.Name = "Admin";
                await roleManager.CreateAsync(role);
            }

            if (await roleManager.FindByNameAsync("user") is null)
            {
                var role = new RoleEntity();
                role.Name = "User";
                await roleManager.CreateAsync(role);
            }
        }
    }
}