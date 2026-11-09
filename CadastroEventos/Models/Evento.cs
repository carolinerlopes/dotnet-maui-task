using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace CadastroEventos.Models;

/// <summary>
/// Model que representa um evento. Implementa INotifyPropertyChanged para que
/// a interface (via BindingContext) seja atualizada automaticamente sempre que
/// algum dado mudar.
/// </summary>
public class Evento : INotifyPropertyChanged
{
    private static readonly CultureInfo CulturaBr = new("pt-BR");

    private string _nome = string.Empty;
    private DateTime _dataInicio = DateTime.Today;
    private DateTime _dataTermino = DateTime.Today.AddDays(1);
    private int _numeroParticipantes;
    private string _local = string.Empty;
    private decimal _custoPorParticipante;

    // ------------------------------------------------------------------
    // Propriedades informadas pelo usuário
    // ------------------------------------------------------------------

    public string Nome
    {
        get => _nome;
        set => SetProperty(ref _nome, value);
    }

    public DateTime DataInicio
    {
        get => _dataInicio;
        set
        {
            if (SetProperty(ref _dataInicio, value.Date))
            {
                // A data de término nunca pode ser anterior à data de início
                if (DataTermino < _dataInicio)
                    DataTermino = _dataInicio;

                NotificarDuracao();
            }
        }
    }

    public DateTime DataTermino
    {
        get => _dataTermino;
        set
        {
            if (SetProperty(ref _dataTermino, value.Date))
                NotificarDuracao();
        }
    }

    public int NumeroParticipantes
    {
        get => _numeroParticipantes;
        set
        {
            if (SetProperty(ref _numeroParticipantes, value))
                NotificarCusto();
        }
    }

    public string Local
    {
        get => _local;
        set => SetProperty(ref _local, value);
    }

    public decimal CustoPorParticipante
    {
        get => _custoPorParticipante;
        set
        {
            if (SetProperty(ref _custoPorParticipante, value))
                NotificarCusto();
        }
    }

    // ------------------------------------------------------------------
    // Propriedades calculadas (lógica de negócio)
    // ------------------------------------------------------------------

    /// <summary>
    /// Diferença entre as datas, calculada com DateTime e armazenada em um TimeSpan.
    /// </summary>
    public TimeSpan Duracao => DataTermino - DataInicio;

    /// <summary>
    /// Duração do evento em dias, contando o dia de início e o dia de término
    /// (ex.: de 10/11 a 12/11 = 3 dias de evento).
    /// </summary>
    public int DuracaoEmDias => Duracao.Days + 1;

    /// <summary>
    /// Custo total = número de participantes × custo por participante.
    /// </summary>
    public decimal CustoTotal => NumeroParticipantes * CustoPorParticipante;

    // ------------------------------------------------------------------
    // Propriedades formatadas para exibição
    // ------------------------------------------------------------------

    public string DataInicioFormatada => DataInicio.ToString("dddd, dd 'de' MMMM 'de' yyyy", CulturaBr);

    public string DataTerminoFormatada => DataTermino.ToString("dddd, dd 'de' MMMM 'de' yyyy", CulturaBr);

    /// <summary>
    /// Exibe a diferença do TimeSpan (Duracao) formatada.
    /// </summary>
    public string DiferencaTimeSpanFormatada =>
        $"{Duracao.Days} dia(s) entre as datas (TimeSpan: {Duracao.ToString(@"d\.hh\:mm\:ss")})";

    public string DuracaoFormatada =>
        DuracaoEmDias == 1 ? "1 dia de evento" : $"{DuracaoEmDias} dias de evento";

    public string NumeroParticipantesFormatado =>
        NumeroParticipantes == 1 ? "1 participante" : $"{NumeroParticipantes:N0} participantes";

    public string CustoPorParticipanteFormatado => CustoPorParticipante.ToString("C", CulturaBr);

    public string CustoTotalFormatado => CustoTotal.ToString("C", CulturaBr);

    // ------------------------------------------------------------------
    // Validação
    // ------------------------------------------------------------------

    /// <summary>
    /// Valida os dados do evento. Retorna uma lista de mensagens de erro
    /// (vazia quando o cadastro está correto).
    /// </summary>
    public List<string> Validar()
    {
        var erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("Informe o nome do evento.");

        if (DataTermino < DataInicio)
            erros.Add("A data de término não pode ser anterior à data de início.");

        if (NumeroParticipantes <= 0)
            erros.Add("Informe um número de participantes maior que zero.");

        if (string.IsNullOrWhiteSpace(Local))
            erros.Add("Informe o local do evento.");

        if (CustoPorParticipante < 0)
            erros.Add("O custo por participante não pode ser negativo.");

        return erros;
    }

    // ------------------------------------------------------------------
    // INotifyPropertyChanged
    // ------------------------------------------------------------------

    public event PropertyChangedEventHandler? PropertyChanged;

    private void NotificarDuracao()
    {
        OnPropertyChanged(nameof(Duracao));
        OnPropertyChanged(nameof(DuracaoEmDias));
        OnPropertyChanged(nameof(DuracaoFormatada));
        OnPropertyChanged(nameof(DiferencaTimeSpanFormatada));
        OnPropertyChanged(nameof(DataInicioFormatada));
        OnPropertyChanged(nameof(DataTerminoFormatada));
    }

    private void NotificarCusto()
    {
        OnPropertyChanged(nameof(CustoTotal));
        OnPropertyChanged(nameof(CustoTotalFormatado));
        OnPropertyChanged(nameof(CustoPorParticipanteFormatado));
        OnPropertyChanged(nameof(NumeroParticipantesFormatado));
    }

    protected bool SetProperty<T>(ref T campo, T valor, [CallerMemberName] string? nomePropriedade = null)
    {
        if (EqualityComparer<T>.Default.Equals(campo, valor))
            return false;

        campo = valor;
        OnPropertyChanged(nomePropriedade);
        return true;
    }

    protected void OnPropertyChanged([CallerMemberName] string? nomePropriedade = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomePropriedade));
}
