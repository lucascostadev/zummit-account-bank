using Conversion.Api.ViewModels.Convert;
using Conversion.Domain.Entities;
using Conversion.Domain.Interfaces;
using Conversion.Infrastructure.CrossCutting;
using Conversion.Infrastructure.Data;
using Conversion.Infrastructure.Data.Repository;
using Conversion.Infrastructure.Hangfire;
using Conversion.Services.Services;
using Conversion.Services.Validators;
using FluentValidation;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.   
builder.Services.AddDbContext<DatabaseContext>(p => p.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IBaseRepository<Euro>, BaseRepository<Euro>>();
builder.Services.AddScoped<IEuroRepository, EuroRepository>();
builder.Services.AddScoped<IEuroService, EuroService>();
builder.Services.AddScoped<EuroXrefDailyService>();

builder.Services.AddValidatorsFromAssemblyContaining<EuroValidator>();

builder.Services.AddHangfire(configuration =>
{
    configuration.UseSimpleAssemblyNameTypeSerializer();
    configuration.UseRecommendedSerializerSettings();
    configuration.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
    {
        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
        QueuePollInterval = TimeSpan.Zero,
        UseRecommendedIsolationLevel = true,
        DisableGlobalLocks = true
    });
});

builder.Services.AddHangfireServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHangfireDashboard();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
await using var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
await dbContext.Database.MigrateAsync();

BackgroundJob.Enqueue<EuroXrefDailyJob>(x => x.Sync());
RecurringJob.AddOrUpdate<EuroXrefDailyJob>("sync", x => x.Sync(), Cron.Daily);

app.Run();
