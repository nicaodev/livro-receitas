using livro_receitas.Application.UseCases.Usuario.Registrar;
using Microsoft.Extensions.DependencyInjection;

namespace livro_receitas.Application;
    public static class Bootstrapper
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IRegistrarUsuarioUserCase, RegistrarUsuarioUserCase>();
        }
    }
