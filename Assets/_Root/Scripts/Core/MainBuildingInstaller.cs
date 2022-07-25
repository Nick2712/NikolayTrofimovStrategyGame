using NikolayTrofimov_StrategyGame.Abstractions;
using UnityEngine;
using Zenject;


namespace NikolayTrofimov_StrategyGame.Core
{
    public class MainBuildingInstaller : MonoInstaller
    {
        [SerializeField] private FactionMemberParallelInfoUpdater _factionMemberParallelInfoUpdater;


        public override void InstallBindings()
        {
            Container.Bind<ITickable>()
                .FromInstance(_factionMemberParallelInfoUpdater);
            Container.Bind<IFactionMember>().FromComponentInChildren();
        }
    }
}
