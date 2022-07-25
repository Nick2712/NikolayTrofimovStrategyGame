using UnityEngine;


namespace NikolayTrofimov_StrategyGame.Abstractions
{
    public interface ITeleportCommand : ICommand
    {
        public Vector3 Target { get; }
    }
}