namespace livro_receitas.Application.UseCases.Receita.Deletar;

public interface IDeletarReceitaUseCase
{
    Task Executar(long id);
}