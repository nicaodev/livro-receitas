﻿using System.Globalization;
using System.Text;

namespace livro_receitas.Domain.Extensions;

public static class StringExtension
{
    public static bool CompararSemconsiderarAcentoECaseSensitive(this string origem, string pesquisarPor)
    {
        var index = CultureInfo.CurrentCulture.CompareInfo.IndexOf(origem, pesquisarPor, CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace);

        return index >= 0;
    }

    public static string RemoverAcentos(this string texto)
    {
        return new string(texto.Normalize(NormalizationForm.FormD).Where(ch => char.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark).ToArray());
    }
}