using NikolayTrofimov_StrategyGame.Abstractions;
using NikolayTrofimov_StrategyGame.Utils;
using System;
using UniRx;
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

        [SerializeField] private CollisionDetector _collisionDetector;
        [SerializeField] private int _throttleFrames = 60;
        [SerializeField] private int _continuityThreshold = 10;


        private void Awake()
        {
            _collisionDetector.Collisions
                .Where(_ => _agent.hasPath)
                .Where(collision => collision.collider.GetComponentInParent<IUnit>() != null)
                .Select(_ => Time.frameCount)
                .Distinct()
                .Buffer(_throttleFrames)
                .Where(buffer =>
                {
                    for(int i = 1; i < buffer.Count; i++)
                    {
                        if (buffer[i] - buffer[i - 1] > _continuityThreshold) return false;
                    }
                    return true;
                })
                .Subscribe(_ =>
                {
                    _agent.isStopped = true;
                    _agent.ResetPath();
                    OnStop?.Invoke();
                })
                .AddTo(this);

        }

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