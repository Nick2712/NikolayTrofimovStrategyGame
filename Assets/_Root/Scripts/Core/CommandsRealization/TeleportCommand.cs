using NikolayTrofimov_StrategyGame.Abstractions;
using UnityEngine;


namespace NikolayTrofimov_StrategyGame.Core
{
    public class TeleportCommand : ITeleportCommand
    {
        public Vector3 Target { get; }

        public TeleportCommand(Vector3 target)
        {
            Target = target;
        }
    }
}