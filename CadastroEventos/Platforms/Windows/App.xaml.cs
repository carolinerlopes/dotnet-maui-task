// Para saber mais sobre WinUI, a estrutura de projeto WinUI
// e os templates de projeto, veja: http://aka.ms/winui-project-info.

namespace CadastroEventos.WinUI;

/// <summary>
/// Fornece o comportamento específico do aplicativo para complementar a classe Application padrão.
/// </summary>
public partial class App : MauiWinUIApplication
{
    /// <summary>
    /// Inicializa o objeto singleton do aplicativo.
    /// </summary>
    public App()
    {
        this.InitializeComponent();
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
