using Microsoft.EntityFrameworkCore;
using LedgerCore.Data;

var builder = WebApplication.CreateBuilder(args);

// Pull DB conneciton string from json & register DB
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
?? throw new InvalidOperationException("DB Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));


//expose endpoint for health check
builder.Services.AddHealthChecks()
     .AddDbContextCheck<AppDbContext>(name: "database");


var app = builder.Build();
app.MapHealthChecks("/health");

app.Run();
