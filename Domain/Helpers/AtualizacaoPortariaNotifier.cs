namespace Domain.Helpers
{
    public class AtualizacaoPortariaNotifier
    {
        public event Func<Task>? AtualizacaoSolicitada;

        public async Task NotificarAtualizacaoAsync()
        {
            var handlers = AtualizacaoSolicitada?
                .GetInvocationList()
                .Cast<Func<Task>>()
                .ToList();

            if (handlers is null || handlers.Count == 0)
                return;

            foreach (var handler in handlers)
            {
                try
                {
                    await handler();
                }
                catch
                {
                    // Página pode ter sido fechada/desconectada.
                }
            }
        }
    }
}