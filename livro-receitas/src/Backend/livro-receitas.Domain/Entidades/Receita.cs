using livro_receitas.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace livro_receitas.Domain.Entidades;
public class Receita : EntidadeBase
{
    public string Titulo { get; set; }
    public Categoria Categoria { get; set; }

    public string ModoPreparo { get; set; }

    public ICollection<Ingrediente> Ingredientes { get; set; }

    public long UsuarioId { get; set; }
}
