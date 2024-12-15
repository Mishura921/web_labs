using Lab_7.Pages.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ��������� ������� � ���������.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<CookbookContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("CookbookContext")));

var app = builder.Build();

// ��������� HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();