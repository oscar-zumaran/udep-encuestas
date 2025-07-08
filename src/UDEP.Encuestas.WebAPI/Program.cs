using UDEP.Encuestas.Business.Services;
using UDEP.Encuestas.DataAccess.Repositories;
using UDEP.Encuestas.DataAccess.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Data.SqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Configuración de JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();

// Configuración de Dapper y repositorios
builder.Services.AddTransient<IDbConnection>(sp => new SqlConnection(
    builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IDepartamentoRepository, DepartamentoRepository>();
builder.Services.AddScoped<IPeriodoRepository, PeriodoRepository>();
builder.Services.AddScoped<IComponenteRepository, ComponenteRepository>();
builder.Services.AddScoped<IAsignaturaRepository, AsignaturaRepository>();
builder.Services.AddScoped<ICapituloRepository, CapituloRepository>();
builder.Services.AddScoped<IActividadRepository, ActividadRepository>();
builder.Services.AddScoped<IAsignacion_Actividad_CapituloRepository, Asignacion_Actividad_CapituloRepository>();
builder.Services.AddScoped<IAsignacion_Componente_ActividadRepository, Asignacion_Componente_ActividadRepository>();
builder.Services.AddScoped<IDimensionRepository, DimensionRepository>();
builder.Services.AddScoped<IPreguntaRepository, PreguntaRepository>();
builder.Services.AddScoped<IOpcion_PreguntaRepository, Opcion_PreguntaRepository>();
builder.Services.AddScoped<IInstruccion_PreguntaRepository, Instruccion_PreguntaRepository>();
builder.Services.AddScoped<IOferta_AcademicaRepository, Oferta_AcademicaRepository>();
builder.Services.AddScoped<IEncuestaRepository, EncuestaRepository>();
builder.Services.AddScoped<IPregunta_EncuestaRepository, Pregunta_EncuestaRepository>();
builder.Services.AddScoped<IOpcion_Pregunta_EncuestaRepository, Opcion_Pregunta_EncuestaRepository>();
builder.Services.AddScoped<IInstruccion_Pregunta_EncuestaRepository, Instruccion_Pregunta_EncuestaRepository>();
builder.Services.AddScoped<ISesion_EncuestaRepository, Sesion_EncuestaRepository>();
builder.Services.AddScoped<IRespuesta_OpcionRepository, Respuesta_OpcionRepository>();
builder.Services.AddScoped<IRespuesta_TextoRepository, Respuesta_TextoRepository>();
builder.Services.AddScoped<IRespuesta_CalificacionRepository, Respuesta_CalificacionRepository>();
builder.Services.AddScoped<IRespuesta_LikertRepository, Respuesta_LikertRepository>();
builder.Services.AddScoped<ILog_AccesoRepository, Log_AccesoRepository>();
builder.Services.AddScoped<ILog_AuditoriaRepository, Log_AuditoriaRepository>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();

// Servicios de negocio
builder.Services.AddScoped<DepartamentoService>();
builder.Services.AddScoped<PeriodoService>();
builder.Services.AddScoped<ComponenteService>();
builder.Services.AddScoped<AsignaturaService>();
builder.Services.AddScoped<CapituloService>();
builder.Services.AddScoped<ActividadService>();
builder.Services.AddScoped<Asignacion_Actividad_CapituloService>();
builder.Services.AddScoped<Asignacion_Componente_ActividadService>();
builder.Services.AddScoped<DimensionService>();
builder.Services.AddScoped<PreguntaService>();
builder.Services.AddScoped<Opcion_PreguntaService>();
builder.Services.AddScoped<Instruccion_PreguntaService>();
builder.Services.AddScoped<Oferta_AcademicaService>();
builder.Services.AddScoped<EncuestaService>();
builder.Services.AddScoped<Pregunta_EncuestaService>();
builder.Services.AddScoped<Opcion_Pregunta_EncuestaService>();
builder.Services.AddScoped<Instruccion_Pregunta_EncuestaService>();
builder.Services.AddScoped<Sesion_EncuestaService>();
builder.Services.AddScoped<Respuesta_OpcionService>();
builder.Services.AddScoped<Respuesta_TextoService>();
builder.Services.AddScoped<Respuesta_CalificacionService>();
builder.Services.AddScoped<Respuesta_LikertService>();
builder.Services.AddScoped<Log_AccesoService>();
builder.Services.AddScoped<Log_AuditoriaService>();
builder.Services.AddScoped<MenuService>();

// Caching en memoria
builder.Services.AddMemoryCache();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Encuestas API", Version = "v1" });
    // Soporte para JWT en Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
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
            new string[] {}
        }
    });
});

var app = builder.Build();

// Redirigir "/" => "/swagger"
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/swagger");
        return;
    }
    await next();
});

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Encuestas API v1");
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
