using backend.Database;
using FluentValidation.AspNetCore;
using backend.Dtos;
using backend.Repositories;
using backend.Services;
using Dapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never;
    })
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<TodoValidator>());

builder.Services.AddOpenApi();
builder.Services.AddScoped<SqlConnectionFactory>();  // Changed from Singleton to Scoped

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", builder => {
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
        builder.AllowAnyOrigin();
    });
});

builder.Services.AddScoped<ITodoItemRepository, TodoItemRepository>(); 
builder.Services.AddScoped<ITodoItemService, TodoItemService>();

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
app.MapOpenApi();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors("AllowAll");

app.Run();
