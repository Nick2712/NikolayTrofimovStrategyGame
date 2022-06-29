using UnityEngine;


namespace NikolayTrofimov_StrategyGame.Abstractions
{
    public interface IProduceUnitCommand : ICommand
    {
        GameObject UnitPrefab { get; }
    }
}