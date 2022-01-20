var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



// 01 dotnet tool install --global dotnet-sql-cache
// 02 dotnet sql-cache create "Data Source=.;initial Catalog=DistributedCache_db;Integrated Security=True;" dbo tblCache
// 03 Config DistributedSqlServerCache
builder.Services.AddDistributedSqlServerCache(options =>
{
    options.ConnectionString = "Data Source=.;initial Catalog=DistributedCache_db;Integrated Security=True;";
    options.SchemaName = "dbo";
    options.TableName = "tblCache";
});

// builder.Services.AddDistributedMemoryCache();


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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


