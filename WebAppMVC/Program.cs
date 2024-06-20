using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using WebAppMVC.Models;
using System.Globalization;
using Infrastructure.Data.Usuario;
using Services;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

// CARREGAR JSON
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
					 .AddJsonFile(path: "appsettings.json", optional: true, reloadOnChange: true)
                     .AddJsonFile(path: $"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                     .AddEnvironmentVariables();
// CARREGAR CONFIGS
builder.Services.Configure<ClientSettings>(builder.Configuration.GetSection(key: "ClientSettings"));

//Context do Identity
builder.Services.AddDbContext<ControleAcessoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Contexts
builder.Services.AddScoped<ControleAcessoContext>();

//Repositories
builder.Services.AddScoped<IControleAcessoRepository, ControleAcessoRepository>();

// Services
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IAspNetUserService, AspNetUserService>();
builder.Services.AddScoped<IControleAcessoService, ControleAcessoService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IPerfilService, PerfilService>();
// UserManager



builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/login";
                    options.AccessDeniedPath = "/erro/403";
                });



var app = builder.Build();

app.UseExceptionHandler("/erro/500");
app.UseStatusCodePagesWithRedirects("/erro/{0}");
app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//////////////////////////////
app.UseAuthentication();
app.UseAuthorization();

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(culture: "pt-BR"),
    SupportedCultures = new[] { new CultureInfo(name: "pt-BR") },
    SupportedUICultures = new[] { new CultureInfo(name: "pt-BR") }
});



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Index}/{id?}");
app.Run();
