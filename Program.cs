var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => { options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.Name = "fehst.Session";
    options.Cookie.IsEssential = true;
});
string connection = System.Configuration.ConfigurationManager.ConnectionStrings["_connection"].ConnectionString;
builder.Configuration.GetConnectionString(connection);

var app = builder.Build();


if (!app.Environment.IsProduction())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=login}/{action=index}/{id?}");

app.Run();
