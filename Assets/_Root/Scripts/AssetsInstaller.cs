using NikolayTrofimov_StrategyGame.Abstractions;
using NikolayTrofimov_StrategyGame.UserControlSystem.Model;
using NikolayTrofimov_StrategyGame.Utils;
using System;
using UnityEngine;
using Zenject;


namespace NikolayTrofimov_StrategyGame
{
    [CreateAssetMenu(fileName = "AssetsInstaller", menuName = "Installers/AssetsInstaller")]
    public class AssetsInstaller : ScriptableObjectInstaller<AssetsInstaller>
    {
        [SerializeField] private AssetsContext _legacyContext;
        [SerializeField] private Vector3Value _groundClickRMB;
        [SerializeField] private AttackableValue _attackableClickRMB;
        [SerializeField] private SelectableValue _selectables;

        public override void InstallBindings()
        {
            Container.BindInstances(_legacyContext, _groundClickRMB, _attackableClickRMB, _selectables);

            Container.Bind<IObservable<ISelectable>>().FromInstance(_selectables);
        }
    }
}