using NikolayTrofimov_StrategyGame.Abstractions;


namespace NikolayTrofimov_StrategyGame.Core
{
    public class AutoAttackCommand : IAttackCommand
    {
        public IAttackable Target { get; }


        public AutoAttackCommand(IAttackable target)
        {
            Target = target;
        }
    }
}
