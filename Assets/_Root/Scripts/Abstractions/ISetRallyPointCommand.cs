using UnityEngine;


namespace NikolayTrofimov_StrategyGame.Abstractions
{
    public interface ISetRallyPointCommand : ICommand
    {
        Vector3 RallyPoint { get; }
    }
}