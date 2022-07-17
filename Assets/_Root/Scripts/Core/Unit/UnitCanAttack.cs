using NikolayTrofimov_StrategyGame.Abstractions;
using System.Threading.Tasks;
using UnityEngine;


namespace NikolayTrofimov_StrategyGame.Core
{
    public sealed class UnitCanAttack : CommandExecutorBase<IAttackCommand>
    {
        public override async Task ExecuteSpecificCommand(IAttackCommand command)
        {
            Debug.Log("атакую");
        }
    }
}