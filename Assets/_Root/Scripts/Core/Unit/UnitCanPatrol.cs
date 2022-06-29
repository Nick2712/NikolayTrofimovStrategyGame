using NikolayTrofimov_StrategyGame.Abstractions;
using UnityEngine;


namespace NikolayTrofimov_StrategyGame.Core
{
    public sealed class UnitCanPatrol : CommandExecutorBase<IPatrolCommand>
    {
        public override void ExecuteSpecificCommand(IPatrolCommand command)
        {
            Debug.Log("патрулирую");
        }
    }
}