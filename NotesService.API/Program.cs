using MongoDB.Driver;
using NotesService.API.Config;
using ns=NotesService.API.Services;
using MongoDatabaseSettings = NotesService.API.Config.MongoDatabaseSettings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDatabaseSettings>(
    builder.Configuration.GetSection("MongoDatabaseSettings"));


builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = builder.Configuration.GetSection("MongoDatabaseSettings").Get<MongoDatabaseSettings>();
    return new MongoClient(settings.ConnectionString);
});


builder.Services.AddScoped<NotesRepository>();
builder.Services.AddSingleton<ns.NotesService>();

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











