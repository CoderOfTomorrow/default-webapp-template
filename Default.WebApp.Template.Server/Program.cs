using Default.WebApp.Template.Infrastrucutre;
using Default.WebApp.Template.Server.Composition;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add custom services.
builder.Services.AddInfrastructure(configuration);
builder.Services.AddJwtIdentity(configuration);
builder.Services.AddPresentation();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseWebAssemblyDebugging();
else
    app.UseHsts();

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();