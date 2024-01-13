using comandaXpress_api_net;
using comandaXpress_api_net.db;
using comandaXpress_api_net.Services;
using comandaXpress_api_net.Services.IService;
using System.Configuration;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IAccesoDatos,  AccesoDatos>();
builder.Services.AddSingleton<IPedidoService,  PedidoService>();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

//builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddAutoMapper(typeof(MappingConfig));

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
