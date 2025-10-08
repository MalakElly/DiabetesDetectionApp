using Microsoft.EntityFrameworkCore;
using PatientService.Infrastructure.Data;
using PatientService.Infrastructure.Repositories;
using PatientService.Core.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PatientDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Config de dependances
builder.Services.AddScoped<IPatientRepository, PatientRepository>();

builder.Services.AddControllers();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Patient Service API v1");
    c.RoutePrefix = "swagger"; 
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<PatientDbContext>();

    try
    {
        SeedData.Initialize(context);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[SeedData] Erreur : {ex.Message}");
    }
}

app.Run();
