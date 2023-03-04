using AutoMapper;
using livro_receitas.Comunicacao.Request;
using livro_receitas.Domain.Entidades;

namespace livro_receitas.Application.Services;

public class AutoMapperConfiguration : Profile
{
    public AutoMapperConfiguration()
    {
        CreateMap<RequestRegistrarUsuarioJson, Usuario>().ForMember(destino => destino.Senha, config => config.Ignore()); //Fazendo criptografia na regra de negocio
    }
}