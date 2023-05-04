using AutoMapper;
using HashidsNet;
using livro_receitas.Api.Filter;
using livro_receitas.Api.Filter.Swagger;
using livro_receitas.Api.Filter.UsuarioLogado;
using livro_receitas.Api.Middleware;
using livro_receitas.Api.WebSockets;
using livro_receitas.Application;
using livro_receitas.Application.Services;
using livro_receitas.Domain.Extensions;
using livro_receitas.Infrastructure;
using livro_receitas.Infrastructure.AcessoRepository;
using livro_receitas.Infrastructure.Migrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRouting(opt => opt.LowercaseUrls = true);

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.OperationFilter<HashidsOperationFilter>();
});
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddInfraestructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

builder.Services.AddMvc(opt => opt.Filters.Add(typeof(FilterExceptions))); // Qualquer exception lançada será capturada pela classe FilterExceptions

builder.Services.AddScoped(provider => new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperConfiguration(provider.GetService<IHashids>()));
}).CreateMapper());

builder.Services.AddScoped<IAuthorizationHandler, UsuarioLogadoHandler>();
builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("PoliticaUsuarioLogado", policy => policy.Requirements.Add(new UsuarioLogadoRequirement()));
});
builder.Services.AddScoped<UsuarioAutenticadoAtribute>();

builder.Services.AddSignalR();

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

AtualizarBD();

app.UseMiddleware<CultureMiddleware>();
app.MapHub<AddConexao>("/addConexao");

app.Run();

void AtualizarBD()
{
    using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

    using var context = serviceScope.ServiceProvider.GetService<LivroReceitasContext>();

    bool? databaseInMemory = context?.Database?.ProviderName?.Equals("Microsoft.EntityFrameworkCore.InMemory");

    if (!databaseInMemory.HasValue || !databaseInMemory.Value)
    {
        var DefaultNameDatabase = builder.Configuration.GetDefaultNameDatabase();
        var DefaultConnection = builder.Configuration.GetDefaultConnection();

        Database.CriarDatabase(DefaultConnection, DefaultNameDatabase);

        app.MigrateBancoDeDados();
    }
}

// Desabilitando erros que podem surgir no sonar.
#pragma warning disable CA1050, S3903, S1118
public partial class Program
{ }

#pragma warning restore CA1050, S3903, S1118