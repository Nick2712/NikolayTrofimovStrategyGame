using NikolayTrofimov_StrategyGame.Abstractions;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;


namespace NikolayTrofimov_StrategyGame.Core
{
    public sealed class UnitCanTeleport : CommandExecutorBase<ITeleportCommand>
    {
        [SerializeField] private Animator _animator;

        public override async Task ExecuteSpecificCommand(ITeleportCommand command)
        {
            GetComponent<NavMeshAgent>().destination = command.Target;
            transform.position = command.Target;
        }
    }
}