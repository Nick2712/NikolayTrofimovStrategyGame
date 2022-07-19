namespace NikolayTrofimov_StrategyGame.Abstractions
{
    public interface IAttackable : IHealthHolder
    {
        void RecieveDamage(int damage);
    }
}