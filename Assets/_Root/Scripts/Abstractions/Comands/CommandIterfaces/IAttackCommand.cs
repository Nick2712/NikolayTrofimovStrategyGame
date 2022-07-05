namespace NikolayTrofimov_StrategyGame.Abstractions
{
    public interface IAttackCommand : ICommand
    {
        IAttackable Target { get; }
    }
}