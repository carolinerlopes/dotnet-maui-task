using System.Globalization;

namespace CadastroEventos.Converters;

/// <summary>
/// Converte o texto digitado em um Entry para número (int ou decimal),
/// aceitando vírgula ou ponto como separador decimal (padrão brasileiro).
/// Usado com Mode=OneWayToSource: o texto do Entry alimenta a propriedade da model.
/// </summary>
public class TextoParaNumeroConverter : IValueConverter
{
    private static readonly CultureInfo CulturaBr = new("pt-BR");

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value?.ToString();

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var texto = (value as string)?.Trim() ?? string.Empty;

        // Remove o símbolo de moeda, caso digitado, e normaliza o separador decimal
        texto = texto.Replace("R$", string.Empty).Replace(" ", string.Empty);
        if (texto.Contains('.') && !texto.Contains(','))
            texto = texto.Replace('.', ',');

        if (targetType == typeof(int))
            return int.TryParse(texto, NumberStyles.Integer, CulturaBr, out var inteiro) ? inteiro : 0;

        return decimal.TryParse(texto, NumberStyles.Number, CulturaBr, out var numero) ? numero : 0m;
    }
}
