using NikolayTrofimov_StrategyGame.Abstractions;
using NikolayTrofimov_StrategyGame.Core;
using UnityEngine;


namespace NikolayTrofimov_StrategyGame.UserControlSystem.Model
{
    public class SetRallyPointCommandCreator : CancellableCommandCreatorBase<ISetRallyPointCommand, Vector3>
    {
        protected override ISetRallyPointCommand CreateCommand(Vector3 argument) =>
            new SetRallyPointCommand(argument);
    }
}