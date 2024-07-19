using Microsoft.EntityFrameworkCore;
using Migration.Data;
using Migration.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(pDBCOptions => pDBCOptions.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();
app.MapGet("/", () => "Hello World!");

app.Run();