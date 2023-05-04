using AutoMapper;
using HashidsNet;
using livro_receitas.Comunicacao.Request;
using livro_receitas.Comunicacao.Response;
using livro_receitas.Domain.Entidades;

namespace livro_receitas.Application.Services;

public class AutoMapperConfiguration : Profile
{
    private readonly IHashids _hashids;
    public AutoMapperConfiguration(IHashids hashids)
    {
        _hashids = hashids;


        RequisicaoParaEntidade();
        EntidadeParaResposta();
    }

    private void RequisicaoParaEntidade()
    {
        CreateMap<RequestRegistrarUsuarioJson, Usuario>().ForMember(destino => destino.Senha, config => config.Ignore()); //Fazendo criptografia na regra de negocio

        CreateMap<RequestReceitaJson, Receita>();
        CreateMap<RequestRegistrarIngredienteJson, Ingrediente>();
    }

    private void EntidadeParaResposta()
    {
        CreateMap<Receita, ResponseReceitaJson>().ForMember(destino => destino.Id, config => config.MapFrom(origem => _hashids.EncodeLong(origem.Id)));
        CreateMap<Ingrediente, ResponseIngredientesJson>().ForMember(destino => destino.Id, config => config.MapFrom(origem => _hashids.EncodeLong(origem.Id)));

        CreateMap<Receita, ResponseReceitasDashboardJson>().ForMember(destino => destino.Id, config => config.MapFrom(origem => _hashids.EncodeLong(origem.Id)))
            .ForMember(destino => destino.QuantidadeIngredientes, config => config.MapFrom(origem => origem.Ingredientes.Count()));

        CreateMap<Usuario, ResponsePerfilUsuarioJson>();

    }
}