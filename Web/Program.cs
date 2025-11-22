using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Application.Validators;
using Application.Handlers;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
        options.UseInMemoryDatabase("PainManager"));
}
else
{
    builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}
builder.Services.AddScoped<ICreateTaskCommandHandler, CreateTaskCommandHandler>();
builder.Services.AddScoped<IUpdateTaskCommandHandler, UpdateTaskCommandHandler>();
builder.Services.AddValidatorsFromAssembly(typeof(CreateTaskCommandValidator).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Tasks}/{action=Index}/{id?}");

app.Run();
