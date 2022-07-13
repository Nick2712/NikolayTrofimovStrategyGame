using System;


namespace NikolayTrofimov_StrategyGame.Abstractions
{
    public interface ITimeModel
    {
        IObservable<int> GameTime { get; }
    }
}