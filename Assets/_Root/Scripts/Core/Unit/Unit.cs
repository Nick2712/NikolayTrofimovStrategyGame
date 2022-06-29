using NikolayTrofimov_StrategyGame.Abstractions;
using UnityEngine;


namespace NikolayTrofimov_StrategyGame.Core
{
    public sealed class Unit : MonoBehaviour, ISelectable
    {
        [SerializeField] private int _maxHealth = 100;
        [SerializeField] private Sprite _icon;

        private float _health = 100;

        public float Health => _health;
        public float MaxHeath => _maxHealth;
        public Sprite Icon => _icon;
    }
}