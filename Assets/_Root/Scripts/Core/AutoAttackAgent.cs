using NikolayTrofimov_StrategyGame.Abstractions;
using System.Linq;
using UniRx;
using UnityEngine;


namespace NikolayTrofimov_StrategyGame.Core
{
    internal class AutoAttackAgent : MonoBehaviour
    {
        [SerializeField] private ChomperCommandQueue _queue;


        private void Start()
        {
            AutoAttackEvaluator.AutoAttackCommands
                .ObserveOnMainThread()
                .Where(command => command.Attacker == gameObject)
                .Where(command => command.Attacker != null && command.Target != null)
                .Subscribe(command => AutoAttack(command.Target))
                .AddTo(this);
        }

        private void AutoAttack(GameObject target)
        { 
            _queue.Clear();
            _queue.EnqueueCommand(new AutoAttackCommand(target.GetComponent<IAttackable>()));
        }
    }
}
