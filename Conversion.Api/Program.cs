using AutoMapper;
using Balance.Api.ViewModels.Convert;
using Balance.Domain.Entities;
using Balance.Domain.Interfaces;
using Balance.Infrastructure.CrossCutting;
using Balance.Infrastructure.Data;
using Balance.Infrastructure.Data.Repository;
using Balance.Services.Services;
using Balance.Services.Validators;
using Bank.Api;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.   
builder.Services.AddDbContext<DatabaseContext>(p => p.UseInMemoryDatabase("account-bank"));
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IBaseRepository<Euro>, BaseRepository<Euro>>();
builder.Services.AddScoped<IEuroRepository, EuroRepository>();
builder.Services.AddScoped<IEuroService, EuroService>();
builder.Services.AddScoped<EuroXrefDailyService>();

builder.Services.AddScoped<IBaseRepository<AccountBank>, BaseRepository<AccountBank>>();
builder.Services.AddScoped<IAccountBankService, AccountBankService>();
builder.Services.AddScoped<IAccountBankRepository, AccountBankRepository>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));


builder.Services.AddMemoryCache();

builder.Services.AddValidatorsFromAssemblyContaining<AccountBankRequestValidator>();

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
