using NikolayTrofimov_StrategyGame.Abstractions;
using NikolayTrofimov_StrategyGame.Utils;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;


namespace NikolayTrofimov_StrategyGame.Core
{
    public sealed class UnitCanMove : CommandExecutorBase<IMoveCommand>
    {
        [SerializeField] private UnitMovementStop _stop;
        [SerializeField] private Animator _animator;
        [SerializeField] private UnitCanStop _stopCommandExecutor;

        private static readonly int _walk = Animator.StringToHash("Walk");
        private static readonly int _idle = Animator.StringToHash("Idle");


        public override async Task ExecuteSpecificCommand(IMoveCommand command)
        {
            GetComponent<NavMeshAgent>().destination = command.Target;
            _animator.SetTrigger(_walk);

            _stopCommandExecutor.CancellationTokenSource = new CancellationTokenSource();

            try
            {
                await _stop.WithCancellation(_stopCommandExecutor.CancellationTokenSource.Token);
            }
            catch
            {
                GetComponent<NavMeshAgent>().isStopped = true;
                GetComponent<NavMeshAgent>().ResetPath();
            }
            _stopCommandExecutor.CancellationTokenSource = null;

            _animator.SetTrigger(_idle);
        }
    }
}