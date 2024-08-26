using LemaApp.Models;
using Microsoft.AspNetCore.Hosting.Server;
using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var addSettings = new ConfigurationBuilder()
            .SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var app = builder.Build();

var dbSection = app.Configuration.GetSection("DataBaseConfiguration");

var usuario = dbSection.GetValue<string>("user");
var DatabaseName = dbSection.GetValue<string>("database");
var Server = dbSection.GetValue<string>("server");
var Password = dbSection.GetValue<string>("password");


string connstring = string.Format("Server={0}; database={1}; UID={2}; password={3}", Server, DatabaseName, usuario, Password);
var DbConnection = new MySqlConnection(connstring);
DbConnection.Open();

var teste = DbConnection.State;

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
