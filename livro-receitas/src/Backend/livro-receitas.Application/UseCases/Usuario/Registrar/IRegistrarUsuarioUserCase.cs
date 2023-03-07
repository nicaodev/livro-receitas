using livro_receitas.Comunicacao.Request;
using livro_receitas.Comunicacao.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace livro_receitas.Application.UseCases.Usuario.Registrar
{
    public interface IRegistrarUsuarioUserCase
    {
        Task<ResponseUsuarioRegistradoJson> Executar(RequestRegistrarUsuarioJson request);
    }
}
