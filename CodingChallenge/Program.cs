

using CodingChallenge.Application.Interfaces;
using CodingChallenge.Application.Mappings;
using CodingChallenge.Application.Services;
using CodingChallenge.Infra;
using CodingChallenge.Infra.Repository;
using CodingChallenge.Infra.UnitOfWork;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<DbContext, AppDbContext>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddScoped<IEmployeeDeviceService, EmployeeDeviceService>();

var allowSpecificOrigins = "AllowedOrigins";

var origins = builder.Configuration.GetValue<string>("AllowedOrigins:Allowed");

if (!string.IsNullOrEmpty(origins))
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: allowSpecificOrigins,
            policy =>
            {
                policy.WithOrigins(origins.Split(',').ToArray())
                .AllowAnyMethod()
                .AllowAnyHeader();
            });
    });

}

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

}

app.UseCors(allowSpecificOrigins);
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
