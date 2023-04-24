namespace livro_receitas.Domain.Repositories.Receita;

public interface IUpdateOnlyRepository
{
    Task<Entidades.Receita> RecuperarPorId(long idUsuario);
    void Update(Entidades.Receita receita);
}