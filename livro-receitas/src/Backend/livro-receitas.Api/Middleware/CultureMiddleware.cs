using System.Globalization;

namespace livro_receitas.Api.Middleware;

public class CultureMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IList<string> _idiomasSuportados = new List<string>
    {
        "pt",
        "en"
    };
    public CultureMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context)
    {
        var culture = new CultureInfo("pt");

        if (context.Request.Headers.ContainsKey("Accept-Language"))
        {
            var idioma = context.Request.Headers["Accept-Language"].ToString();

            if (_idiomasSuportados.Any(x => x.Equals(idioma)))
            {
                culture = new CultureInfo(idioma);
            }
        }

        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;

        await _next(context);
    }
}