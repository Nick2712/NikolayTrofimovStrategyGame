using NikolayTrofimov_StrategyGame.Abstractions;
using UnityEngine;


namespace NikolayTrofimov_StrategyGame.UserControlSystem.Model
{
    [CreateAssetMenu(fileName = nameof(SelectableValue), menuName = "StrategyGame/" + nameof(SelectableValue))]
    public sealed class SelectableValue : ScriptableObjectValueBase<ISelectable>
    {
        
    }
}