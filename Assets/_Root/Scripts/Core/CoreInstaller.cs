using NikolayTrofimov_StrategyGame.Abstractions;
using UnityEngine;
using Zenject;


namespace NikolayTrofimov_StrategyGame.Core
{
    public class CoreInstaller : MonoInstaller
    {
        [SerializeField] GameStatus _gameStatus;


        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<TimeModel>().AsSingle();
            Container.Bind<IGameStatus>().FromInstance(_gameStatus);
        }
    }
}