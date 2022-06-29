using NikolayTrofimov_StrategyGame.Abstractions;
using UnityEngine;


namespace NikolayTrofimov_StrategyGame.Core
{
    public sealed class UnitCanMove : CommandExecutorBase<IMoveCommand>
    {
        public override void ExecuteSpecificCommand(IMoveCommand command)
        {
            Debug.Log("двигаюсь");
        }
    }
}