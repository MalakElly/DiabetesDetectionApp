using FrontEnd.Services;

var builder = WebApplication.CreateBuilder(args);

//Services
builder.Services.AddRazorPages();
builder.Services.AddControllers();
//uthService
builder.Services.AddHttpClient("AuthService", client =>
{
    client.BaseAddress = new Uri("https://localhost:7282");
});

//NotesService
builder.Services.AddHttpClient("NotesApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:5199/api/");
});

//ApiGateway (pour PatientService + RiskService)
builder.Services.AddHttpClient("GatewayApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7162/");
});

// Injection du bon client selon le service
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("GatewayApi"));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
// pour
// HttpClient qui pointe sur ApiGateway
builder.Services.AddHttpClient<FrontEnd.Services.PatientService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7162"); // ApiGateway
});


var app = builder.Build();

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();


app.UseRouting();
app.UseAuthorization();

//Route par défaut vers /Home/Index
app.MapGet("/", context =>
{
    context.Response.Redirect("/Account/Login");
    return Task.CompletedTask;
});

app.MapRazorPages();
app.MapControllers();

app.Run();
