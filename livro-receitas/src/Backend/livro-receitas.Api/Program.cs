using AutoMapper;
using livro_receitas.Api.Filter;
using livro_receitas.Application.Services;
using livro_receitas.Domain.Extensions;
using livro_receitas.Infrastructure;
using livro_receitas.Infrastructure.Migrations;
using livro_receitas.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRepositorio(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

builder.Services.AddMvc(opt => opt.Filters.Add(typeof(FilterExceptions))); // Qualquer exception lançada será capturada pela classe FilterExceptions

builder.Services.AddScoped(provider => new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperConfiguration());
}).CreateMapper());

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

app.Run();

void AtualizarBD()
{
    var DefaultNameDatabase = builder.Configuration.GetDefaultNameDatabase();
    var DefaultConnection = builder.Configuration.GetDefaultConnection();

    Database.CriarDatabase(DefaultConnection, DefaultNameDatabase);

    app.MigrateBancoDeDados();
}