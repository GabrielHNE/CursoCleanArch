using CleanArchMvc.Domain.Account;
using CleanArchMvc.Infra.IoC;

var builder = WebApplication.CreateBuilder(args);
 
// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllersWithViews();

// builder.Host.ConfigureWebHostDefaults( webBuilder => webBuilder.UseStartup>)

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Erro ao buscar o servico por conta do scopo https://stackoverflow.com/questions/71882183/net-6-inject-service-into-program-cs
var seedUserRoleInitial = app.Services.CreateScope().ServiceProvider.GetService<ISeedUserRoleInitial>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

if(seedUserRoleInitial != null)
{
    seedUserRoleInitial.SeedRoles();
    seedUserRoleInitial.SeedUsers();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
