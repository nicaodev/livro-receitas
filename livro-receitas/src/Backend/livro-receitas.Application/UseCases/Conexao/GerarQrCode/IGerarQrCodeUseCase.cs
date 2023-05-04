namespace livro_receitas.Application.UseCases.Conexao.GerarQrCode;

public interface IGerarQrCodeUseCase
{
    Task<string> Executar();
}