using Zenject;


namespace NikolayTrofimov_StrategyGame.UserControlSystem.View
{
    public sealed class UiViewInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<BottomCenterView>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
    }
}