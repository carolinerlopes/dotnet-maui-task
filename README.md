# 📅 Cadastro de Eventos — .NET MAUI

Aplicativo .NET MAUI (.NET 10) com uma tela de **Cadastro de Eventos** e uma página de **Resumo do Evento**.

## Funcionalidades

- Cadastro dos dados básicos do evento:
  - Nome do evento
  - Data de início e data de término
  - Número de participantes
  - Local do evento
  - Custo por participante
- **Prévia em tempo real** da duração e do custo total enquanto o usuário digita.
- Validação dos dados antes de concluir o cadastro.
- Página de **resumo** com todas as informações formatadas em pt-BR (datas por extenso e valores em R$) e o **custo total calculado**.

## Como os requisitos foram atendidos

| Requisito | Onde |
|---|---|
| `BindingContext` associando a tela à model `Evento` | [`CadastroEventoPage.xaml.cs`](CadastroEventos/Views/CadastroEventoPage.xaml.cs) → `BindingContext = _evento;` e [`ResumoEventoPage.xaml.cs`](CadastroEventos/Views/ResumoEventoPage.xaml.cs) |
| Model com as propriedades do evento | [`Models/Evento.cs`](CadastroEventos/Models/Evento.cs) |
| Cálculo da duração em dias (`DateTime`) | `Duracao => DataTermino - DataInicio` (subtração de dois `DateTime`) e `DuracaoEmDias => Duracao.Days + 1` |
| Exibição da diferença com `TimeSpan` | `Duracao` é um `TimeSpan`; `DiferencaTimeSpanFormatada` usa `Duracao.Days` e `Duracao.ToString(...)` |
| Custo total | `CustoTotal => NumeroParticipantes * CustoPorParticipante` |
| Resumo em nova página | `Navigation.PushAsync(new ResumoEventoPage(_evento))` |

> **Regra da duração:** a duração conta o dia de início e o de término. Ex.: de 10/11 a 12/11 → o `TimeSpan` tem 2 dias de diferença e o evento tem **3 dias**.

## Estrutura

```
CadastroEventos.sln
CadastroEventos/
├── Models/Evento.cs                      # Model + lógica de cálculo (INotifyPropertyChanged)
├── Converters/TextoParaNumeroConverter.cs# Converte texto digitado em int/decimal (aceita vírgula)
├── Views/CadastroEventoPage.xaml(.cs)    # Tela de cadastro
├── Views/ResumoEventoPage.xaml(.cs)      # Tela de resumo
├── App.xaml / AppShell.xaml              # Aplicação e navegação (Shell)
├── MauiProgram.cs                        # Configuração (cultura pt-BR)
├── Resources/                            # Estilos, cores, ícone e splash
└── Platforms/                            # Android, iOS, MacCatalyst, Windows
```

## Como executar

Pré-requisitos: **.NET 10 SDK** + workload MAUI (`dotnet workload install maui`) ou Visual Studio 2022/2026 com a carga de trabalho *.NET Multi-platform App UI*.

```bash
# Windows
dotnet build CadastroEventos/CadastroEventos.csproj -t:Run -f net10.0-windows10.0.19041.0

# Android (emulador aberto)
dotnet build CadastroEventos/CadastroEventos.csproj -t:Run -f net10.0-android
```

Ou abra `CadastroEventos.sln` no Visual Studio, escolha **Windows Machine** ou um emulador Android e pressione **F5**.
