using DataAccessLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TaskManagementAPI;
using TaskManagementAPI.Data;
using TaskManagementAPI.Helper;
using TaskManagementAPI.Interface;
using TaskManagementAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(policy =>
    policy.AddPolicy("AllowAll", b =>
        b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("TaskManagementDB"));
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddScoped<IUserServiceDAL, UserServiceDAL>();
builder.Services.AddScoped<IUserServiceBAL, UserServiceBAL>();
builder.Services.AddScoped<ITaskServiceDAL, TaskServiceDAL>();
builder.Services.AddScoped<ITaskServiceBAL, TaskServiceBAL>();
builder.Services.AddScoped<IAuthServiceDAL, AuthServiceDAL>();
builder.Services.AddScoped<IAuthServiceBAL, AuthServiceBAL>();



var appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();
var key = Encoding.UTF8.GetBytes(appSettings.JwtKey);

// Add authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = appSettings.Issuer,
        ValidAudience = appSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TaskManagement API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter token with Bearer."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddAuthorization();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!db.Users.Any())
    {
        db.Users.AddRange(
            new User
            {
                id = 1,
                username = "Admin",
                email = "admin@example.com",
                password = PasswordHelper.HashPassword("Admin@123"),
                role = Roles.Admin,
                createdAt = DateTime.UtcNow
            },
            new User
            {
                id = 2,
                username = "User",
                email = "user@example.com",
                password = PasswordHelper.HashPassword("User@123"),
                role = Roles.User,
                createdAt = DateTime.UtcNow
            }
        );
        db.SaveChanges();
    }

    if (!db.TaskDetails.Any())
    {
        db.TaskDetails.AddRange(
            new TaskDetail
            {
                id = 1,
                title = "Setup project structure",
                description = "Create initial solution and project setup",
                status = Status.Done,
                priority = Priority.High,
                assigneeId = 1,
                creatorId = 1,
                createdAt = DateTime.UtcNow.AddDays(-10),
                updatedAt = DateTime.UtcNow.AddDays(-7)
            },
            new TaskDetail
            {
                id = 2,
                title = "Implement authentication",
                description = "Add JWT-based authentication",
                status = Status.InProgress,
                priority = Priority.High,
                assigneeId = 2,
                creatorId = 1,
                createdAt = DateTime.UtcNow.AddDays(-5),
                updatedAt = DateTime.UtcNow.AddDays(-1)
            },
            new TaskDetail
            {
                id = 3,
                title = "Design UI mockups",
                description = "Prepare Figma wireframes",
                status = Status.Done,
                priority = Priority.Medium,
                assigneeId = 2,
                creatorId = 2,
                createdAt = DateTime.UtcNow.AddDays(-8),
                updatedAt = DateTime.UtcNow.AddDays(-6)
            },
            new TaskDetail
            {
                id = 4,
                title = "Write unit tests",
                description = "Add unit tests for User service",
                status = Status.ToDo,
                priority = Priority.Medium,
                assigneeId = 1,
                creatorId = 2,
                createdAt = DateTime.UtcNow.AddDays(-2),
                updatedAt = DateTime.UtcNow
            },
            new TaskDetail
            {
                id = 5,
                title = "Setup CI/CD pipeline",
                description = "Configure GitHub Actions workflow",
                status = Status.InProgress,
                priority = Priority.High,
                assigneeId = 1,
                creatorId = 1,
                createdAt = DateTime.UtcNow.AddDays(-4),
                updatedAt = DateTime.UtcNow.AddDays(-1)
            }
        );
        db.SaveChanges();
    }
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
