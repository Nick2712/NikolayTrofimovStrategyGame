using NikolayTrofimov_StrategyGame.Abstractions;
using System;


namespace NikolayTrofimov_StrategyGame.UserControlSystem.Model
{
    public sealed class PatrolCommandCommandCreator : CommandCreatorBase<IPatrolCommand>
    {
        protected override void ClassSpecificCommandCreation(Action<IPatrolCommand> creationCallback)
        {
            
        }
    }
}