using _0_Framework.Application;
using DiscountManagement.Configuration;
using InventoryManagement.Infrastructure.Configuration;
using ServiceHost;
using ShopManagement.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
var connectionString = builder.Configuration.GetConnectionString("LampshadeDB");
//builder.Services.AddControllersWithViews();
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
//Services Project
ShopManagementBootstrapper.Configure(builder.Services, connectionString);
DiscountManagementBootstrapper.Configuration(builder.Services, connectionString);
InventoryBootstrapper.Configure(builder.Services, connectionString);
builder.Services.AddTransient<IFileUploader, FileUploader>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseHsts();
}
//app.UseAuthentication();

app.UseHttpsRedirection();

app.UseStaticFiles();


app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

});


app.Run();
