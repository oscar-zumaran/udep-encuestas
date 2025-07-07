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
builder.Services.AddScoped<IGenericRepository<Componente>>(sp =>
    new GenericRepository<Componente>(sp.GetRequiredService<IDbConnection>(), "Componente", "iIdComponente"));
builder.Services.AddScoped<IGenericRepository<Asignatura>>(sp =>
    new GenericRepository<Asignatura>(sp.GetRequiredService<IDbConnection>(), "Asignatura", "iIdAsignatura"));
builder.Services.AddScoped<IGenericRepository<Capitulo>>(sp =>
    new GenericRepository<Capitulo>(sp.GetRequiredService<IDbConnection>(), "Capitulo", "iIdCapitulo"));
builder.Services.AddScoped<IGenericRepository<Actividad>>(sp =>
    new GenericRepository<Actividad>(sp.GetRequiredService<IDbConnection>(), "Actividad", "iIdActividad"));
builder.Services.AddScoped<IGenericRepository<Asignacion_Actividad_Capitulo>>(sp =>
    new GenericRepository<Asignacion_Actividad_Capitulo>(sp.GetRequiredService<IDbConnection>(), "Asignacion_Actividad_Capitulo", "iIdAsignacion"));
builder.Services.AddScoped<IGenericRepository<Asignacion_Componente_Actividad>>(sp =>
    new GenericRepository<Asignacion_Componente_Actividad>(sp.GetRequiredService<IDbConnection>(), "Asignacion_Componente_Actividad", "iIdAsignacion"));
builder.Services.AddScoped<IGenericRepository<Dimension>>(sp =>
    new GenericRepository<Dimension>(sp.GetRequiredService<IDbConnection>(), "Dimension", "iIdDimension"));
builder.Services.AddScoped<IGenericRepository<Pregunta>>(sp =>
    new GenericRepository<Pregunta>(sp.GetRequiredService<IDbConnection>(), "Pregunta", "iIdPregunta"));
builder.Services.AddScoped<IGenericRepository<Opcion_Pregunta>>(sp =>
    new GenericRepository<Opcion_Pregunta>(sp.GetRequiredService<IDbConnection>(), "Opcion_Pregunta", "iIdOpcion"));
builder.Services.AddScoped<IGenericRepository<Instruccion_Pregunta>>(sp =>
    new GenericRepository<Instruccion_Pregunta>(sp.GetRequiredService<IDbConnection>(), "Instruccion_Pregunta", "iIdInstruccion"));
builder.Services.AddScoped<IGenericRepository<Oferta_Academica>>(sp =>
    new GenericRepository<Oferta_Academica>(sp.GetRequiredService<IDbConnection>(), "Oferta_Academica", "iIdOfertaAcademica"));
builder.Services.AddScoped<IGenericRepository<Encuesta>>(sp =>
    new GenericRepository<Encuesta>(sp.GetRequiredService<IDbConnection>(), "Encuesta", "iIdEncuesta"));
builder.Services.AddScoped<IGenericRepository<Pregunta_Encuesta>>(sp =>
    new GenericRepository<Pregunta_Encuesta>(sp.GetRequiredService<IDbConnection>(), "Pregunta_Encuesta", "iIdPreguntaEncuesta"));
builder.Services.AddScoped<IGenericRepository<Opcion_Pregunta_Encuesta>>(sp =>
    new GenericRepository<Opcion_Pregunta_Encuesta>(sp.GetRequiredService<IDbConnection>(), "Opcion_Pregunta_Encuesta", "iIdOpcionPreguntaEncuesta"));
builder.Services.AddScoped<IGenericRepository<Instruccion_Pregunta_Encuesta>>(sp =>
    new GenericRepository<Instruccion_Pregunta_Encuesta>(sp.GetRequiredService<IDbConnection>(), "Instruccion_Pregunta_Encuesta", "iIdInstruccionPreguntaEncuesta"));
builder.Services.AddScoped<IGenericRepository<Sesion_Encuesta>>(sp =>
    new GenericRepository<Sesion_Encuesta>(sp.GetRequiredService<IDbConnection>(), "Sesion_Encuesta", "iIdSesion"));
builder.Services.AddScoped<IGenericRepository<Respuesta_Opcion>>(sp =>
    new GenericRepository<Respuesta_Opcion>(sp.GetRequiredService<IDbConnection>(), "Respuesta_Opcion", "iIdRespuesta"));
builder.Services.AddScoped<IGenericRepository<Respuesta_Texto>>(sp =>
    new GenericRepository<Respuesta_Texto>(sp.GetRequiredService<IDbConnection>(), "Respuesta_Texto", "iIdRespuesta"));
builder.Services.AddScoped<IGenericRepository<Respuesta_Calificacion>>(sp =>
    new GenericRepository<Respuesta_Calificacion>(sp.GetRequiredService<IDbConnection>(), "Respuesta_Calificacion", "iIdRespuesta"));
builder.Services.AddScoped<IGenericRepository<Respuesta_Likert>>(sp =>
    new GenericRepository<Respuesta_Likert>(sp.GetRequiredService<IDbConnection>(), "Respuesta_Likert", "iIdRespuesta"));
builder.Services.AddScoped<IGenericRepository<Log_Acceso>>(sp =>
    new GenericRepository<Log_Acceso>(sp.GetRequiredService<IDbConnection>(), "Log_Acceso", "iIdLogAcceso"));
builder.Services.AddScoped<IGenericRepository<Log_Auditoria>>(sp =>
    new GenericRepository<Log_Auditoria>(sp.GetRequiredService<IDbConnection>(), "Log_Auditoria", "iIdLogAuditoria"));
builder.Services.AddScoped<IGenericRepository<Rol>>(sp =>
    new GenericRepository<Rol>(sp.GetRequiredService<IDbConnection>(), "Rol", "iIdRol"));
builder.Services.AddScoped<IGenericRepository<Usuario>>(sp =>
    new GenericRepository<Usuario>(sp.GetRequiredService<IDbConnection>(), "Usuario", "iIdUsuario"));
builder.Services.AddScoped<IGenericRepository<Usuario_Rol>>(sp =>
    new GenericRepository<Usuario_Rol>(sp.GetRequiredService<IDbConnection>(), "Usuario_Rol", "iIdUsuarioRol"));
builder.Services.AddScoped<IGenericRepository<Rol_Menu>>(sp =>
    new GenericRepository<Rol_Menu>(sp.GetRequiredService<IDbConnection>(), "Rol_Menu", "iIdRolMenu"));
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
