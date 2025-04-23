using api_teste;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHostedService<QueueReader>();
builder.Services.AddSingleton<BackgroundQueueService<int>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("", ([FromServices] BackgroundQueueService<int> queue) =>
{
    for (int i = 0; i < 10; i++)
    {
        queue.Enqueue(Random.Shared.Next(0, 1001));
    }
});

app.UseHttpsRedirection();

app.Run();