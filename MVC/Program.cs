using Core.Entities.Identity;
using Core.Interfaces;
using DBaccess;
using DBaccess.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MyShopDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("MyShop"));
    opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
builder.Services.AddDbContext<IdentityDBContext>(opt => 
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Identity"));
});
//configuring passwords constraints and adding signin manager
builder.Services.AddIdentity<AppUser,IdentityRole>(opt=>
{
    opt.Password.RequireDigit=true;
    opt.Password.RequiredUniqueChars=1;
    opt.Password.RequireUppercase=true;
    opt.Password.RequiredLength=8;
    opt.Password.RequireLowercase=true;
}).AddEntityFrameworkStores<IdentityDBContext>()
                .AddDefaultTokenProviders()
                .AddRoles<IdentityRole>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddSignInManager<SignInManager<AppUser>>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitofWork, UnitOfWork>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


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

//seeding data at the run of the application 
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<MyShopDbContext>();
//var IdentityContext = services.GetRequiredService<AppIdentityDbContext>();
//var userManager = services.GetRequiredService<UserManager<AppUser>>();
var logger = services.GetRequiredService<ILogger<Program>>();
try
{
    await context.Database.MigrateAsync();
    //await IdentityContext.Database.MigrateAsync();
    //adding seed contents json file to the darabse 
    await MyShopDbSeed.SeedAsync(context);
    //await AppIdentityDbContextSeed.SeedusersAsync(userManager);
}
catch (Exception ex)
{
    logger.LogError(ex, "Error occured during migration");
}

app.Run();
