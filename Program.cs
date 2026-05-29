using Microsoft.EntityFrameworkCore;
using UsersTasksBackend.Context;
using UsersTasksBackend.Services.Implementations;
using UsersTasksBackend.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddDbContext<UsersTasksContext>(opt => 
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();