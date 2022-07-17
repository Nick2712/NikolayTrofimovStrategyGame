using NikolayTrofimov_StrategyGame.Abstractions;
using UnityEngine;


namespace NikolayTrofimov_StrategyGame.Core
{
    public class SetRallyPointCommand : ISetRallyPointCommand
    {
        public Vector3 RallyPoint { get; }


        public SetRallyPointCommand(Vector3 rallyPoint)
        {
            RallyPoint = rallyPoint;
        }
    }
}