using NikolayTrofimov_StrategyGame.Abstractions;
using UnityEngine;


namespace NikolayTrofimov_StrategyGame.Core
{
    public sealed class MainBuilding : MonoBehaviour, ISelectable, IAttackable
    {
        [SerializeField] private float _maxHealth = 1000;
        [SerializeField] private Sprite _icon;

        private float _health = 1000;

        public float Health => _health;
        public float MaxHealth => _maxHealth;
        public Sprite Icon => _icon;
        public Transform PivotPoint => transform;

        public Vector3 RallyPoint { get; set; }


        private void Start()
        {
            RallyPoint = transform.position.x > 0 ? 
                transform.position + new Vector3(-3, 0, 0) : 
                transform.position + new Vector3(3, 0, 0);
        }

        public void RecieveDamage(int damage)
        {
            if (_health <= 0) return;
            _health -= damage;
            if (_health < -0) Destroy(gameObject);
        }
    }
}