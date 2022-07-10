using NikolayTrofimov_StrategyGame.Abstractions;
using UnityEngine;
using UnityEngine.AI;


namespace NikolayTrofimov_StrategyGame.Core
{
    public sealed class UnitCanMove : CommandExecutorBase<IMoveCommand>
    {
        [SerializeField] private UnitMovementStop _stop;
        [SerializeField] private Animator _animator;

        private static readonly int _walk = Animator.StringToHash("Walk");
        private static readonly int _idle = Animator.StringToHash("Idle");

        public override async void ExecuteSpecificCommand(IMoveCommand command)
        {
            GetComponent<NavMeshAgent>().destination = command.Target;
            _animator.SetTrigger(_walk);
            await _stop;
            _animator.SetTrigger(_idle);
        }
    }
}