using System.Reflection;
using Infrastructure.Database.FileManagement;
using Infrastructure.EF.Repository.AlbumRepository;
using Infrastructure.EF.Repository.ReactionCommentRepository;
using Infrastructure.EF.Repository.UserRepository;
using Infrastructure.EventListener;
using Infrastructure.Utility;
using Microsoft.OpenApi.Models;
using WebAPI.Configuration;
using WebAPI.ExceptionFilter;
using WebAPI.Managers;
using WebAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<HttpResponseExceptionFilter>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Authentication and authorization
builder.Services.AddSingleton<JwtSettings>();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(new JwtSettings(builder.Configuration));
builder.Services.ConfigureCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme.
              Enter 'Bearer' and then your token in the text input below.
              Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,

            },
            new List<string>()
        }
    });

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ImageSharing",
    });
});

// User section
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Album section
builder.Services.AddScoped<AlbumSerivce>();
builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();

// Reaction section
builder.Services.AddScoped<ReactionService>();
builder.Services.AddScoped<IReactionRepository, ReactionRepository>();

// Image section
builder.Services.AddScoped<ImageManager>();

// Services
builder.Services.AddScoped<IAccountService, IAccountService>();
builder.Services.AddScoped<IAuthService, AuthManager>();
builder.Services.AddScoped<ImageEntityEventListener>();
builder.Services.AddScoped<FileManager>();
builder.Services.AddScoped<UniqueFileNameAssigner>();
builder.Services.ConfigureLiteX();
builder.Services.AddInfrastructures(builder.Configuration);
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.AddUsers();
app.AddRoles();
app.Run();