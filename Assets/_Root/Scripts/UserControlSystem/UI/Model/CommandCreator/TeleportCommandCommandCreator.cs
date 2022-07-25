using NikolayTrofimov_StrategyGame.Abstractions;
using NikolayTrofimov_StrategyGame.Utils;
using System;
using UnityEngine;
using Zenject;
using UniRx;
using NikolayTrofimov_StrategyGame.Core;


namespace NikolayTrofimov_StrategyGame.UserControlSystem.Model
{
    public class TeleportCommandCommandCreator : CommandCreatorBase<ITeleportCommand>
    {
        [Inject] private AssetsContext _context;

        private Action<ITeleportCommand> _creationCallback;


        [Inject]
        private void Init(Vector3Value groundClicks)
        {
            groundClicks.ReactiveValue.Subscribe(OnNewValue);
        }

        private void OnNewValue(Vector3 groundClick)
        {
            _creationCallback?.Invoke(_context.Inject(new TeleportCommand(groundClick)));
            _creationCallback = null;
        }

        protected override void ClassSpecificCommandCreation(Action<ITeleportCommand> creationCallback)
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