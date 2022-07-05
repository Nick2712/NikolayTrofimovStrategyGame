using NikolayTrofimov_StrategyGame.Abstractions;


namespace NikolayTrofimov_StrategyGame.Core
{
    public sealed class AttackCommand : IAttackCommand
    {
        public IAttackable Target { get; }

        public AttackCommand(IAttackable target)
        {
            Target = target;
        }
    }
}