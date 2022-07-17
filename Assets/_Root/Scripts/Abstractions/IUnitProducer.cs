using UniRx;


namespace NikolayTrofimov_StrategyGame.Abstractions
{
    public interface IUnitProducer
    {
        IReadOnlyReactiveCollection<IUnitProductionTask> Queue { get; }
        public void Cancel(int index);
    }
}