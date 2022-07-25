using NikolayTrofimov_StrategyGame.Abstractions;
using NikolayTrofimov_StrategyGame.Utils;
using UnityEngine;
using Zenject;


namespace NikolayTrofimov_StrategyGame.Core
{
    public class ProduceUnit2Command : IProduceUnit2Command
    {
        [Inject(Id = "Chomper 1")] public string UnitName { get; }
        [Inject(Id = "Chomper 1")] public Sprite Icon { get; }
        [Inject(Id = "Chomper 1")] public float ProductionTime { get; }
        public GameObject UnitPrefab => _unitPrefab;
        [InjectAsset("Chomper 1")] private GameObject _unitPrefab;
    }
}