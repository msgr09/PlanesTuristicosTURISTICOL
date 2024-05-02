using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json; // Agrega esta directiva using para poder usar Newtonsoft.Json
using PlanesTuristicos.Models;
using PlanesTuristicos.Servicios.Contrato;
using PlanesTuristicos.Servicios.Implementacion;
using System;
using PlanesTuristicos.Data;
using PlanesTuristicos.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();


builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<PlanesTuristicosContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("Planes")));

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IProveedorService, ProveedorService>();
builder.Services.AddScoped<IPlanesTService, PlanesTService>();
builder.Services.AddScoped<IReservaService, ReservaService>();




builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.LoginPath = "/Inicio/InicirSesion";
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminOrUsuarioOrTurista",
        policy => policy.RequireAuthenticatedUser());
});





var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "perfil",
        pattern: "perfil",
        defaults: new { controller = "Inicio", action = "Perfil" }
    );

    endpoints.MapControllerRoute(
        name: "perfilProveedor",
        pattern: "perfil-proveedor",
        defaults: new { controller = "Inicio", action = "PerfilProveedor" }
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Inicio}/{action=Elegir}/{id?}"
    );
});
app.UseStaticFiles();

app.Run();
