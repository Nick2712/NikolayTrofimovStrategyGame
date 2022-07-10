using NikolayTrofimov_StrategyGame.Abstractions;
using System.Threading;


namespace NikolayTrofimov_StrategyGame.Core
{
    public sealed class UnitCanStop : CommandExecutorBase<IStopCommand>
    {
        public CancellationTokenSource CancellationTokenSource;


        public override void ExecuteSpecificCommand(IStopCommand command)
        {
            CancellationTokenSource?.Cancel();
        }
    }
}