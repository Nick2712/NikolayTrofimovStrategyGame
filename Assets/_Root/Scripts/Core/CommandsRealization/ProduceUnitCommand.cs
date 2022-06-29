using NikolayTrofimov_StrategyGame.Abstractions;
using NikolayTrofimov_StrategyGame.Utils;
using UnityEngine;


namespace NikolayTrofimov_StrategyGame.Core
{
    public class ProduceUnitCommand : IProduceUnitCommand
    {
        public GameObject UnitPrefab => _unitPrefab;
        [InjectAsset("Chomper")] private GameObject _unitPrefab;
    }
}