using NikolayTrofimov_StrategyGame.Abstractions;
using UnityEngine;


namespace NikolayTrofimov_StrategyGame.UserControlSystem.Model
{
    [CreateAssetMenu(fileName = nameof(AttackableValue), menuName = "StrategyGame/" + nameof(AttackableValue))]
    public class AttackableValue : ScriptableObjectValueBase<IAttackable>
    {
        
    }
}