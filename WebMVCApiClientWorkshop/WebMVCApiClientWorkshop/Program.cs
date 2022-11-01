using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebMVCApiClientWorkshop.Data;
using WebMVCApiClientWorkshop.Services;
using WebMVCApiClientWorkshop.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<WebMVCApiClientWorkshopContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebMVCApiClientWorkshopContext") ?? throw new InvalidOperationException("Connection string 'WebMVCApiClientWorkshopContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<IPartyService, PartyService>(c =>
c.BaseAddress = new Uri("https://localhost:7190/"));

builder.Services.AddHttpClient<ICharacterService, CharacterService>(c =>
c.BaseAddress = new Uri("https://localhost:7190/"));

builder.Services.AddHttpClient<ICharacterInventoryService, CharacterInventoryService>(c =>
c.BaseAddress = new Uri("https://localhost:7190/"));

builder.Services.AddHttpClient<IAbilitiesService, AbilitiesService>(c =>
c.BaseAddress = new Uri("https://localhost:7190/"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
