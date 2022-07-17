using UnityEngine;


namespace NikolayTrofimov_StrategyGame.Abstractions
{
    public interface IProduceUnitCommand : ICommand, IIconHolder
    {
        float ProductionTime { get; }
        GameObject UnitPrefab { get; }
        string UnitName { get; }
    }
}