using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Reverse_Text.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Reverse_TextContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Reverse_TextContext") ?? throw new InvalidOperationException("Connection string 'Reverse_TextContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddDbContext<DbContext>(options => options.UseSqlServer(
//builder.Configuration.GetConnectionString("DefaultConnection")
//));



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
    pattern: "{controller=ReverseText}/{action=Index}/{id?}");

app.Run();


