using System.Configuration;
using Microsoft.Extensions.Options;
using PruebaTecnicaMasiv.Models;
using PruebaTecnicaMasiv.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<DbSettings>(builder.Configuration.GetSection(nameof(DbSettings)));
builder.Services.AddSingleton<IDbSettings>(d=>d.GetRequiredService<IOptions<DbSettings>>().Value);
builder.Services.AddSingleton<DiscountService>();
builder.Services.AddSingleton<SaleService>();

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
