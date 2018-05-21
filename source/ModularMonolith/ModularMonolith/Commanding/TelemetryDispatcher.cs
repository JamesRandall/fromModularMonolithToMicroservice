using System.Threading;
using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using AzureFromTheTrenches.Commanding.Abstractions.Model;
using Microsoft.Extensions.Logging;

namespace ModularMonolith.Commanding
{
    public class TelemetryDispatcher : ICommandDispatcher
    {
        private readonly IFrameworkCommandDispatcher _decoratedDispatcher;
        private readonly ILogger<TelemetryDispatcher> _logger;

        public TelemetryDispatcher(IFrameworkCommandDispatcher decoratedDispatcher,
            ILogger<TelemetryDispatcher> logger)
        {
            _decoratedDispatcher = decoratedDispatcher;
            _logger = logger;
        }

        public async Task<CommandResult<TResult>> DispatchAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = new CancellationToken())
        {
            _logger.LogInformation("Dispatching command {commandType}", command.GetType().Name);
            return await _decoratedDispatcher.DispatchAsync(command, cancellationToken);
        }

        public async Task<CommandResult> DispatchAsync(ICommand command, CancellationToken cancellationToken = new CancellationToken())
        {
            _logger.LogInformation("Dispatching command {commandType}", command.GetType().Name);
            return await _decoratedDispatcher.DispatchAsync(command, cancellationToken);
        }

        public ICommandExecuter AssociatedExecuter => _decoratedDispatcher.AssociatedExecuter;
    }
}
