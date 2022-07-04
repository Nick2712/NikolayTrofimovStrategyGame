using UnityEngine;

namespace NikolayTrofimov_StrategyGame.Abstractions
{
    public interface IMoveCommand : ICommand
    {
        public Vector3 Target { get; }
    }
}