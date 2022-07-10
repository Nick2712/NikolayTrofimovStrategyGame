using NikolayTrofimov_StrategyGame.Utils;
using System;
using UnityEngine;
using UnityEngine.AI;


namespace NikolayTrofimov_StrategyGame.Core
{
    public class UnitMovementStop : MonoBehaviour, IAwaitable<AsyncExtensions.Void>
    {
        public class StopAwaiter : AwaiterBase<AsyncExtensions.Void>
        {
            private readonly UnitMovementStop _unitMovementStop;
            

            public StopAwaiter(UnitMovementStop unitMovementStop)
            {
                _unitMovementStop = unitMovementStop;
                _unitMovementStop.OnStop += OnStop;
                _result = new AsyncExtensions.Void();
            }

            private void OnStop()
            {
                _unitMovementStop.OnStop -= OnStop;
                _isCompleted = true;
                _continuation?.Invoke();
            }
        }

        public event Action OnStop;

        [SerializeField] private NavMeshAgent _agent;


        private void Update()
        {
            if (!_agent.pathPending)
                if (_agent.remainingDistance <= _agent.stoppingDistance)
                    if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0)
                        OnStop?.Invoke();
        }

        public IAwaiter<AsyncExtensions.Void> GetAwaiter() => new StopAwaiter(this);
    }
}