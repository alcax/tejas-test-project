using Microsoft.EntityFrameworkCore;
using SampleProject.Database;
using SampleProject.Interface;
using SampleProject.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Adding database service
builder.Services.AddDbContext<ApplicationDbContext>(item =>
item.UseSqlite(builder.Configuration.GetConnectionString("TaskDB")));


// Adding services 
builder.Services.AddScoped<IContactService, ContactService>();

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
    pattern: "{controller=Home}/{action=Contact}/{id?}");

app.Run();
