using Microsoft.AspNetCore.Mvc;
using ResponseCache.MiddleWare;

var builder = WebApplication.CreateBuilder(args);
var MaxAge = builder.Configuration.GetSection("MaxAge");
// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.CacheProfiles.Add("default", new CacheProfile()
    {
        Duration = MaxAge != null ? int.Parse(MaxAge.Value) : 5,
        Location = ResponseCacheLocation.Any,
    });
});
builder.Services.AddResponseCaching();

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
// app.UseCors()
app.UseMaxAgeRequestHeaderMiddleware();
app.UseResponseCaching();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
