using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MudBlazor.Services;
using System.Text;
using ToyoCarsClients.Application.Interfaces;
using ToyoCarsClients.Business.Interfaces;
using ToyoCarsClients.Business.Mapping;
using ToyoCarsClients.Business.Services;
using ToyoCarsClients.Infraestructure.Data;
using ToyoCarsClients.Infraestructure.Repositories;
using ToyoCarsClients.Infraestructure.Services;
using ToyoCarsClients.Web.Auth;
using ToyoCarsClients.Web.Components;
using ToyoCarsClients.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


//contexto de datos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseMySql(
//        connectionString,
//        ServerVersion.AutoDetect(connectionString), // O la versión específica de tu servidor MySQL
//        mySqlOptions =>
//        {
//            mySqlOptions.EnableRetryOnFailure(
//                maxRetryCount: 3,       // Número máximo de reintentos
//                maxRetryDelay: TimeSpan.FromSeconds(10), // Retraso máximo entre reintentos
//                errorNumbersToAdd: null  // Códigos de error MySQL adicionales a considerar transitorios (null para usar los predeterminados)
//            );
//        }
//    )
//);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        connectionString,
        sqlServerOptions =>
        {
            sqlServerOptions.EnableRetryOnFailure(
                maxRetryCount: 3,                  // Número máximo de reintentos
                maxRetryDelay: TimeSpan.FromSeconds(10), // Retraso máximo entre reintentos
                errorNumbersToAdd: null            // Códigos de error de SQL Server adicionales
            );
        }
    )
);


//automapper
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

//repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();

//services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();

//autenticacion
var key = builder.Configuration["Jwt:key"];

builder.Services.AddScoped<AuthenticationProviderJWT>();
builder.Services.AddScoped<AuthServiceLocal>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationProviderJWT>(x => x.GetRequiredService<AuthenticationProviderJWT>());
builder.Services.AddScoped<ILoginService, AuthenticationProviderJWT>(x => x.GetRequiredService<AuthenticationProviderJWT>());
builder.Services.AddScoped<InfoAutoDriveService>();
builder.Services.AddScoped<IEncuesta, EncuestaRepository>();

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultSignInScheme = "External";
})
   .AddCookie("External")
   .AddJwtBearer(config =>
   {
       config.RequireHttpsMetadata = false;
       config.SaveToken = true;
       config.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuerSigningKey = true,
           ValidateIssuer = false,
           ValidateAudience = false,
           ValidateLifetime = true,
           ClockSkew = TimeSpan.Zero,
           RoleClaimType = "role",
           IssuerSigningKey = new SymmetricSecurityKey
           (Encoding.UTF8.GetBytes(key!))
       };
   });

builder.Services.AddMudServices();
builder.Services.AddAuthenticationCore();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
