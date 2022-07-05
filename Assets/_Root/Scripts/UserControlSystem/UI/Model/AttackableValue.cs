using NikolayTrofimov_StrategyGame.Abstractions;
using System;
using UnityEngine;


namespace NikolayTrofimov_StrategyGame.UserControlSystem.Model
{
    [CreateAssetMenu(fileName = nameof(AttackableValue), menuName = "StrategyGame/" + nameof(AttackableValue))]
    public class AttackableValue : ScriptableObject
    {
        public IAttackable CurrentValue { get; private set; }
        public Action<IAttackable> OnNewValue;


        public void SetValue(IAttackable value)
        {
            CurrentValue = value;
            OnNewValue?.Invoke(value);
        }
    }
}