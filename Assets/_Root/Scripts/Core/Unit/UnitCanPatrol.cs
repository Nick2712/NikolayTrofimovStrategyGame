using NikolayTrofimov_StrategyGame.Abstractions;
using System.Threading.Tasks;
using UnityEngine;


namespace NikolayTrofimov_StrategyGame.Core
{
    public sealed class UnitCanPatrol : CommandExecutorBase<IPatrolCommand>
    {
        public override async Task ExecuteSpecificCommand(IPatrolCommand command)
        {
            Debug.Log("патрулирую");
        }
    }
}