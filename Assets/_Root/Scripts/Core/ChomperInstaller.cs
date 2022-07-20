using NikolayTrofimov_StrategyGame.Abstractions;
using UnityEngine;
using Zenject;


namespace NikolayTrofimov_StrategyGame.Core
{
    public class ChomperInstaller : MonoInstaller
    {
        [SerializeField] private AttackerParallelInfoUpdater _attackerParallelInfoUpdater;
        [SerializeField] private FactionMemberParallelInfoUpdater _factionMemberParallelInfoUpdater;

        public override void InstallBindings()
        {
            Container.Bind<IHealthHolder>().FromComponentInChildren();
            Container.Bind<float>().WithId("AttackDistance").FromInstance(3f);
            Container.Bind<int>().WithId("AttackPeriod").FromInstance(1400);

            Container.Bind<IAutomaticAttacker>().FromComponentInChildren();
            Container.Bind<ITickable>()
                .FromInstance(_attackerParallelInfoUpdater);
            Container.Bind<ITickable>()
                .FromInstance(_factionMemberParallelInfoUpdater);
            Container.Bind<IFactionMember>().FromComponentInChildren();
            Container.Bind<ICommandsQueue>().FromComponentInChildren();
        }
    }
}