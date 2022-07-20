using NikolayTrofimov_StrategyGame.Abstractions;
using System.Threading.Tasks;
using UnityEngine;


namespace NikolayTrofimov_StrategyGame.Core
{
    public sealed class Unit : MonoBehaviour, ISelectable, IAttackable, IUnit, IDamageDealer, IAutomaticAttacker
    {
        [SerializeField] private int _maxHealth = 100;
        [SerializeField] private Sprite _icon;

        [SerializeField] private Animator _animator;
        [SerializeField] private UnitCanStop _stopCommand;

        [SerializeField] private int _damage = 2;

        [SerializeField] private float _visionRadius = 8;

        private float _health;

        public float Health => _health;
        public float MaxHealth => _maxHealth;
        public Sprite Icon => _icon;
        public Transform PivotPoint => transform;

        public int Damage => _damage;

        public float VisionRadius => _visionRadius;

        private void Start()
        {
            _health = _maxHealth;
        }

        public void RecieveDamage(int damage)
        {
            if (_health <= 0) return;
            _health -= damage;
            if (_health <= 0)
            {
                _animator.SetTrigger("PlayDead");
                Destroy();
            }
        }

        private async void Destroy()
        {
            await _stopCommand.ExecuteSpecificCommand(new StopCommand());
            await Task.Delay(500);
            Destroy(gameObject);
        }
    }
}