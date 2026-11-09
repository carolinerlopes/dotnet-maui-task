using CadastroEventos.Models;

namespace CadastroEventos.Views;

public partial class CadastroEventoPage : ContentPage
{
    private Evento _evento = new();

    public CadastroEventoPage()
    {
        InitializeComponent();

        // Associa a model Evento à tela: todos os {Binding} do XAML
        // passam a ler/escrever as propriedades deste objeto.
        BindingContext = _evento;
    }

    private async void OnConcluirClicked(object? sender, EventArgs e)
    {
        var erros = _evento.Validar();

        if (erros.Count > 0)
        {
            await DisplayAlertAsync("Dados inválidos", string.Join(Environment.NewLine, erros), "OK");
            return;
        }

        // Abre a página de resumo enviando o evento cadastrado
        await Navigation.PushAsync(new ResumoEventoPage(_evento));
    }

    private void OnLimparClicked(object? sender, EventArgs e)
    {
        EntryNome.Text = string.Empty;
        EntryParticipantes.Text = string.Empty;
        EntryLocal.Text = string.Empty;
        EntryCusto.Text = string.Empty;

        // Cria um novo evento e o associa novamente à tela
        _evento = new Evento();
        BindingContext = _evento;
    }
}
