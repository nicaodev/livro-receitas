using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace livro_receitas.Domain.Repositories
{
    public interface IUnityOfWork
    {
        Task Commit();
    }
}
