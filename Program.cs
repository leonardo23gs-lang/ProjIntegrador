using EinsteinGestaoAcademica.API.Dados;
using EinsteinGestaoAcademica.API.Dados.Repositorio;
using EinsteinGestaoAcademica.API.Repositorio;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Adiciona suporte aos Controllers
builder.Services.AddControllers();

var configuracao = builder.Configuration;

// 2. Configura o Swagger/OpenAPI
// Saiba mais em https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Einstein Gest�o Acad�mica WebAPI - .NET CORE 8",
        Version = "v1",
        Description = "API para gest�o acad�mica do sistema Einstein"
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseNpgsql(configuracao.GetValue<string>("Settings:CONNECTION_STRING"), o => o.UseRelationalNulls()));
builder.Services.AddTransient<ICursoRepositorio,CursoRepositorio>();
builder.Services.AddTransient<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddTransient<IDisciplinaRepositorio, DisciplinaRepositorio>();

var app = builder.Build();

// 3. Configura o pipeline de requisi��o HTTP
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    // Define o endpoint do JSON e o nome que aparece no topo da p�gina
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Einstein API v1");

    // Opcional: Define o Swagger como a p�gina inicial (ao acessar raiz /)
    // options.RoutePrefix = string.Empty; 
});

app.UseAuthorization();

app.MapControllers();

app.Run();