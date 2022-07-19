using NikolayTrofimov_StrategyGame.Abstractions;
using Zenject;


namespace NikolayTrofimov_StrategyGame.Core
{
    public class ChomperInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IHealthHolder>().FromComponentInChildren();
            Container.Bind<float>().WithId("AttackDistance").FromInstance(3f);
            Container.Bind<int>().WithId("AttackPeriod").FromInstance(1400);
        }
    }
}