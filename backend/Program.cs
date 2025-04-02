using backend.Database;
using FluentValidation.AspNetCore;
using backend.Dtos;
using backend.Repositories;
using backend.Services;
using Dapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never;
    })
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<TodoValidator>());

// Configure JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                builder.Configuration.GetSection("AppSettings:Token").Value!)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddOpenApi();
builder.Services.AddScoped<SqlConnectionFactory>(); 

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        policy => policy
             .WithOrigins("http://localhost:5173", "https://lyhua-tech.github.io") 
            .AllowAnyMethod()                     
            .AllowAnyHeader()                      
            .AllowCredentials());                 
});
builder.Services.AddScoped<ITodoItemRepository, TodoItemRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ITodoItemService, TodoItemService>();
builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SqlConnectionFactory>();
    using var connection = db.CreateConnection();
    
    string createTableSql = @"
        IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TodoItems')
        BEGIN
            CREATE TABLE TodoItems (
            Id BIGINT PRIMARY KEY IDENTITY(1,1),
            Title NVARCHAR(200) NULL,
            IsCompleted BIT NOT NULL DEFAULT 0,
            Content NVARCHAR(MAX) NULL,
            CompleteAt DATETIME2 NULL,
            CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
            UpdatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
        );
        END;
    ";
    connection.Execute(createTableSql);
}

if (app.Environment.IsDevelopment())
{
    
}
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowLocalhost");  

app.MapControllers();
app.MapOpenApi();

app.Run();
