using System;


namespace NikolayTrofimov_StrategyGame.Abstractions
{
    public interface IGameStatus
    {
        IObservable<int> Status { get; }
    }
}