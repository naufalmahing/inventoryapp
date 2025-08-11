using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using inventoryapp.Data;
using inventoryapp.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<inventoryappContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("inventoryappContext") ?? throw new InvalidOperationException("Connection string 'inventoryappContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpClient<AuthApiService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
