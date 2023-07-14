using author_api;
using author_api.AutoMapperProfile;
using author_api.Extensions;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(AutomapperProfile));


builder.Services.ConfigureDb();
builder.Services.ConfigureCors();
//builder.Services

var app = builder.Build();

app.UseHttpLogging();
app.UseSwagger();
app.UseSwaggerUI();


Helper.CreateDbIfNotExists(app);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("Cors");

app.Run();
