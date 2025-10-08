using rs=RiskService.API.Services;
using System;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);

// HttpClient pour les appels inter-services

builder.Services.AddHttpClient("DefaultClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7162/");
});

// Service de calcul de risque
builder.Services.AddSingleton<rs.RiskService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
