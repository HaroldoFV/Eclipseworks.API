using Eclipseworks.API.Data.Contexts;
using Eclipseworks.API.Data.Repositories;
using Eclipseworks.API.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region Configure Services

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
 
builder.Services.AddResponseCompression();


var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<EclipseworksContext>(opt => opt.UseNpgsql(connectionString));
builder.Services.AddScoped<EclipseworksContext>();
builder.Services.AddScoped<IOfferRepository, OfferRepository>();

var app = builder.Build();

#endregion


#region Configure Pipeline

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

#endregion