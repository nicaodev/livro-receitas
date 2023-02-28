using livro_receitas.Domain.Extensions;
using livro_receitas.Infrastructure;
using livro_receitas.Infrastructure.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRepositorio(builder.Configuration);

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