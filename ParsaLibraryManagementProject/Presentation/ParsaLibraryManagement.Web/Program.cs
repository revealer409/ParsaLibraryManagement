using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ParsaLibraryManagement.Application.Configuration;
using ParsaLibraryManagement.Application.Interfaces;
using ParsaLibraryManagement.Application.Interfaces.ImageServices;
using ParsaLibraryManagement.Application.Mappings;
using ParsaLibraryManagement.Application.Services;
using ParsaLibraryManagement.Application.Validators;
using ParsaLibraryManagement.Domain.Entities;
using ParsaLibraryManagement.Domain.Interfaces;
using ParsaLibraryManagement.Domain.Interfaces.ImageServices;
using ParsaLibraryManagement.Domain.Interfaces.Repository;
using ParsaLibraryManagement.Infrastructure.Data.Contexts;
using ParsaLibraryManagement.Infrastructure.Data.Repositories;
using ParsaLibraryManagement.Infrastructure.Services.ImageServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Services

#region DataBase Context

//Add dbContext services 
builder.Services.AddDbContext<ParsaLibraryManagementDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ParsaLibraryManagementSQLServerConnection")));

#endregion

#region Identity

builder.Services.AddIdentity<User, Role>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<ParsaLibraryManagementDBContext>()
.AddDefaultTokenProviders();

builder.Services.AddAuthentication();


#endregion

#region IOC

#region Core

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<BookCategoryValidator>());



#endregion

#region Services

builder.Services.AddScoped<IRepositoryFactory, RepositoryFactory>();
var fileUploadOptions = builder.Configuration.GetSection("FileUploadOptions").Get<FileUploadOptions>();
builder.Services.AddSingleton(fileUploadOptions);
builder.Services.AddScoped<ImageFileValidationService>();
builder.Services.AddScoped<ImageFileValidationService>();
builder.Services.AddTransient<IBookCategoryServices, BookCategoryServices>();
builder.Services.AddTransient<IBooksCategoryRepository, BooksCategoryRepository>();
builder.Services.AddTransient<IImageServices, ImageServices>();
builder.Services.AddTransient<IImageFileValidationService, ImageFileValidationService>();

#endregion

#endregion

#endregion

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
