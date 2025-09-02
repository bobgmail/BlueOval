// Server/Program.cs


using BlueOval.Components;
using BlueOval.Login;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.RateLimiting;
using MudBlazor.Services;
using Scalar.AspNetCore;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddMudServices();
builder.Services.AddSignalR();
builder.Services.AddScoped<PdfUtility>();

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 100 * 1024 * 1024; // 100 MB
});


builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = 100 * 1024 * 1024; // 100 MB
});
builder.Services.AddRateLimiter(options =>
{
    // Define a global rate limit for all endpoints.
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
    {
        return RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 10,
                Window = TimeSpan.FromSeconds(15)
            });
    });

    // Define a named policy.
    options.AddFixedWindowLimiter("fixed", opts =>
    {
        opts.PermitLimit = 6;
        opts.Window = TimeSpan.FromSeconds(12);
        opts.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opts.QueueLimit = 4; // Queue up to 6 requests if the limit is exceeded.
    });

    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});
builder.Services.AddValidatorsFromAssemblyContaining<Program>();// Keep if validators are in Shared project
builder.Services.AddScoped<DapperContext>();
builder.Services.AddScoped<IAccountLogic, AccountLogic>(); // This will be injected into your API controllers

// --- Authentication and Authorization ---
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/ToLogin";
        options.Cookie.Name = "BlueOvalPark";
        //options.ExpireTimeSpan = TimeSpan.FromMinutes(120);
        options.Events.OnRedirectToLogin = context =>
        {
            if (context.Request.Path.StartsWithSegments("/api"))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
            else
            {
                context.Response.Redirect(context.RedirectUri);
            }
            return Task.CompletedTask;
        };
    })
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

// Add Authorization and define a policy for both schemes
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("BasicApiPolicy", policy =>
    {
        policy.AddAuthenticationSchemes("BasicAuthentication");//, CookieAuthenticationDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser();
    });
});

// --- API and Swagger ---
// This is required to discover the API endpoints
builder.Services.AddEndpointsApiExplorer();

// This adds the Swagger generator to the services collection
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "SAP API",
        Version = "v1",
        Description = "Modern API for managing product catalogs.",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "API Support",
            Email = "api@example.com"
        }
    });
});

// Your CORS policy configuration remains the same
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowClient", policy =>
        policy.WithOrigins("https://localhost:7123") // Replace with your client's URL
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials());
});

builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();

    // 1. UseSwagger() - This middleware generates the swagger.json file.
    app.UseSwagger();

    // 2. UseSwaggerUI() - This middleware serves the interactive Swagger UI.
    // This is the standard UI that comes with Swashbuckle.
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "SAP API V1");
        options.RoutePrefix = "swagger";
    });

    // 3. MapScalarApiReference() - This will now work after installing the package.
    // It provides an alternative UI to Swagger UI.
    app.MapScalarApiReference();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}




app.UseHttpsRedirection();

app.UseStaticFiles();



//app.UseCors("AllowClient"); // Apply the CORS policy

app.UseCors(builder =>
        builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
app.UseBlazorFrameworkFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();
// --- Map API Endpoints ---
// Your login/logout/register logic moves to API controllers

app.MapControllers();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlueOval.Client.Components.Pages.Home).Assembly);

app.Run();