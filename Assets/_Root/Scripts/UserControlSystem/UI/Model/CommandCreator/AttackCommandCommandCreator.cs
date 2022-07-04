using NikolayTrofimov_StrategyGame.Abstractions;
using System;


namespace NikolayTrofimov_StrategyGame.UserControlSystem.Model
{
    public sealed class AttackCommandCommandCreator : CommandCreatorBase<IAttackCommand>
    {
        protected override void ClassSpecificCommandCreation(Action<IAttackCommand> creationCallback)
        {
            
        }
    }
}