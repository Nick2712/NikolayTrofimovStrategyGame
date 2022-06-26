using UnityEngine;


namespace NikolayTrofimov_StrategyGame.Abstractions
{
    public interface ISelectable
    {
        float Health { get; }
        float MaxHeath { get; }
        Sprite Icon { get; }
    }
}