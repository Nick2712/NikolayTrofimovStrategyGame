using NikolayTrofimov_StrategyGame.Abstractions;
using NikolayTrofimov_StrategyGame.Core;
using System;
using UnityEngine;
using Zenject;
using UniRx;


namespace NikolayTrofimov_StrategyGame.UserControlSystem.Model
{
    public sealed class CommandButtonsModel
    {
        public event Action<ICommandExecutor> OnCommandAccepted;
        public event Action OnCommandSent;
        public event Action OnCommandCancel;

        [Inject] private CommandCreatorBase<IProduceUnitCommand> _unitProducer;
        [Inject] private CommandCreatorBase<IAttackCommand> _attacker;
        [Inject] private CommandCreatorBase<IStopCommand> _stopper;
        [Inject] private CommandCreatorBase<IMoveCommand> _mover;
        [Inject] private CommandCreatorBase<IPatrolCommand> _patroller;
        [Inject] private CommandCreatorBase<ISetRallyPointCommand> _setrallyPoint;

        [Inject] private Vector3Value _vector3Value;

        private bool _commandIsPending;

        private IDisposable _currentSelectable;


        public void OnCommandButtonClicked(ICommandExecutor commandExecutor, ICommandsQueue commandsQueue)
        {
            if (_commandIsPending) ProcessOnCancel();
            _commandIsPending = true;
            OnCommandAccepted?.Invoke(commandExecutor);

            _unitProducer.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(command, commandsQueue));
            _attacker.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(command, commandsQueue));
            _stopper.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(command, commandsQueue));
            _mover.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(command, commandsQueue));
            _patroller.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(command, commandsQueue));
            _setrallyPoint.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(command, commandsQueue));
        }

        public void ExecuteCommandWrapper(object command, ICommandsQueue commandsQueue)
        {
            if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
                commandsQueue.Clear();

            commandsQueue.EnqueueCommand(command);
            _commandIsPending = false;
            OnCommandSent?.Invoke();
        }

        public void OnSelectionChanged(ISelectable selectable)
        {
            _currentSelectable?.Dispose();

            _commandIsPending = false;
            ProcessOnCancel();

            _currentSelectable = _vector3Value.ReactiveValue.Subscribe(_ => RMBMoveCommand(selectable));
        }

        private void RMBMoveCommand(ISelectable selectable)
        {
            var moveExecutor = (selectable as Component).gameObject.GetComponent<ICommandExecutor<IMoveCommand>>();
            var commandsQueue = (selectable as Component).gameObject.GetComponent<ICommandsQueue>();
            _mover.ProcessCommandExecutor(moveExecutor, command => ExecuteCommandWrapper(command, commandsQueue));
        }


        private void ProcessOnCancel()
        {
            _unitProducer.ProcessCancel();
            _attacker.ProcessCancel();
            _stopper.ProcessCancel();
            _mover.ProcessCancel();
            _patroller.ProcessCancel();
            _setrallyPoint.ProcessCancel();

            OnCommandCancel?.Invoke();
        }
    }
}