using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using UsersTasksBackend.Context;
using UsersTasksBackend.Services.Implementations;
using UsersTasksBackend.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var allowedOrigins = builder.Configuration
    .GetSection("Cors:AllowedOrigins")
    .Get<string[]>();

// Services
builder.Services.AddDbContext<UsersTasksContext>(opt => 
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<ITasksService, TasksService>();
builder.Services.AddControllers()
    .AddJsonOptions(opt => opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddRouting(opt => opt.LowercaseUrls = true);
builder.Services.AddCors(opt => 
    opt.AddDefaultPolicy(policy => 
        policy.WithOrigins(allowedOrigins)
        .AllowAnyHeader()
        .AllowAnyMethod()
        )
);
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();