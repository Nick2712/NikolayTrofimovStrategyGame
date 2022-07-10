using System.Runtime.CompilerServices;


namespace NikolayTrofimov_StrategyGame.Utils
{
    public interface IAwaiter<TAwaited> : INotifyCompletion
    {
        bool IsCompleted { get; }
        TAwaited GetResult();
    }
}