using NikolayTrofimov_StrategyGame.Utils;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace NikolayTrofimov_StrategyGame.Core
{
    public class UnitMovementStop : MonoBehaviour, IAwaitable<AsyncExtensions.Void>
    {
        public class StopAwaiter : IAwaiter<AsyncExtensions.Void>
        {
            private readonly UnitMovementStop _unitMovementStop;
            private Action _continuation;
            private bool _isCompleted;


            public StopAwaiter(UnitMovementStop unitMovementStop)
            {
                _unitMovementStop = unitMovementStop;
                _unitMovementStop.OnStop += OnStop;
            }

            private void OnStop()
            {
                _unitMovementStop.OnStop -= OnStop;
                _isCompleted = true;
                _continuation?.Invoke();
            }

            public void OnCompleted(Action continuation)
            {
                if (_isCompleted) continuation.Invoke();
                else _continuation = continuation;
            }

            public bool IsCompleted => _isCompleted;
            public AsyncExtensions.Void GetResult() => new AsyncExtensions.Void();
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