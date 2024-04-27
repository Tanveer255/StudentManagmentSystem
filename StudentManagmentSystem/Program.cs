using StudentManagmentSystem.Data;
using Microsoft.EntityFrameworkCore;
using StudentManagmentSystem.MiddleWare;
using StudentManagmentSystem.Infrastructure.Repositories;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<SchoolContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolContext")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<StudentManagmentSystem.Services.LogAnalyticService>();
builder.Services.AddScoped<StudentManagmentSystem.Infrastructure.Repositories.IAuditColumnTransformer, AuditColumnTransformer>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<SchoolContext>();

    context.Database.Migrate();

    DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseMiddleware<LogAnalyticMiddleware>();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
