using Jsos3.Authorization;
using Jsos3.Grades;
using Jsos3.Groups;
using Jsos3.LecturerInformations;
using Jsos3.WeeklyPlan;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthorizationModule();
builder.Services.AddGradesModule();
builder.Services.AddGroupsModule();
builder.Services.AddLecturerInformationsModule();
builder.Services.AddWeeklyPlanModule();


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
