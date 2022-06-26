using NikolayTrofimov_StrategyGame.Abstractions;
using System.Linq;
using UnityEngine;


namespace NikolayTrofimov_StrategyGame.Core
{
    public sealed class MainBuilding : MonoBehaviour, IUnitProducer, ISelectable
    {
        [SerializeField] private GameObject _unitPrefab;
        [SerializeField] private Transform _unitParent;

        [SerializeField] private float _maxHealth = 1000;
        [SerializeField] private Sprite _icon;

        [SerializeField] private float _health = 1000;


        public float Health => _health;
        public float MaxHeath => _maxHealth;
        public Sprite Icon => _icon;

        
        public void ProduceUnit()
        {
            Instantiate(_unitPrefab, 
                new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)), 
                Quaternion.identity, 
                _unitParent);
        }
    }
}