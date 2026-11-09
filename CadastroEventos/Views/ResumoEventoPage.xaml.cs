using CadastroEventos.Models;

namespace CadastroEventos.Views;

public partial class ResumoEventoPage : ContentPage
{
    public ResumoEventoPage(Evento evento)
    {
        InitializeComponent();

        // O mesmo objeto Evento preenchido no cadastro é usado como BindingContext
        BindingContext = evento;
    }

    private async void OnVoltarClicked(object? sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
