using NikolayTrofimov_StrategyGame.Abstractions;
using NikolayTrofimov_StrategyGame.Core;
using NikolayTrofimov_StrategyGame.Utils;
using System;
using UnityEngine;
using Zenject;
using UniRx;


namespace NikolayTrofimov_StrategyGame.UserControlSystem.Model
{
    public class SetRallyPointCommandCreator : CommandCreatorBase<ISetRallyPointCommand>
    {
        [Inject] private AssetsContext _context;

        private Action<ISetRallyPointCommand> _creationCallback;


        [Inject]
        private void Init(Vector3Value groundClicks)
        {
            groundClicks.ReactiveValue.Subscribe(OnNewValue);
        }

        private void OnNewValue(Vector3 groundClick)
        {
            _creationCallback?.Invoke(_context.Inject(new SetRallyPointCommand(groundClick)));
            _creationCallback = null;
        }

        protected override void ClassSpecificCommandCreation(Action<ISetRallyPointCommand> creationCallback)
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