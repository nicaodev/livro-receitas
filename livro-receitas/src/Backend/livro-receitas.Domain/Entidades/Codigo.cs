using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace livro_receitas.Domain.Entidades;
public class Codigos : EntidadeBase
{
    public string Codigo { get; set; }
    public long UsuarioId { get; set; }
}
