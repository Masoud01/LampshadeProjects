using DiscountManagement.Configuration;
using InventoryManagement.Infrastructure.Configuration;
using ShopManagement.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
var connectionString = builder.Configuration.GetConnectionString("LampshadeDB");

// Add services to the container.
builder.Services.AddRazorPages();
//Services Project
ShopManagementBootstrapper.Configure(builder.Services, connectionString);
DiscountManagementBootstrapper.Configuration(builder.Services, connectionString);
InventoryBootstrapper.Configure(builder.Services, connectionString);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseDeveloperExceptionPage();
    //app.UseExceptionHandler("/Error");
    app.UseHsts();
}
//app.UseAuthentication();

app.UseHttpsRedirection();

app.UseStaticFiles();

//app.UseCookiePolicy();

app.UseRouting();

//app.UseAuthorization();

//app.UseCors("MyPolicy");

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    //endpoints.MapControllers();
});
app.Run();
