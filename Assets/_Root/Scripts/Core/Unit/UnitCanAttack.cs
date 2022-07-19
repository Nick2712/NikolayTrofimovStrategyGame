using NikolayTrofimov_StrategyGame.Abstractions;
using NikolayTrofimov_StrategyGame.Utils;
using System;
using System.Threading;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using Zenject;


namespace NikolayTrofimov_StrategyGame.Core
{
    public sealed class UnitCanAttack : CommandExecutorBase<IAttackCommand>
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private UnitCanStop _stopCommandExecutor;

        [Inject] private IHealthHolder _ourHealth;
        [Inject(Id = "AttackDistance")] private float _attackDistance;
        [Inject(Id = "AttackPeriod")] private int _attackPeriod;

        private Vector3 _ourPosition;
        private Vector3 _targetPosition;
        private Quaternion _ourRotation;

        private readonly Subject<Vector3> _targetPositions = new();
        private readonly Subject<Quaternion> _targetRotations = new();
        private readonly Subject<IAttackable> _attackTargets = new();

        private Transform _targetTransform;
        private AttackOperation _currentAttackOp;


        [Inject]
        private void Init()
        {
            _targetPositions
                .Select(value => new Vector3((float)Math.Round(value.x, 2), (float)Math.Round(value.y, 2), (float)Math.Round(value.z, 2)))
                .Distinct()
                .ObserveOnMainThread()
                .Subscribe(StartMovingToPosition);

            _attackTargets
                .ObserveOnMainThread()
                .Subscribe(StartAttackingTargets);

            _targetRotations
                .ObserveOnMainThread()
                .Subscribe(SetAttackRotation);
        }

        private void SetAttackRotation(Quaternion targetRotation) =>
            transform.rotation = targetRotation;

        private void StartAttackingTargets(IAttackable target)
        {
            GetComponent<NavMeshAgent>().isStopped = true;
            GetComponent<NavMeshAgent>().ResetPath();
            _animator.SetTrigger(Animator.StringToHash("Attack"));
            target.RecieveDamage(GetComponent<IDamageDealer>().Damage);
        }

        private void StartMovingToPosition(Vector3 position)
        {
            GetComponent<NavMeshAgent>().destination = position;
            _animator.SetTrigger(Animator.StringToHash("Walk"));
        }

        public override async Task ExecuteSpecificCommand(IAttackCommand command)
        {
            _targetTransform = (command.Target as Component).transform;
            _currentAttackOp = new AttackOperation(this, command.Target);
            Update();
            _stopCommandExecutor.CancellationTokenSource = new CancellationTokenSource();

            try
            {
                await _currentAttackOp.WithCancellation(_stopCommandExecutor.CancellationTokenSource.Token);
            }
            catch
            {
                _currentAttackOp.Cancel();
            }

            _animator.SetTrigger("Idle");
            _currentAttackOp = null;
            _targetTransform = null;
            _stopCommandExecutor.CancellationTokenSource = null;
        }

        private void Update()
        {
            if (_currentAttackOp == null) return;

            lock(this)
            {
                _ourPosition = transform.position;
                _ourRotation = transform.rotation;
                if (_targetTransform != null) _targetPosition = _targetTransform.position;
            }
        }

        public sealed class AttackOperation : IAwaitable<AsyncExtensions.Void>
        {
            public sealed class AttackOperationAwaiter : AwaiterBase<AsyncExtensions.Void>
            {
                private AttackOperation _attackOperation;

                public AttackOperationAwaiter(AttackOperation attackOperation)
                {
                    _attackOperation = attackOperation;
                    attackOperation.OnComplete += OnComplete;
                }

                private void OnComplete()
                {
                    _attackOperation.OnComplete -= OnComplete;
                    OnWaitFinish(new AsyncExtensions.Void());
                }
            }

            private event Action OnComplete;

            private readonly UnitCanAttack _attackCommandExecutor;
            private readonly IAttackable _target;

            private bool _isCancelled;


            public AttackOperation(UnitCanAttack attackCommandExecutor, IAttackable target)
            {
                _target = target;
                _attackCommandExecutor = attackCommandExecutor;

                var thread = new Thread(AttackAlgorytm);
                thread.Start();
            }

            public void Cancel()
            {
                _isCancelled = true;
                OnComplete?.Invoke();
            }

            private void AttackAlgorytm(object obj)
            {
                while(true)
                {
                    if(_attackCommandExecutor == null
                        || _attackCommandExecutor._ourHealth.Health == 0
                        || _target.Health == 0
                        || _isCancelled)
                    {
                        OnComplete?.Invoke();
                        return;
                    }

                    var targetPosition = default(Vector3);
                    var ourPosition = default(Vector3);
                    var ourRotation = default(Quaternion);
                    lock(_attackCommandExecutor)
                    {
                        targetPosition = _attackCommandExecutor._targetPosition;
                        ourPosition = _attackCommandExecutor._ourPosition;
                        ourRotation = _attackCommandExecutor._ourRotation;
                    }

                    var vector = targetPosition - ourPosition;
                    var distanceToTarget = vector.magnitude;
                    if(distanceToTarget > _attackCommandExecutor._attackDistance)
                    {
                        var finalDestination = targetPosition - vector.normalized * (_attackCommandExecutor._attackDistance * 0.9f);
                        _attackCommandExecutor._targetPositions.OnNext(finalDestination);
                        Thread.Sleep(100);
                    }
                    else if (ourRotation != Quaternion.LookRotation(vector))
                    {
                        _attackCommandExecutor._targetRotations.OnNext(Quaternion.LookRotation(vector));
                    }
                    else
                    {
                        _attackCommandExecutor._attackTargets.OnNext(_target);
                        Thread.Sleep(_attackCommandExecutor._attackPeriod);
                    }
                }
            }

            public IAwaiter<AsyncExtensions.Void> GetAwaiter() => new AttackOperationAwaiter(this);
        }
    }
}