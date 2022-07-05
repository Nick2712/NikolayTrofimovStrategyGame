using UnityEngine;


namespace NikolayTrofimov_StrategyGame.Abstractions
{
    public interface ISelectable : IHealthHolder
    {
        Transform PivotPoint { get; }
        Sprite Icon { get; }
    }
}