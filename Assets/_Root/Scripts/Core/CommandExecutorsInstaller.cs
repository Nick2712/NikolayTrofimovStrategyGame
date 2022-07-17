using NikolayTrofimov_StrategyGame.Abstractions;
using Zenject;


namespace NikolayTrofimov_StrategyGame.Core
{
    public class CommandExecutorsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var executors = gameObject.GetComponentsInChildren<ICommandExecutor>();
            foreach(var executor in executors)
            {
                var baseType = executor.GetType().BaseType;
                Container.Bind(baseType).FromInstance(executor);
            }
        }
    }
}