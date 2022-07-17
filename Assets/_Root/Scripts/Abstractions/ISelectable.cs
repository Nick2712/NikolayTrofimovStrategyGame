using UnityEngine;


namespace NikolayTrofimov_StrategyGame.Abstractions
{
    public interface ISelectable : IHealthHolder, IIconHolder
    {
        Transform PivotPoint { get; }
    }
}