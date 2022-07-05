using NikolayTrofimov_StrategyGame.Abstractions;
using System;


namespace NikolayTrofimov_StrategyGame.UserControlSystem.Model
{
    public sealed class StopCommandCommandCreator : CommandCreatorBase<IStopCommand>
    {
        protected override void ClassSpecificCommandCreation(Action<IStopCommand> creationCallback)
        {
            
        }
    }
}