using Api;

var builder = WebApplication.CreateBuilder(args);

var hafariStartup = new Startup(builder.Configuration); 

// setup hafari services
hafariStartup.ConfigureServices(builder.Services);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// setup hafari app config
hafariStartup.Configure(app, app.Environment);

app.UseBlazorFrameworkFiles();

app.MapFallbackToFile("index.html");

app.Run();