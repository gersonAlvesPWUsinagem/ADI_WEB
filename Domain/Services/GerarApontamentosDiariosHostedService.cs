using Domain.Helpers;
using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Domain.Services
{
    public class GerarApontamentosDiariosHostedService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<GerarApontamentosDiariosHostedService> _logger;
        private readonly AtualizacaoPortariaNotifier _notifier;

        public GerarApontamentosDiariosHostedService(
            IServiceProvider serviceProvider,
            ILogger<GerarApontamentosDiariosHostedService> logger,
            AtualizacaoPortariaNotifier notifier)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _notifier = notifier;
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "Serviço de geração diária de apontamentos iniciado.");

            while (!stoppingToken.IsCancellationRequested)
            {
                var agora = DateTime.Now;

                // Próxima execução às 01:00
                var proximaExecucao = agora.Date.AddDays(1).AddMinutes(10);

                var tempoAteExecucao =
                    proximaExecucao - agora;

                _logger.LogInformation(
                    "Próxima execução agendada para {Horario}",
                    proximaExecucao);

                try
                {
                    await Task.Delay(
                        tempoAteExecucao,
                        stoppingToken);
                }
                catch (TaskCanceledException)
                {
                    break;
                }

                if (stoppingToken.IsCancellationRequested)
                    break;

                await ExecutarRotinaAsync();
            }
        }

        private async Task ExecutarRotinaAsync()
        {
            try
            {
                _logger.LogInformation(
                    "==============================================");

                _logger.LogInformation(
                    "INICIANDO GERAÇÃO AUTOMÁTICA DE APONTAMENTOS");

                _logger.LogInformation(
                    "Data/Hora: {DataHora}",
                    DateTime.Now);

                using var scope =
                    _serviceProvider.CreateScope();

                var portariaService =
                    scope.ServiceProvider
                        .GetRequiredService<IPortariaService>();

                var dataSessionHelper =
                    scope.ServiceProvider
                        .GetRequiredService<IDataSessionHelper>();

                var user =
                    dataSessionHelper.DataSession;

                await portariaService
                    .PostGerarListaDeApontamentAsync(user);

                _logger.LogInformation(
                    "GERAÇÃO DE APONTAMENTOS CONCLUÍDA COM SUCESSO");

                // Avisa somente as páginas ControlePessoaPage
                // que estiverem abertas.
                await _notifier.NotificarAtualizacaoAsync();

                _logger.LogInformation(
                    "NOTIFICAÇÃO DAS PÁGINAS ENVIADA");

                _logger.LogInformation(
                    "==============================================");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "ERRO AO EXECUTAR GERAÇÃO AUTOMÁTICA DE APONTAMENTOS");
            }
        }
    }
}