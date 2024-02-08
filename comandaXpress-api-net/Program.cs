using comandaXpress_api_net;
using comandaXpress_api_net.db;
using comandaXpress_api_net.Services;
using comandaXpress_api_net.Services.IService;
using System.Configuration;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IAccesoDatos,  AccesoDatos>();
builder.Services.AddSingleton<IPedidoService,  PedidoService>();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddSingleton<IAutorizacionService, AutorizacionService>();
builder.Services.AddSingleton<IUsuarioService, UsuarioService>();

//builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddAutoMapper(typeof(MappingConfig));

//configuro politica de CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        //builder.WithOrigins("http://127.0.0.1:5500/src/views/carta.html")
        //       .AllowAnyHeader()
        //       .AllowAnyMethod();
        builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
    });
});

var key = builder.Configuration.GetValue<string>("JwtSettings:key"); //accedo al valor del .json
var keyBytes = Encoding.ASCII.GetBytes(key);

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    /*Esta configuración indica si se requiere que las solicitudes sean HTTPS (conexión segura). 
     En este caso, se establece en false, lo que significa que no es obligatorio que las solicitudes sean HTTPS.*/
    config.RequireHttpsMetadata = false;

    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = false, //no nos interesa quien solicita
        ValidateAudience = false, //no nos interesa desde donde esta solicitando el usuario
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero //no debe existir ningun tipo de desviacion del reloj en cuanto al tiempo de vida del token
    };

});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
