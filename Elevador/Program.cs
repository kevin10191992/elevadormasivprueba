using Coravel;
using Elevador.Context;
using Elevador.Interface;
using Elevador.Jobs;
using Elevador.Models;
using Elevador.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ElevatorContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("DB")));

builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IElevatorState, ElevatorStateService>();
builder.Services.AddScoped<IElevator, ElevatorService>();

builder.Services.AddScheduler();
builder.Services.AddTransient<ElevatorJob>();

var app = builder.Build();

using var scope = app.Services.CreateScope();
using var context = scope.ServiceProvider.GetService<ElevatorContext>();
if (context != null)
{
    context.Database.Migrate();
}

ElevatorRules rules = new ElevatorRules(app.Configuration);

Console.WriteLine("\n" + rules.HumanSpeechRules + "\n");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Services.UseScheduler(sh =>
{
    sh.Schedule<ElevatorJob>().EverySecond().PreventOverlapping("ElevatorJob");
});

app.Run();
