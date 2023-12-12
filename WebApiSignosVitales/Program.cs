
using DataSignosVitales.Data;
using DataSignosVitales.Entities.NotaEnfermeriaModels;
using DataSignosVitales.Interfaces;
using LogicaSignosVitales.Exceptions;
using LogicaSignosVitales.Interfaces;
using LogicaSignosVitales.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;
using System.Text.Json;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = actionContext =>
    {
        var errorsInModelState = actionContext.ModelState
            .Where(x => x.Value.Errors.Count > 0)
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Errors
                    .Select(e => e.ErrorMessage.Contains("required") ? "requerido" :
                                 e.ErrorMessage.Contains("regular expression") ? $" no cumple con el formato esperado" :
                                 e.ErrorMessage.Contains("max length") ? $"{kvp.Key} es demasiado largo" :
                                 e.ErrorMessage)
                    .FirstOrDefault()
            );

        return new ObjectResult(errorsInModelState)
        {
            StatusCode = StatusCodes.Status422UnprocessableEntity
        };
    };
});



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSwaggerUI",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Signos Vitales", Version = "v1" });

    // Configuracion para la autenticacion con JWT en Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });
});



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:key"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });


builder.Services.AddAuthorization(options => {
    options.AddPolicy("SuperAdmin", policy => policy.RequireClaim("AdminType", "Admin"));
});

builder.Services.AddSqlServer<NotaEnfermeriaDbContext>(builder.Configuration.GetConnectionString("SignosVitales"));

builder.Services.AddScoped<INotaEnfermeriaDbContext, NotaEnfermeriaDbContext>();

builder.Services.AddScoped<INotaEnfermeriaService, NotaEnfemeriaService>();

builder.Services.AddScoped<ICensoService , CensoService>(); 

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    // Use CORS
    app.UseCors("AllowSwaggerUI");

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contabilidad");
        c.OAuthClientId("swagger");
        c.OAuthClientSecret("secret");
        c.OAuthRealm("realm");
        c.OAuthAppName("Mi API");
    });
}


app.Use(async (context, next) =>
{
    try
    {
        // Primero llamas al siguiente middleware en la cadena
        await next();
    }
    catch (DbUpdateException ex)
    {
        context.Response.StatusCode = 400; 
        var result = JsonSerializer.Serialize(new
        {
            Mensaje = ex.Message 
        });
        await context.Response.WriteAsync(result);
    }

    catch (Exception ex)
    {
        // Manejar excepciones aquí y devolver una respuesta JSON
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        var result = JsonSerializer.Serialize(new
        {
            Mensaje = $"Ah ocurrido un error inesperado en el servidor, por favor contactar al administrador del sistema. ({ex.Message})"
        });

        await context.Response.WriteAsync(result);
    }

    // Luego verificas el código de estado de la respuesta
    if (context.Response.StatusCode == 401)
    {
        var result = JsonSerializer.Serialize(new { Mensaje = "No se ha autenticado para realizar este proceso." });
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(result);
    }
    else if (context.Response.StatusCode == 403)
    {
        var result = JsonSerializer.Serialize(new { Mensaje = "No tienes permiso para realizar esta acción." });
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(result);
    }
});




app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
