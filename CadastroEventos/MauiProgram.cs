using System.Globalization;
using Microsoft.Extensions.Logging;

namespace CadastroEventos;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        // Define a cultura pt-BR para formatar datas (dd/MM/yyyy) e moeda (R$)
        var culturaBr = new CultureInfo("pt-BR");
        CultureInfo.DefaultThreadCurrentCulture = culturaBr;
        CultureInfo.DefaultThreadCurrentUICulture = culturaBr;
        CultureInfo.CurrentCulture = culturaBr;
        CultureInfo.CurrentUICulture = culturaBr;

        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
