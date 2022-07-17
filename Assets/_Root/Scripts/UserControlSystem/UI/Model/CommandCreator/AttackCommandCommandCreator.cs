using NikolayTrofimov_StrategyGame.Abstractions;
using NikolayTrofimov_StrategyGame.Core;
using NikolayTrofimov_StrategyGame.Utils;
using System;
using Zenject;
using UniRx;


namespace NikolayTrofimov_StrategyGame.UserControlSystem.Model
{
    public sealed class AttackCommandCommandCreator : CommandCreatorBase<IAttackCommand>
    {
        [Inject] private AssetsContext _context;

        private Action<IAttackCommand> _creationCallback;


        [Inject]
        private void Init(AttackableValue targetClicks)
        {
            targetClicks.ReactiveValue.Subscribe(OnNewValue);
        }

        private void OnNewValue(IAttackable target)
        {
            _creationCallback?.Invoke(_context.Inject(new AttackCommand(target)));
            _creationCallback = null;
        }

        protected override void ClassSpecificCommandCreation(Action<IAttackCommand> creationCallback)
        {
            _creationCallback = creationCallback;
        }

        public override void ProcessCancel()
        {
            base.ProcessCancel();

            _creationCallback = null;
        }
    }
}