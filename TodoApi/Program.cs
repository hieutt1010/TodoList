using Microsoft.EntityFrameworkCore;
using MongoDB.Bson.Serialization;
using TodoApi.Configurations;
using TodoApi.Context;
using TodoApi.Core.Models.User;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

services.AddGoogleAuthentication(configuration); // Authen with Google
//services.UseMongoDb(configuration); // Config MongoDb
var mongoDBSettings = configuration.GetSection("MongoDBSettings").Get<MongoDbSettings>();
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDBSettings"));
services.AddDbContext<MongoDbContext>(options =>
options.UseMongoDB(mongoDBSettings.ConnectionString ?? "", mongoDBSettings.DatabaseName ?? ""),
    ServiceLifetime.Transient, ServiceLifetime.Singleton);

services.RegisterServices(); // DI
services.AddCustomeCors(); // Cross-Origin 

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.AddCustomMiddleware();
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
app.Run();
