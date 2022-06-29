using NikolayTrofimov_StrategyGame.Abstractions;
using UnityEngine;


namespace NikolayTrofimov_StrategyGame.Core
{
    public sealed class UnitCanStop : CommandExecutorBase<IStopCommand>
    {
        public override void ExecuteSpecificCommand(IStopCommand command)
        {
            Debug.Log("стою");
        }
    }
}