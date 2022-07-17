using NikolayTrofimov_StrategyGame.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace NikolayTrofimov_StrategyGame.Core
{
    public sealed class UnitCanStop : CommandExecutorBase<IStopCommand>
    {
        public CancellationTokenSource CancellationTokenSource;


        public override async Task ExecuteSpecificCommand(IStopCommand command)
        {
            CancellationTokenSource?.Cancel();
        }
    }
}