using NikolayTrofimov_StrategyGame.Abstractions;
using NikolayTrofimov_StrategyGame.Core;
using NikolayTrofimov_StrategyGame.UserControlSystem.Model;
using NikolayTrofimov_StrategyGame.UserControlSystem.View;
using NikolayTrofimov_StrategyGame.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace NikolayTrofimov_StrategyGame.UserControlSystem.Presenter
{
    public sealed class CommandButtonPresenter : MonoBehaviour
    {
        [SerializeField] private SelectableValue _selectable;
        [SerializeField] private CommandButtonsView _view;
        [SerializeField] private AssetsContext _context;

        private ISelectable _currentSelectable;


        private void Start()
        {
            _selectable.OnSelected += OnSelected;
            OnSelected(_selectable.CurrentValue);

            _view.OnClick += OnButtonClick;
        }

        private void OnSelected(ISelectable selectable)
        {
            if (_currentSelectable == selectable) return;

            _currentSelectable = selectable;

            _view.Clear();
            if(selectable != null)
            {
                var commandExecutors = new List<ICommandExecutor>();
                commandExecutors.AddRange((selectable as Component).GetComponentsInParent<ICommandExecutor>());
                _view.MakeLayout(commandExecutors);
            }
        }

        private void OnButtonClick(ICommandExecutor commandExecutor)
        {
            if (TryExecuteSpecificCommand<IProduceUnitCommand, ProduceUnitCommand>(commandExecutor)) return;
            if (TryExecuteSpecificCommand<IAttackCommand, AttackCommand>(commandExecutor)) return;
            if (TryExecuteSpecificCommand<IMoveCommand, MoveCommand>(commandExecutor)) return;
            if (TryExecuteSpecificCommand<IPatrolCommand, PatrolCommand>(commandExecutor)) return;
            if (TryExecuteSpecificCommand<IStopCommand, StopCommand>(commandExecutor)) return;

            throw new ApplicationException($"{nameof(CommandButtonPresenter)}.{nameof(OnButtonClick)}: " + 
                $"Unknown type of commands executor: {commandExecutor.GetType().FullName}!");
        }

        private bool TryExecuteSpecificCommand<T, U>(ICommandExecutor commandExecutor) where T : ICommand where U : T, new()
        {
            var unitProducer = commandExecutor as CommandExecutorBase<T>;
            if (unitProducer != null)
            {
                unitProducer.ExecuteSpecificCommand(_context.Inject(new U()));
                return true;
            }
            return false;
        }
    }
}