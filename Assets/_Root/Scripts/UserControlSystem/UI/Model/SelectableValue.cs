using NikolayTrofimov_StrategyGame.Abstractions;
using System;
using UnityEngine;


namespace NikolayTrofimov_StrategyGame.UserControlSystem.Model
{
    [CreateAssetMenu(fileName = nameof(SelectableValue), menuName = "StrategyGame/" + nameof(SelectableValue))]
    public sealed class SelectableValue : ScriptableObject
    {
        public ISelectable CurrentValue { get; private set; }
        public Action<ISelectable> OnSelected;


        public void SetValue(ISelectable value)
        {
            CurrentValue = value;
            OnSelected?.Invoke(value);
        }
    }
}