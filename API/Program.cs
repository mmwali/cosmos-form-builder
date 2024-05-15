using API.Data;
using API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var options = new DbContextOptionsBuilder<AppDbContext>()
    .UseCosmos(builder.Configuration
    .GetConnectionString("DefaultConnection")!, "JobApp");
await AppDbContext.SeedDatabaseAsync(options.Options);

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseCosmos(builder.Configuration
    .GetConnectionString("DefaultConnection")!, "JobApp"));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IProgramService, ProgramService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
